using UnityEngine;

public class DrumSheetCursor : MonoBehaviour
{
    public TransformPlayBacker transformPlayBacker; // Reference to the TransformPlayBacker, which controls playback.

    private DrumSheet drumSheet; // Reference to the DrumSheet, providing cursor start/end positions.
    private Vector3 startPosition; // The world position where the cursor begins its movement.
    private Vector3 endPosition;   // The world position where the cursor ends its movement.
    private float journeyLength;   // The total distance the cursor travels.
    private bool isMoving = false; // Flag to indicate if the cursor is currently in motion.

    private void OnEnable()
    {
        // Obtain the DrumSheet reference from the TransformPlayBacker.
        if (transformPlayBacker != null)
        {
            drumSheet = transformPlayBacker.drumSheet;

            // Subscribe to events from TransformPlayBacker to control cursor movement.
            transformPlayBacker.OnPlayTransformDataStart += StartMoving;
            transformPlayBacker.OnPlayTransformDataEnd += StopMoving;
        }
    }

    private void OnDisable()
    {
        // Unsubscribe from TransformPlayBacker events to prevent memory leaks.
        if (transformPlayBacker != null)
        {
            transformPlayBacker.OnPlayTransformDataStart -= StartMoving;
            transformPlayBacker.OnPlayTransformDataEnd -= StopMoving;
        }
    }

    /// <summary>
    /// Initiates the cursor's movement.
    /// Sets the start and end positions based on the DrumSheet's anchors and calculates the total journey length.
    /// </summary>
    private void StartMoving()
    {
        if (drumSheet != null && drumSheet.drumSheetCursorStart != null && drumSheet.drumSheetCursorEnd != null)
        {
            // Retrieve the start and end world positions for the cursor.
            startPosition = drumSheet.drumSheetCursorStart.position;
            endPosition = drumSheet.drumSheetCursorEnd.position;

            // Calculate the total distance the cursor needs to travel.
            journeyLength = Vector3.Distance(startPosition, endPosition);

            // Optionally, reset the cursor to the start position immediately.
            // transform.position = startPosition;

            // Set the movement flag to true.
            isMoving = true;
        }
    }

    /// <summary>
    /// Halts the cursor's movement.
    /// </summary>
    private void StopMoving()
    {
        isMoving = false;
    }

    private void Update()
    {
        // Only update the cursor's position if it's set to move and the journey length is valid.
        if (isMoving && journeyLength > 0)
        {
            // Get the current elapsed time from the TransformPlayBacker's playback data.
            // This time represents the current point in the overall playback duration.
            float elapsedTime = transformPlayBacker.playbackData.dataList[transformPlayBacker.currentIndex].timestamp;

            // Calculate the total duration of the playback from the last timestamp in the data list.
            float totalDuration = transformPlayBacker.playbackData.dataList[transformPlayBacker.playbackData.dataList.Count - 1].timestamp;
            
            // Calculate the fractional progress of the journey (0.0 to 1.0).
            // This determines how far along the path the cursor should be.
            float fracJourney = elapsedTime / totalDuration;

            // Linearly interpolate the cursor's position between the start and end points
            // based on the calculated fractional progress.
            transform.position = Vector3.Lerp(startPosition, endPosition, fracJourney);

            // If the cursor has reached or passed the end of its journey, stop its movement.
            if (fracJourney >= 1f)
            {
                StopMoving();
            }
        }
    }
}