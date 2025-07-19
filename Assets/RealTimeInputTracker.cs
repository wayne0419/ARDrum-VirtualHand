using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; // Required for Unity's new Input System.

public class RealTimeInputTracker : MonoBehaviour
{
    /// <summary>
    /// Defines the modes for judging the correctness of drum hits.
    /// </summary>
    public enum CorrectMode
    {
        CorrectRhythmMode, // Judgment based on timing accuracy (within a time tolerance).
        CorrectOrderMode   // Judgment based on the sequence of hits, allowing any correct drum type at the current beat.
    }

    public CorrectMode currentMode = CorrectMode.CorrectRhythmMode; // The currently active correctness judgment mode.

    public TransformPlayBacker transformPlayBacker; // Reference to the TransformPlayBacker to monitor playback state and data.
    public GameObject HitDrumInputCorrectMarker;     // Prefab for visual feedback of a perfectly timed hit.
    public GameObject HitDrumInputLevel1ErrorMarker; // Prefab for visual feedback of a Level 1 timing error (minor deviation).
    public GameObject HitDrumInputErrorMarker;       // Prefab for visual feedback of an incorrect hit (e.g., wrong drum in order mode).
    public GameObject HitDrumInputMissMarker;        // Prefab for visual feedback when a required hit is missed or an extra hit occurs.

    public float correctTimeTolerance = 0.1f;     // Time window (in seconds) for a hit to be considered perfectly correct.
    public float level1ErrorTimeTolerance = 0.2f; // Time window (in seconds) for a hit to be considered a Level 1 error.
    public float level1ErrorShift = 0.1f;         // Visual offset for Level 1 error markers to indicate early/late hits.
    
    public Transform markerHolder; // Parent Transform for all generated hit markers to keep the hierarchy clean.

    // InputActions for various drum hits. These should be set up in the Unity Input System.
    public InputAction bassDrumHit;
    public InputAction snareDrumHit;
    public InputAction closedHiHatHit;
    public InputAction tom1Hit;
    public InputAction tom2Hit;
    public InputAction floorTomHit;
    public InputAction crashHit;
    public InputAction rideHit;
    public InputAction openHiHatHit;

    private bool isTracking = false; // Flag to indicate if real-time input tracking is active.
    public List<HitDrumInputData> inputLog; // A log of all recorded drum input events.
    private List<TrackedHitSegment> trackedHitSegments; // A working copy of HitSegments from playback data, used for tracking matches.

    // Event triggered when the tracking session concludes (i.e., StopTracking() is called).
    public event Action OnFinishTracking;

    private void Awake()
    {
        inputLog = new List<HitDrumInputData>(); // Initialize the input log.
    }

    private void OnEnable()
    {
        // Subscribe to TransformPlayBacker's playback events to control tracking.
        if (transformPlayBacker != null)
        {
            transformPlayBacker.OnPlayTransformDataStart += StartTracking;
            transformPlayBacker.OnPlayTransformDataEnd += StopTracking;
        }
    }

    private void OnDisable()
    {
        // Unsubscribe from playback events to prevent memory leaks.
        if (transformPlayBacker != null)
        {
            transformPlayBacker.OnPlayTransformDataStart -= StartTracking;
            transformPlayBacker.OnPlayTransformDataEnd -= StopTracking;
        }
    }

    /// <summary>
    /// Initiates real-time input tracking.
    /// Clears previous data, destroys old markers, initializes tracked segments, and enables InputActions.
    /// This method is typically called when TransformPlayBacker starts playing.
    /// </summary>
    private void StartTracking()
    {
        isTracking = true;
        inputLog.Clear(); // Clear any historical input data.

        // Destroy all previously generated visual markers.
        if (markerHolder != null)
        {
            foreach (Transform child in markerHolder)
            {
                Destroy(child.gameObject);
            }
        }

        // Create a deep copy of the TransformPlayBacker's hit segments for tracking.
        // This allows modification (e.g., `matched` status) without affecting the original data.
        trackedHitSegments = new List<TrackedHitSegment>();
        foreach (var segment in transformPlayBacker.hitSegments)
        {
            trackedHitSegments.Add(new TrackedHitSegment
            {
                limbUsed = segment.limbUsed,
                drumHit = segment.drumHit,
                startIdx = segment.startIdx,
                endIdx = segment.endIdx,
                skip = segment.skip, // Preserve the 'skip' status from the original segment.
                associatedNote = segment.associatedNote,
                matched = false, // Initialize as unmatched.
                correct = false, // Initialize as incorrect.
                level1TimeError = false // Initialize as no Level 1 error.
            });
        }

        // Enable all defined InputActions to start listening for input.
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

    /// <summary>
    /// Stops real-time input tracking.
    /// Disables InputActions and triggers the OnFinishTracking event.
    /// This method is typically called when TransformPlayBacker stops playing.
    /// </summary>
    private void StopTracking()
    {
        isTracking = false;
        OnFinishTracking?.Invoke(); // Notify subscribers that tracking has finished.

        // Disable all InputActions to stop listening for input.
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

    private void Update()
    {
        if (isTracking)
        {
            // Continuously check and log input for each drum type.
            CheckAndLogInput(bassDrumHit, DrumType.BassDrum);
            CheckAndLogInput(snareDrumHit, DrumType.SnareDrum);
            CheckAndLogInput(closedHiHatHit, DrumType.ClosedHiHat);
            CheckAndLogInput(tom1Hit, DrumType.Tom1);
            CheckAndLogInput(tom2Hit, DrumType.Tom2);
            CheckAndLogInput(floorTomHit, DrumType.FloorTom);
            CheckAndLogInput(crashHit, DrumType.Crash);
            CheckAndLogInput(rideHit, DrumType.Ride);
            CheckAndLogInput(openHiHatHit, DrumType.OpenHiHat);
        }
        // Debug input for toggling marker visibility.
        if (Input.GetKeyDown(KeyCode.Keypad7))
        {
            markerHolder.gameObject.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            markerHolder.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Checks if a given InputAction was triggered in the current frame and logs the hit data.
    /// It then calls the appropriate correctness check based on the `currentMode`.
    /// </summary>
    /// <param name="inputAction">The InputAction to check (e.g., bassDrumHit).</param>
    /// <param name="drumType">The DrumType associated with this input action.</param>
    private void CheckAndLogInput(InputAction inputAction, DrumType drumType)
    {
        if (inputAction.triggered)
        {
            // Get the current timestamp from the playback data, adjusted for playback speed.
            float timestamp = transformPlayBacker.playbackData.dataList[transformPlayBacker.currentIndex].timestamp 
                              * (transformPlayBacker.playbackData.bpm / transformPlayBacker.playBackBPM);
            float hitValue = inputAction.ReadValue<float>(); // Get the input value (e.g., pressure).

            // Log the input event.
            inputLog.Add(new HitDrumInputData
            {
                drumType = drumType,
                timestamp = timestamp,
                hitValue = hitValue
            });

            // Perform correctness check based on the current mode.
            if (currentMode == CorrectMode.CorrectRhythmMode)
            {
                CheckHitDrumCorrectRhythmMode(drumType, timestamp);
            }
            else if (currentMode == CorrectMode.CorrectOrderMode)
            {
                CheckHitDrumCorrectOrderMode(drumType);
            }
        }
    }

    /// <summary>
    /// Checks if a drum hit is correct in `CorrectRhythmMode`.
    /// This mode focuses on the timing accuracy of the hit relative to expected drum segments.
    /// </summary>
    /// <param name="drumType">The type of drum that was hit.</param>
    /// <param name="timestamp">The timestamp of the user's hit.</param>
    private void CheckHitDrumCorrectRhythmMode(DrumType drumType, float timestamp)
    {
        bool matched = false; // Flag to indicate if the user's hit successfully matched any expected segment.

        foreach (var segment in trackedHitSegments)
        {
            // Only consider segments that haven't been matched yet, are not skipped, and match the drum type.
            if (!segment.matched && !segment.skip && segment.drumHit == drumType)
            {
                // Get the expected timestamp for the end of the segment, adjusted for current playback BPM.
                float segmentTimestamp = transformPlayBacker.playbackData.dataList[segment.endIdx].timestamp;
                float adjustedTimestamp = segmentTimestamp * transformPlayBacker.playbackData.bpm / transformPlayBacker.playBackBPM;
                float timeDifference = Mathf.Abs(timestamp - adjustedTimestamp);

                // Check for perfect correctness within `correctTimeTolerance`.
                if (timeDifference < correctTimeTolerance)
                {
                    segment.matched = true;
                    segment.correct = true; // Mark as perfectly correct.

                    // Instantiate a correct marker at the associated drum note's position.
                    if (HitDrumInputCorrectMarker != null && segment.associatedNote != null)
                    {
                        Vector3 notePosition = segment.associatedNote.transform.position;
                        Instantiate(HitDrumInputCorrectMarker, notePosition, Quaternion.identity, markerHolder);
                    }
                    matched = true;
                    break; // A match was found, exit the loop.
                }
                // Check for Level 1 error within `level1ErrorTimeTolerance`.
                else if (timeDifference < level1ErrorTimeTolerance)
                {
                    segment.matched = true;
                    segment.level1TimeError = true; // Mark as a Level 1 timing error.

                    if (HitDrumInputLevel1ErrorMarker != null && segment.associatedNote != null)
                    {
                        Vector3 notePosition = segment.associatedNote.transform.position;

                        // Offset the marker visually to indicate if the hit was early or late.
                        if (timestamp < adjustedTimestamp)
                        {
                            notePosition.x -= level1ErrorShift; // Hit too early, shift left.
                        }
                        else
                        {
                            notePosition.x += level1ErrorShift; // Hit too late, shift right.
                        }

                        Instantiate(HitDrumInputLevel1ErrorMarker, notePosition, Quaternion.identity, markerHolder);
                    }
                    matched = true;
                    break; // A match was found, exit the loop.
                }
            }
        }

        // If no matching segment was found for the user's hit, it's considered a "Miss" or an extra hit.
        if (!matched)
        {
            if (HitDrumInputMissMarker != null && transformPlayBacker.drumSheetCursor != null)
            {
                Vector3 cursorPosition = transformPlayBacker.drumSheetCursor.transform.position;
                // Adjust the Y position of the miss marker to align with the corresponding drum row on the sheet.
                if (drumType == DrumType.Crash) {
                    cursorPosition.y = transformPlayBacker.drumSheet.drumSheetCrashRowAnchor.transform.position.y;
                }
                else if (drumType == DrumType.OpenHiHat) {
                    cursorPosition.y = transformPlayBacker.drumSheet.drumSheetOpenHiHatRowAnchor.transform.position.y;
                }
                else if (drumType == DrumType.ClosedHiHat) {
                    cursorPosition.y = transformPlayBacker.drumSheet.drumSheetClosedHiHatRowAnchor.transform.position.y;
                }
                else if (drumType == DrumType.Ride) {
                    cursorPosition.y = transformPlayBacker.drumSheet.drumSheetRideRowAnchor.transform.position.y;
                }
                else if (drumType == DrumType.Tom1) {
                    cursorPosition.y = transformPlayBacker.drumSheet.drumSheetTom1RowAnchor.transform.position.y;
                }
                else if (drumType == DrumType.Tom2) {
                    cursorPosition.y = transformPlayBacker.drumSheet.drumSheetTom2RowAnchor.transform.position.y;
                }
                else if (drumType == DrumType.FloorTom) {
                    cursorPosition.y = transformPlayBacker.drumSheet.drumSheetFloorTomRowAnchor.transform.position.y;
                }
                else if (drumType == DrumType.SnareDrum) {
                    cursorPosition.y = transformPlayBacker.drumSheet.drumSheetSnareRowAnchor.transform.position.y;
                }
                else if (drumType == DrumType.BassDrum) {
                    cursorPosition.y = transformPlayBacker.drumSheet.drumSheetBassRowAnchor.transform.position.y;
                }
                Instantiate(HitDrumInputMissMarker, cursorPosition, Quaternion.identity, markerHolder);
            }
        }
    }

    /// <summary>
    /// Checks if a drum hit is correct in `CorrectOrderMode`.
    /// This mode focuses on hitting the correct drum(s) at the current expected beat position, regardless of precise timing within the beat.
    /// </summary>
    /// <param name="drumType">The type of drum that was hit by the user.</param>
    private void CheckHitDrumCorrectOrderMode(DrumType drumType)
    {
        // Find the smallest beat position among all unmatched and unskipped segments.
        // This represents the "current" beat that the player is expected to hit.
        float currentBeatPosition = float.PositiveInfinity;
        foreach (var segment in trackedHitSegments)
        {
            if (!segment.matched && !segment.skip && segment.associatedNote.beatPosition < currentBeatPosition)
            {
                currentBeatPosition = segment.associatedNote.beatPosition;
            }
        }

        // Check if the user's hit matches any unskipped, unmatched segment at the `currentBeatPosition`.
        bool findMatched = false;
        foreach (var segment in trackedHitSegments)
        {
            if (!segment.matched && !segment.skip && segment.associatedNote.beatPosition == currentBeatPosition)
            {
                // If the hit drum type matches an expected drum type at this beat position, mark it as correct.
                if (segment.drumHit == drumType)
                {
                    segment.matched = true;
                    segment.correct = true; // Mark as correct.

                    // Instantiate a correct marker.
                    if (HitDrumInputCorrectMarker != null && segment.associatedNote != null)
                    {
                        Vector3 notePosition = segment.associatedNote.transform.position;
                        Instantiate(HitDrumInputCorrectMarker, notePosition, Quaternion.identity, markerHolder);
                    }
                    findMatched = true;
                    break; // A correct match was found, exit the loop.
                }
            }
        }

        // If the user's hit did not match any expected segment at `currentBeatPosition` (i.e., it was an incorrect drum or an extra hit),
        // then mark the *next* expected segment at `currentBeatPosition` as an error.
        if (!findMatched)
        {
            foreach (var segment in trackedHitSegments)
            {
                if (!segment.matched && !segment.skip && segment.associatedNote.beatPosition == currentBeatPosition)
                {
                    segment.matched = true;
                    segment.correct = false; // Mark as incorrect.

                    // Instantiate an error marker.
                    if (HitDrumInputErrorMarker != null && segment.associatedNote != null)
                    {
                        Vector3 notePosition = segment.associatedNote.transform.position;
                        Instantiate(HitDrumInputErrorMarker, notePosition, Quaternion.identity, markerHolder);
                    }
                    break; // Mark only the first unmatched segment at this beat as an error.
                }
            }
        }
    }

    /// <summary>
    /// Returns the list of tracked hit segments, including their matched and correctness status.
    /// </summary>
    /// <returns>A List of TrackedHitSegment objects.</returns>
    public List<TrackedHitSegment> GetTrackedHitSegments()
    {
        return trackedHitSegments;
    }

    /// <summary>
    /// Serializable class to store data for each recorded drum input.
    /// </summary>
    [Serializable]
    public class HitDrumInputData
    {
        public DrumType drumType; // The type of drum that was hit.
        public float timestamp;   // The timestamp (in seconds) when the hit occurred.
        public float hitValue;    // The intensity or value of the hit (e.g., pressure, velocity).
    }

    /// <summary>
    /// Extends the base HitSegment class to include tracking-specific properties
    /// for matching user input against expected hits.
    /// </summary>
    [Serializable]
    public class TrackedHitSegment : TransformPlayBacker.HitSegment
    {
        public bool matched = false;       // True if this segment has been successfully matched by a user input.
        public bool correct = false;       // True if the matched input was perfectly correct (within `correctTimeTolerance`).
        public bool level1TimeError = false; // True if the matched input was a Level 1 timing error (within `level1ErrorTimeTolerance`).
    }
}