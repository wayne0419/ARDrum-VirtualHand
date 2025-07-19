using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem; // Required for Unity's new Input System.

public class TransformRecorder : MonoBehaviour
{
    /// <summary>
    /// Represents a single drum hit event, including its intensity and the limb used.
    /// </summary>
    [System.Serializable]
    public class DrumHit
    {
        public float value; // The intensity or velocity of the drum hit.
        public string limb; // The limb (e.g., "lefthand", "righthand", "rightfeet") used for the hit.

        public DrumHit(float val, string limbType)
        {
            value = val;
            limb = limbType;
        }
    }

    /// <summary>
    /// Stores the transform data (position, rotation) for multiple tracked objects
    /// and drum hit information at a specific timestamp.
    /// </summary>
    [System.Serializable]
    public class TransformData
    {
        public Vector3 position1; // Position of the first tracked Transform (e.g., left hand).
        public Quaternion rotation1; // Rotation of the first tracked Transform.
        public Vector3 position2; // Position of the second tracked Transform (e.g., right hand).
        public Quaternion rotation2; // Rotation of the second tracked Transform.
        public Vector3 position3; // Position of the third tracked Transform (e.g., right foot).
        public Quaternion rotation3; // Rotation of the third tracked Transform.
        public DrumHit bassDrumHit; // Hit data for the Bass Drum.
        public DrumHit snareDrumHit; // Hit data for the Snare Drum.
        public DrumHit closedHiHatHit; // Hit data for the Closed Hi-Hat.
        public DrumHit tom1Hit; // Hit data for Tom 1.
        public DrumHit tom2Hit; // Hit data for Tom 2.
        public DrumHit floorTomHit; // Hit data for the Floor Tom.
        public DrumHit crashHit; // Hit data for the Crash Cymbal.
        public DrumHit rideHit; // Hit data for the Ride Cymbal.
        public DrumHit openHiHatHit; // Hit data for the Open Hi-Hat.
        public float timestamp; // The time (in seconds) at which this data was recorded.

        public TransformData(Vector3 pos1, Quaternion rot1, Vector3 pos2, Quaternion rot2, Vector3 pos3, Quaternion rot3,
                             DrumHit bassHit, DrumHit snareHit, DrumHit hiHatHit, DrumHit t1Hit, DrumHit t2Hit,
                             DrumHit floorHit, DrumHit crashH, DrumHit rideH, DrumHit openHiHat, float time)
        {
            position1 = pos1;
            rotation1 = rot1;
            position2 = pos2;
            rotation2 = rot2;
            position3 = pos3;
            rotation3 = rot3;
            bassDrumHit = bassHit;
            snareDrumHit = snareHit;
            closedHiHatHit = hiHatHit;
            tom1Hit = t1Hit;
            tom2Hit = t2Hit;
            floorTomHit = floorHit;
            crashHit = crashH;
            rideHit = rideH;
            openHiHatHit = openHiHat;
            timestamp = time;
        }
    }

    public Transform targetTransform1; // The first Transform to record (e.g., left hand controller).
    public Transform targetTransform2; // The second Transform to record (e.g., right hand controller).
    public Transform targetTransform3; // The third Transform to record (e.g., right foot controller).
    public string folderPath = "Assets/RecordedTransforms"; // The folder where recorded data will be saved.
    public float bpm = 120f; // The BPM at which the recording is made.
    public float recordDelayBeats = 4f; // Number of beats to delay before actual recording starts.
    public float recordDurationBeats = 4f; // Duration of the recording in beats.
    public Metronome metronome; // Reference to the Metronome component for synchronization.
    public AnimationClip animationClip; // AnimationClip to apply to targetTransform3 (e.g., for foot pedal animation).
    public bool allowInputControl = true; // Flag to enable/disable Spacebar control for recording.
    public TransformPlayBacker transformPlayBacker; // Reference to the TransformPlayBacker to update its file path after saving.

    // Input Actions for various drum hits. These should be set up in the Unity Input System.
    public InputAction bassDrumHit;
    public InputAction snareDrumHit;
    public InputAction closedHiHatHit;
    public InputAction tom1Hit;
    public InputAction tom2Hit;
    public InputAction floorTomHit;
    public InputAction crashHit;
    public InputAction rideHit;
    public InputAction openHiHatHit;

    private List<TransformData> transformDataList = new List<TransformData>(); // List to store recorded TransformData.
    public bool isRecording = false; // Flag indicating if actual recording is in progress.
    public bool isRecordingInProgress = false; // Flag indicating if the recording process (including delay) is active.
    private float recordingStartTime = 0f; // The Unity `Time.time` when the actual recording started.

    void OnEnable()
    {
        // Enable all InputActions to start listening for input.
        bassDrumHit.Enable();
        snareDrumHit.Enable();
        closedHiHatHit.Enable();
        tom1Hit.Enable();
        tom2Hit.Enable();
        floorTomHit.Enable();
        crashHit.Enable();
        rideHit.Enable();
        openHiHatHit.Enable();
    }

    void OnDisable()
    {
        // Disable all InputActions when the script is disabled to stop listening for input.
        bassDrumHit.Disable();
        snareDrumHit.Disable();
        closedHiHatHit.Disable();
        tom1Hit.Disable();
        tom2Hit.Disable();
        floorTomHit.Disable();
        crashHit.Disable();
        rideHit.Disable();
        openHiHatHit.Disable();
    }

    void Update()
    {
        // Handle Spacebar input to start a new recording session if allowed and no session is active.
        if (allowInputControl && Input.GetKeyDown(KeyCode.Space))
        {
            if (!isRecordingInProgress)
            {
                // If a metronome is assigned, set its BPM and start it.
                if (metronome != null)
                {
                    metronome.bpm = bpm;
                    metronome.StartMetronome();
                }
                // Start the recording coroutine.
                StartRecord(recordDelayBeats, recordDurationBeats);
            }
        }

        // If actual recording is in progress, record the current transform data.
        if (isRecording)
        {
            RecordTransform();
        }
    }

    /// <summary>
    /// Public method to start the recording process with specified delay and duration in beats.
    /// </summary>
    /// <param name="delayBeats">Number of beats to delay before recording starts.</param>
    /// <param name="recordBeats">Duration of recording in beats.</param>
    /// <returns>A Coroutine reference.</returns>
    public Coroutine StartRecord(float delayBeats, float recordBeats)
    {
        return StartCoroutine(StartRecordingAfterBeats(delayBeats, recordBeats));
    }

    /// <summary>
    /// Coroutine to manage the recording process, including initial delay and recording duration.
    /// </summary>
    /// <param name="delayBeats">Number of beats to wait before starting recording.</param>
    /// <param name="recordBeats">Duration of recording in beats.</param>
    System.Collections.IEnumerator StartRecordingAfterBeats(float delayBeats, float recordBeats)
    {
        isRecordingInProgress = true; // Indicate that a recording process has started.
        float beatDuration = 60f / bpm; // Calculate the duration of a single beat in seconds.

        // Wait for the specified delay duration.
        yield return new WaitForSeconds(delayBeats * beatDuration);

        isRecording = true; // Start actual recording.
        recordingStartTime = Time.time; // Mark the precise start time of recording.
        transformDataList.Clear(); // Clear any previous recorded data.

        // Wait for the specified recording duration.
        yield return new WaitForSeconds(recordBeats * beatDuration);

        isRecording = false; // Stop actual recording.
        SaveTransformData(); // Save the recorded data to a file.

        // Stop the metronome if it's running.
        if (metronome != null)
        {
            metronome.StopMetronome();
        }

        isRecordingInProgress = false; // Indicate that the entire recording process has finished.
    }

    /// <summary>
    /// Records the current transform data of the target objects and drum hit states.
    /// This method is called every frame while `isRecording` is true.
    /// </summary>
    void RecordTransform()
    {
        if (targetTransform1 != null && targetTransform2 != null)
        {
            // Check if each drum input action was triggered and create a DrumHit object.
            // If not triggered, value is 0 and limb is "no limb".
            DrumHit bassDrumHitValue = bassDrumHit.triggered ? new DrumHit(bassDrumHit.ReadValue<float>(), "unspecified") : new DrumHit(0f, "no limb");
            DrumHit snareDrumHitValue = snareDrumHit.triggered ? new DrumHit(snareDrumHit.ReadValue<float>(), "unspecified") : new DrumHit(0f, "no limb");
            DrumHit closedHiHatHitValue = closedHiHatHit.triggered ? new DrumHit(closedHiHatHit.ReadValue<float>(), "unspecified") : new DrumHit(0f, "no limb");
            DrumHit tom1HitValue = tom1Hit.triggered ? new DrumHit(tom1Hit.ReadValue<float>(), "unspecified") : new DrumHit(0f, "no limb");
            DrumHit tom2HitValue = tom2Hit.triggered ? new DrumHit(tom2Hit.ReadValue<float>(), "unspecified") : new DrumHit(0f, "no limb");
            DrumHit floorTomHitValue = floorTomHit.triggered ? new DrumHit(floorTomHit.ReadValue<float>(), "unspecified") : new DrumHit(0f, "no limb");
            DrumHit crashHitValue = crashHit.triggered ? new DrumHit(crashHit.ReadValue<float>(), "unspecified") : new DrumHit(0f, "no limb");
            DrumHit rideHitValue = rideHit.triggered ? new DrumHit(rideHit.ReadValue<float>(), "unspecified") : new DrumHit(0f, "no limb");
            DrumHit openHiHatHitValue = openHiHatHit.triggered ? new DrumHit(openHiHatHit.ReadValue<float>(), "unspecified") : new DrumHit(0f, "no limb");

            float timestamp = Time.time - recordingStartTime; // Calculate timestamp relative to recording start.

            // If bass drum was hit, overwrite targetTransform3's animation data.
            // This implies targetTransform3 is used for a foot pedal animation that needs to be synchronized with hits.
            if (bassDrumHit.triggered)
            {
                float bassDrumHitTime = timestamp;
                OverwriteTransformDataWithAnimationClip(bassDrumHitTime);
            }

            // Get current position and rotation for targetTransform3 (which might have been updated by animation).
            Vector3 position3 = targetTransform3.position;
            Quaternion rotation3 = targetTransform3.rotation;

            // Create a new TransformData entry with current states and add to the list.
            TransformData data = new TransformData(
                targetTransform1.position, targetTransform1.rotation,
                targetTransform2.position, targetTransform2.rotation,
                position3, rotation3,
                bassDrumHitValue,
                snareDrumHitValue,
                closedHiHatHitValue,
                tom1HitValue,
                tom2HitValue,
                floorTomHitValue,
                crashHitValue,
                rideHitValue,
                openHiHatHitValue,
                timestamp
            );
            transformDataList.Add(data);
        }
    }

    /// <summary>
    /// Overwrites the recorded position and rotation data for `targetTransform3`
    /// within a specific time window, applying an `AnimationClip` to simulate animation.
    /// This is typically used for foot pedal animations that are triggered by hits.
    /// </summary>
    /// <param name="endTime">The timestamp of the drum hit that triggers this animation.</param>
    void OverwriteTransformDataWithAnimationClip(float endTime)
    {
        float clipLength = animationClip.length;
        // The animation clip is assumed to be based on 120 BPM. Adjust its effective length based on current recording BPM.
        float adjustedClipLength = clipLength * (120f / bpm); 

        for (int i = 0; i < transformDataList.Count; i++)
        {
            float timestamp = transformDataList[i].timestamp;
            // Check if the current data point's timestamp falls within the animation's active window.
            // The window starts `adjustedClipLength` before `endTime`.
            if (timestamp <= endTime && timestamp >= endTime - adjustedClipLength)
            {
                // Calculate the normalized time within the animation clip (0.0 to 1.0).
                float animationTime = (timestamp - (endTime - adjustedClipLength)) / (120f / bpm); 
                
                // Sample the animation clip at the calculated time and apply it to targetTransform3.
                animationClip.SampleAnimation(targetTransform3.gameObject, animationTime);
                
                // Overwrite the recorded position and rotation for targetTransform3 with the animated values.
                transformDataList[i].position3 = targetTransform3.position;
                transformDataList[i].rotation3 = targetTransform3.rotation;
            }
        }
    }

    /// <summary>
    /// Saves the recorded transform data to a JSON file.
    /// The file is named sequentially (e.g., 0.json, 1.json) within the specified folder.
    /// It also updates the `jsonFilePath` in the `TransformPlayBacker` component.
    /// </summary>
    public void SaveTransformData()
    {
        // Create the folder if it doesn't exist.
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        // Count existing JSON files to determine the next sequential file name.
        int fileCount = Directory.GetFiles(folderPath, "*.json").Length;
        string filePath = Path.Combine(folderPath, fileCount + ".json");

        // Create a container object for serialization, including the BPM.
        TransformDataList dataList = new TransformDataList(bpm, transformDataList);
        // Serialize the data to JSON with pretty printing.
        string json = JsonUtility.ToJson(dataList, true);
        // Write the JSON string to the file.
        File.WriteAllText(filePath, json);

        Debug.Log("Transform data saved to " + filePath);

        // Update the TransformPlayBacker's JSON file path to the newly saved file.
        if (transformPlayBacker != null)
        {
            transformPlayBacker.jsonFilePath = filePath;
        }
    }

    /// <summary>
    /// A serializable class to encapsulate the BPM and the list of TransformData.
    /// This structure is used for JSON serialization/deserialization.
    /// </summary>
    [System.Serializable]
    public class TransformDataList
    {
        public float bpm; // The BPM associated with this list of transform data.
        public List<TransformData> dataList; // The list of recorded TransformData.

        public TransformDataList(float bpmValue, List<TransformData> list)
        {
            bpm = bpmValue;
            dataList = list;
        }
    }
}