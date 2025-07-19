using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class TransformPlayBacker : MonoBehaviour
{
    /// <summary>
    /// Represents a single drum hit event with its intensity and the limb used.
    /// </summary>
    [Serializable]
    public class DrumHit
    {
        public float value; // The intensity or velocity of the drum hit.
        public string limb; // The limb (e.g., "lefthand", "righthand", "rightfeet") used for the hit.
    }

    /// <summary>
    /// Stores the transform data (position, rotation) for multiple tracked objects
    /// and drum hit information at a specific timestamp.
    /// </summary>
    [Serializable]
    public class TransformData
    {
        public Vector3 position1; // Position of the first tracked Transform (e.g., left hand).
        public Quaternion rotation1; // Rotation of the first tracked Transform.
        public Vector3 position2; // Position of the second tracked Transform (e.g., right hand).
        public Quaternion rotation2; // Rotation of the second tracked Transform.
        public Vector3 position3; // Position of the third tracked Transform (e.g., right foot).
        public Quaternion rotation3; // Rotation of the third tracked Transform.
        public float timestamp; // The time (in seconds) at which this data was recorded.
        public DrumHit bassDrumHit; // Hit data for the Bass Drum.
        public DrumHit snareDrumHit; // Hit data for the Snare Drum.
        public DrumHit closedHiHatHit; // Hit data for the Closed Hi-Hat.
        public DrumHit tom1Hit; // Hit data for Tom 1.
        public DrumHit tom2Hit; // Hit data for Tom 2.
        public DrumHit floorTomHit; // Hit data for the Floor Tom.
        public DrumHit crashHit; // Hit data for the Crash Cymbal.
        public DrumHit rideHit; // Hit data for the Ride Cymbal.
        public DrumHit openHiHatHit; // Hit data for the Open Hi-Hat.
    }

    /// <summary>
    /// A container class for a list of TransformData, including the original BPM of the recording.
    /// </summary>
    [Serializable]
    public class TransformPlaybackData
    {
        public float bpm; // The BPM at which the original data was recorded.
        public List<TransformData> dataList; // The list of recorded TransformData points.
    }

    /// <summary>
    /// Represents a segment of a drum hit, defining its start and end indices in the data list,
    /// the limb used, the drum type, and whether it should be skipped during playback/tracking.
    /// </summary>
    [Serializable]
    public class HitSegment
    {
        public string limbUsed; // The limb associated with this hit segment.
        public DrumType drumHit; // The type of drum hit in this segment.
        public int startIdx; // The starting index in `playbackData.dataList` for this segment.
        public int endIdx; // The ending index in `playbackData.dataList` for this segment.
        public bool skip; // Flag to indicate if this segment should be skipped (e.g., for practice modes).
        public DrumNote associatedNote; // Reference to the visual DrumNote object on the drum sheet.
    }

    /// <summary>
    /// Defines different playback modes.
    /// </summary>
    public enum PlayMode { A, B }
    public PlayMode playMode = PlayMode.A; // The current playback mode, defaults to A.

    public string jsonFilePath; // Path to the JSON file containing recorded transform data.
    public Transform targetTransform1; // The first Transform object to animate (e.g., left hand controller).
    public Transform targetTransform2; // The second Transform object to animate (e.g., right hand controller).
    public Transform targetTransform3; // The third Transform object to animate (e.g., right foot controller).
    public float playBackBPM; // The desired BPM for playback. This will scale the original recording's speed.
    public float startOffsetBeat; // Number of beats to wait before starting the actual playback data.
    public Metronome metronome; // Reference to the Metronome component for synchronization.
    public DrumSheet drumSheet; // Reference to the DrumSheet component for managing drum notes.
    public DrumSheetCursor drumSheetCursor; // Reference to the DrumSheetCursor for visual tracking.

    // AudioSources for playing drum hit sounds.
    public AudioSource bassDrumAudioSource;
    public AudioSource snareDrumAudioSource;
    public AudioSource closedHiHatAudioSource;
    public AudioSource tom1AudioSource;
    public AudioSource tom2AudioSource;
    public AudioSource floorTomAudioSource;
    public AudioSource crashAudioSource;
    public AudioSource rideAudioSource;
    public AudioSource openHiHatAudioSource;

    public DrumHitIndicator drumHitIndicator; // Reference to the DrumHitIndicator for visual hit feedback.
    public GameObject PlayBackVirtualMan; // The GameObject representing the virtual player (can be toggled).
    public GameObject drumAudio; // The GameObject containing drum audio components (can be toggled).

    public TransformPlaybackData playbackData; // The loaded playback data from the JSON file.
    public List<HitSegment> hitSegments; // List of generated hit segments from the playback data.
    public int currentIndex; // The current index in `playbackData.dataList` being processed.
    private float playbackSpeedMultiplier; // Calculated multiplier to adjust playback speed based on BPM.
    private float offsetDuration; // Calculated duration of the initial offset in seconds.
    public bool isPlaying = false; // Flag indicating if playback is currently active.
    public bool allowInputControl = true; // Flag to enable/disable Spacebar control for playback.
    private Coroutine playbackCoroutine; // Reference to the currently running playback coroutine.
    public float playbackStartTime; // The Unity `Time.time` when the current playback segment started.

    // Event triggered when the PlayTransformData coroutine begins.
    public event Action OnPlayTransformDataStart;

    // Event triggered when the PlayTransformData coroutine ends.
    public event Action OnPlayTransformDataEnd;

    private bool isPlayingTransformData = false; // Internal flag to track if the core playback coroutine is active.

    void OnEnable()
    {
        // Load the JSON file containing playback data every time the component is enabled.
        LoadJsonFile(jsonFilePath);

        // Initialize the drum hit indicator.
        if (drumHitIndicator != null)
        {
            drumHitIndicator.Initialize();
        }

        // Generate hit segments from the loaded playback data.
        GenerateHitSegments();
    }

    void Update()
    {
        // Handle Spacebar input to toggle playback if allowed.
        if (allowInputControl && Input.GetKeyDown(KeyCode.Space))
        {
            if (!isPlaying)
            {
                StartPlayBack();
            }
            else
            {
                StopPlayBack();
            }
        }

        // Input for switching playback modes.
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            playMode = PlayMode.A;
        }
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            playMode = PlayMode.B;
        }

        // Input for toggling visibility of virtual man, drum hit indicator, and drum audio.
        if (Input.GetKeyDown(KeyCode.Keypad9) && PlayBackVirtualMan != null)
        {
            PlayBackVirtualMan.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Keypad8) && PlayBackVirtualMan != null)
        {
            PlayBackVirtualMan.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Keypad6) && drumHitIndicator != null)
        {
            drumHitIndicator.gameObject.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Keypad5) && drumHitIndicator != null)
        {
            drumHitIndicator.gameObject.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Keypad3) && drumAudio != null)
        {
            drumAudio.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Keypad2) && drumAudio != null)
        {
            drumAudio.SetActive(false);
        }
    }

    /// <summary>
    /// Starts the playback process based on the selected `playMode`.
    /// Calculates playback speed and offset duration.
    /// </summary>
    public void StartPlayBack()
    {
        // Calculate the playback speed multiplier and initial offset duration.
        if (playbackData != null)
        {
            playbackSpeedMultiplier = playBackBPM / playbackData.bpm;
            offsetDuration = 60f / playBackBPM * startOffsetBeat;
        }
        else
        {
            playbackSpeedMultiplier = 1f;
            offsetDuration = 0f;
        }

        isPlaying = true;

        // Start the appropriate playback coroutine based on the current mode.
        if (playMode == PlayMode.A)
        {
            playbackCoroutine = StartCoroutine(PlayBackCoroutineA());
        }
        else if (playMode == PlayMode.B)
        {
            playbackCoroutine = StartCoroutine(PlayBackCoroutineB());
        }
    }

    /// <summary>
    /// Stops the current playback process.
    /// Halts the playback coroutine and stops the metronome.
    /// </summary>
    public void StopPlayBack()
    {
        // Stop the active playback coroutine.
        if (playbackCoroutine != null)
        {
            StopCoroutine(playbackCoroutine);

            // Ensure the OnPlayTransformDataEnd event is triggered if the core playback was active.
            if (isPlayingTransformData)
            {
                OnPlayTransformDataEnd?.Invoke();
                isPlayingTransformData = false;
            }
        }

        isPlaying = false;

        // Stop the metronome.
        if (metronome != null)
        {
            metronome.StopMetronome();
        }
    }

    /// <summary>
    /// Playback Coroutine for Mode A: Continuous loop of the entire recorded data.
    /// Includes an initial offset and continuous metronome synchronization.
    /// </summary>
    private IEnumerator PlayBackCoroutineA()
    {
        while (isPlaying) // Loop indefinitely as long as `isPlaying` is true.
        {
            // Start/restart the metronome with the current playback BPM.
            if (metronome != null)
            {
                metronome.bpm = playBackBPM;
                metronome.StopMetronome(); // Ensure it's stopped before restarting to prevent multiple instances.
                metronome.StartMetronome();
            }

            // Wait for the initial blank offset duration.
            yield return new WaitForSeconds(offsetDuration);

            playbackStartTime = Time.time; // Record the actual start time of data playback.
            currentIndex = 0; // Reset index to start from the beginning of the data.

            // Play through the entire transform data.
            yield return PlayTransformData();
        }
    }

    /// <summary>
    /// Playback Coroutine for Mode B: Plays the entire recorded data once,
    /// then loops from the second beat onwards.
    /// </summary>
    private IEnumerator PlayBackCoroutineB()
    {
        // Start/restart the metronome.
        if (metronome != null)
        {
            metronome.bpm = playBackBPM;
            metronome.StopMetronome();
            metronome.StartMetronome();
        }

        // Wait for the initial blank offset duration.
        yield return new WaitForSeconds(offsetDuration);

        playbackStartTime = Time.time;
        currentIndex = 0;

        // Play the entire TransformPlayBackData once.
        yield return PlayTransformData();

        // Loop playback, skipping the first beat's duration.
        while (isPlaying)
        {
            playbackStartTime = Time.time;

            // Calculate the time duration of one beat at the original recording's BPM.
            float skipBeat = 1f;

            // Reset Metronome for the loop.
            if (metronome != null)
            {
                metronome.bpm = playBackBPM;
                metronome.StopMetronome();
                metronome.StartMetronome();
            }

            // Play TransformData, starting from an offset (skipping the first beat).
            yield return PlayTransformData(skipBeat * (60f / playbackData.bpm));
        }
    }

    /// <summary>
    /// Core coroutine for playing back transform data.
    /// Interpolates transform positions/rotations and triggers drum hits.
    /// </summary>
    /// <param name="startTimeOffset">An offset in seconds to start playback from within the data list.</param>
    private IEnumerator PlayTransformData(float startTimeOffset = 0f)
    {
        OnPlayTransformDataStart?.Invoke(); // Trigger the start event.
        isPlayingTransformData = true;

        if (startTimeOffset > 0)
        {
            // Determine the starting index in the data list based on the `startTimeOffset`.
            currentIndex = GetStartIndexAfterSkipTime(startTimeOffset);
        }

        // Loop through the data list until the end.
        while (currentIndex < playbackData.dataList.Count - 1)
        {
            // Calculate elapsed time, adjusted by playback speed multiplier and initial offset.
            float elapsedTime = (Time.time - playbackStartTime) * playbackSpeedMultiplier + startTimeOffset;

            // Advance `currentIndex` if the elapsed time has passed the next data point's timestamp.
            // This handles cases where frames are skipped or playback is very fast.
            while (currentIndex < playbackData.dataList.Count - 1 && elapsedTime >= playbackData.dataList[currentIndex + 1].timestamp)
            {
                // If a data point is skipped, ensure its drum hits are still processed.
                CheckAndPlayDrumHits(playbackData.dataList[currentIndex]);
                currentIndex++;
            }

            // If there are still data points to process, interpolate transforms.
            if (currentIndex < playbackData.dataList.Count - 1)
            {
                float targetTime = playbackData.dataList[currentIndex].timestamp;
                float nextTime = playbackData.dataList[currentIndex + 1].timestamp;

                // Calculate the interpolation factor (t) between the current and next data points.
                float t = Mathf.InverseLerp(targetTime, nextTime, elapsedTime);

                // Update the target Transforms using linear interpolation.
                UpdateTransforms(currentIndex, currentIndex + 1, t);
            }

            yield return null; // Wait for the next frame.
        }

        // Ensure the last transform data is applied when playback finishes.
        UpdateTransforms(currentIndex, currentIndex, 1.0f);

        OnPlayTransformDataEnd?.Invoke(); // Trigger the end event.
        isPlayingTransformData = false;
    }

    /// <summary>
    /// Finds the index in `playbackData.dataList` corresponding to a given skip time.
    /// </summary>
    /// <param name="skipTime">The time (in seconds) to skip to.</param>
    /// <returns>The index of the first data point whose timestamp is greater than or equal to `skipTime`.</returns>
    private int GetStartIndexAfterSkipTime(float skipTime)
    {
        for (int i = 0; i < playbackData.dataList.Count; i++)
        {
            if (playbackData.dataList[i].timestamp >= skipTime)
            {
                return i;
            }
        }
        return 0; // If skipTime is beyond all data, start from the beginning.
    }

    /// <summary>
    /// Loads transform playback data from a JSON file.
    /// </summary>
    /// <param name="path">The file path to the JSON data.</param>
    void LoadJsonFile(string path)
    {
        if (File.Exists(path))
        {
            string jsonContent = File.ReadAllText(path);
            playbackData = JsonUtility.FromJson<TransformPlaybackData>(jsonContent);
        }
        else
        {
            Debug.LogError("JSON file not found: " + path);
        }
    }

    /// <summary>
    /// Updates the positions and rotations of the target Transforms by interpolating
    /// between two data points from the `playbackData`.
    /// Transforms are only updated if their corresponding segment is not marked as skipped.
    /// </summary>
    /// <param name="indexA">The index of the first data point.</param>
    /// <param name="indexB">The index of the second data point.</param>
    /// <param name="t">The interpolation factor (0.0 to 1.0).</param>
    void UpdateTransforms(int indexA, int indexB, float t)
    {
        TransformData dataA = playbackData.dataList[indexA];
        TransformData dataB = playbackData.dataList[indexB];

        // Update left hand (position1 and rotation1) if not skipped.
        if (!IsCurrentSegmentSkipped(indexA, "lefthand"))
        {
            targetTransform1.position = Vector3.Lerp(dataA.position1, dataB.position1, t);
            targetTransform1.rotation = Quaternion.Lerp(dataA.rotation1, dataB.rotation1, t);
        }

        // Update right hand (position2 and rotation2) if not skipped.
        if (!IsCurrentSegmentSkipped(indexA, "righthand"))
        {
            targetTransform2.position = Vector3.Lerp(dataA.position2, dataB.position2, t);
            targetTransform2.rotation = Quaternion.Lerp(dataA.rotation2, dataB.rotation2, t);
        }

        // Update right foot (position3 and rotation3) if not skipped.
        if (!IsCurrentSegmentSkipped(indexA, "rightfeet"))
        {
            targetTransform3.position = Vector3.Lerp(dataA.position3, dataB.position3, t);
            targetTransform3.rotation = Quaternion.Lerp(dataA.rotation3, dataB.rotation3, t);
        }
    }

    /// <summary>
    /// Checks the current `TransformData` for drum hit events and plays corresponding audio
    /// and visual indicators, provided the segment is not skipped.
    /// </summary>
    /// <param name="data">The current TransformData to check for hits.</param>
    void CheckAndPlayDrumHits(TransformData data)
    {
        // Play Bass Drum hit audio and trigger indicator if hit value > 0 and not skipped.
        if (data.bassDrumHit.value > 0f && !IsCurrentSegmentSkipped(currentIndex, data.bassDrumHit.limb))
        {
            PlayDrumHit(bassDrumAudioSource, data.bassDrumHit.value);
            if (drumHitIndicator != null)
            {
                drumHitIndicator.TriggerBassDrum(data.bassDrumHit.limb);
            }
        }

        // Play Snare Drum hit audio and trigger indicator.
        if (data.snareDrumHit.value > 0f && !IsCurrentSegmentSkipped(currentIndex, data.snareDrumHit.limb))
        {
            PlayDrumHit(snareDrumAudioSource, data.snareDrumHit.value);
            if (drumHitIndicator != null)
            {
                drumHitIndicator.TriggerSnareDrum(data.snareDrumHit.limb);
            }
        }

        // Play Closed Hi-Hat hit audio and trigger indicator.
        if (data.closedHiHatHit.value > 0f && !IsCurrentSegmentSkipped(currentIndex, data.closedHiHatHit.limb))
        {
            PlayDrumHit(closedHiHatAudioSource, data.closedHiHatHit.value);
            if (drumHitIndicator != null)
            {
                drumHitIndicator.TriggerClosedHiHat(data.closedHiHatHit.limb);
            }
        }

        // Play Tom 1 hit audio and trigger indicator.
        if (data.tom1Hit.value > 0f && !IsCurrentSegmentSkipped(currentIndex, data.tom1Hit.limb))
        {
            PlayDrumHit(tom1AudioSource, data.tom1Hit.value);
            if (drumHitIndicator != null)
            {
                drumHitIndicator.TriggerTom1(data.tom1Hit.limb);
            }
        }

        // Play Tom 2 hit audio and trigger indicator.
        if (data.tom2Hit.value > 0f && !IsCurrentSegmentSkipped(currentIndex, data.tom2Hit.limb))
        {
            PlayDrumHit(tom2AudioSource, data.tom2Hit.value);
            if (drumHitIndicator != null)
            {
                drumHitIndicator.TriggerTom2(data.tom2Hit.limb);
            }
        }

        // Play Floor Tom hit audio and trigger indicator.
        if (data.floorTomHit.value > 0f && !IsCurrentSegmentSkipped(currentIndex, data.floorTomHit.limb))
        {
            PlayDrumHit(floorTomAudioSource, data.floorTomHit.value);
            if (drumHitIndicator != null)
            {
                drumHitIndicator.TriggerFloorTom(data.floorTomHit.limb);
            }
        }

        // Play Crash Cymbal hit audio and trigger indicator.
        if (data.crashHit.value > 0f && !IsCurrentSegmentSkipped(currentIndex, data.crashHit.limb))
        {
            PlayDrumHit(crashAudioSource, data.crashHit.value);
            if (drumHitIndicator != null)
            {
                drumHitIndicator.TriggerCrash(data.crashHit.limb);
            }
        }

        // Play Ride Cymbal hit audio and trigger indicator.
        if (data.rideHit.value > 0f && !IsCurrentSegmentSkipped(currentIndex, data.rideHit.limb))
        {
            PlayDrumHit(rideAudioSource, data.rideHit.value);
            if (drumHitIndicator != null)
            {
                drumHitIndicator.TriggerRide(data.rideHit.limb);
            }
        }

        // Play Open Hi-Hat hit audio and trigger indicator.
        if (data.openHiHatHit.value > 0f && !IsCurrentSegmentSkipped(currentIndex, data.openHiHatHit.limb))
        {
            PlayDrumHit(openHiHatAudioSource, data.openHiHatHit.value);
            if (drumHitIndicator != null)
            {
                drumHitIndicator.TriggerOpenHiHat(data.openHiHatHit.limb);
            }
        }
    }

    /// <summary>
    /// Plays a drum hit sound through the given AudioSource with a specified volume.
    /// </summary>
    /// <param name="audioSource">The AudioSource component to play the sound.</param>
    /// <param name="volume">The volume at which to play the sound.</param>
    void PlayDrumHit(AudioSource audioSource, float volume)
    {
        if (audioSource != null)
        {
            audioSource.volume = volume;
            audioSource.PlayOneShot(audioSource.clip);
        }
    }

    /// <summary>
    /// Generates `HitSegment` objects from the `playbackData`.
    /// Each segment represents a continuous period of time between drum hits by a specific limb.
    /// It also associates each segment with its corresponding `DrumNote` on the drum sheet.
    /// </summary>
    void GenerateHitSegments()
    {
        hitSegments = new List<HitSegment>();
        // Tracks the last index where a hit occurred for each limb, used to define segment start.
        Dictionary<string, int> lastHitIndex = new Dictionary<string, int>();
        // Counts hits for each drum type to correctly associate with `DrumNote` objects by index.
        Dictionary<DrumType, int> drumHitCounter = new Dictionary<DrumType, int>();

        for (int i = 0; i < playbackData.dataList.Count; i++)
        {
            var data = playbackData.dataList[i];
            // Process each possible drum hit type in the current data frame.
            ProcessHit(data.bassDrumHit, DrumType.BassDrum, i, lastHitIndex, drumHitCounter);
            ProcessHit(data.snareDrumHit, DrumType.SnareDrum, i, lastHitIndex, drumHitCounter);
            ProcessHit(data.closedHiHatHit, DrumType.ClosedHiHat, i, lastHitIndex, drumHitCounter);
            ProcessHit(data.tom1Hit, DrumType.Tom1, i, lastHitIndex, drumHitCounter);
            ProcessHit(data.tom2Hit, DrumType.Tom2, i, lastHitIndex, drumHitCounter);
            ProcessHit(data.floorTomHit, DrumType.FloorTom, i, lastHitIndex, drumHitCounter);
            ProcessHit(data.crashHit, DrumType.Crash, i, lastHitIndex, drumHitCounter);
            ProcessHit(data.rideHit, DrumType.Ride, i, lastHitIndex, drumHitCounter);
            ProcessHit(data.openHiHatHit, DrumType.OpenHiHat, i, lastHitIndex, drumHitCounter);
        }
    }

    /// <summary>
    /// Helper method to process a single drum hit within a `TransformData` frame.
    /// It creates a `HitSegment` if a hit occurred and associates it with a `DrumNote`.
    /// </summary>
    /// <param name="drumHit">The DrumHit object (e.g., data.bassDrumHit).</param>
    /// <param name="drumType">The DrumType enum value for this hit.</param>
    /// <param name="currentIndex">The current index in the `playbackData.dataList`.</param>
    /// <param name="lastHitIndex">Dictionary tracking the last hit index for each limb.</param>
    /// <param name="drumHitCounter">Dictionary counting hits for each drum type.</param>
    void ProcessHit(DrumHit drumHit, DrumType drumType, int currentIndex, Dictionary<string, int> lastHitIndex, Dictionary<DrumType, int> drumHitCounter)
    {
        if (drumHit.value > 0) // If a hit occurred (value > 0).
        {
            // Determine the start index of this hit segment. It's either the index after the last hit
            // by the same limb, or 0 if it's the first hit for that limb.
            int startIdx = lastHitIndex.ContainsKey(drumHit.limb) ? lastHitIndex[drumHit.limb] + 1 : 0;
            int endIdx = currentIndex; // The end index of the segment is the current data index.

            // Increment the counter for this specific drum type to get its sequential index.
            if (!drumHitCounter.ContainsKey(drumType))
            {
                drumHitCounter[drumType] = 0;
            }
            int hitIndex = drumHitCounter[drumType];
            drumHitCounter[drumType]++;

            // Get the corresponding visual DrumNote from the DrumSheet based on type and sequential index.
            DrumNote associatedNote = drumSheet.GetDrumNoteByIndex(drumType, hitIndex);

            // Create and add the new HitSegment.
            HitSegment segment = new HitSegment
            {
                limbUsed = drumHit.limb,
                drumHit = drumType,
                startIdx = startIdx,
                endIdx = endIdx,
                skip = false, // Default to not skipped.
                associatedNote = associatedNote // Link to the visual DrumNote.
            };

            hitSegments.Add(segment);

            // Establish a bidirectional link: set the associatedSegment in the DrumNote.
            if (associatedNote != null)
            {
                associatedNote.associatedSegment = segment;
            }

            // Update the last hit index for the limb that just hit.
            lastHitIndex[drumHit.limb] = currentIndex;
        }
    }

    /// <summary>
    /// Checks if the current playback index falls within a `HitSegment` that is marked as skipped
    /// for a specific limb.
    /// </summary>
    /// <param name="currentIndex">The current index in the playback data list.</param>
    /// <param name="limb">The limb to check (e.g., "lefthand", "righthand").</param>
    /// <returns>True if the current index is within a skipped segment for the given limb, false otherwise.</returns>
    bool IsCurrentSegmentSkipped(int currentIndex, string limb)
    {
        foreach (var segment in hitSegments)
        {
            if (currentIndex >= segment.startIdx && currentIndex <= segment.endIdx && segment.skip && segment.limbUsed == limb)
            {
                return true;
            }
        }
        return false;
    }
}