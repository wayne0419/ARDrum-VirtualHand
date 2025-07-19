using System.Collections;
using UnityEngine;

public class PracticeRecorder : MonoBehaviour
{
    public TransformRecorder transformRecorder; // Reference to the TransformRecorder component.
    public TransformPlayBacker transformPlayBacker; // Reference to the TransformPlayBacker component.
    public float recordDelayBeats = 4f; // The number of beats to wait before starting the recording.
    public float recordDurationBeats = 4f; // The duration of each recording segment in beats.
    public bool isPracticeRecording = false; // Flag indicating if a practice recording session is currently active.

    private Coroutine practiceCoroutine; // Stores the reference to the running practice coroutine.

    void Update()
    {
        // Toggle practice recording on Spacebar press.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isPracticeRecording)
            {
                StartPracticeRecord();
            }
            else
            {
                StopPracticeRecord();
            }
        }
    }

    /// <summary>
    /// Initiates a practice recording session.
    /// Sets the `isPracticeRecording` flag and starts the `StartPractice` coroutine.
    /// </summary>
    public void StartPracticeRecord()
    {
        isPracticeRecording = true;
        practiceCoroutine = StartCoroutine(StartPractice());
    }

    /// <summary>
    /// Stops the current practice recording session.
    /// Stops the `StartPractice` coroutine, halts playback, and resets the `isPracticeRecording` flag.
    /// </summary>
    public void StopPracticeRecord()
    {
        if (practiceCoroutine != null)
        {
            StopCoroutine(practiceCoroutine);
            practiceCoroutine = null;
            transformPlayBacker.StopPlayBack(); // Stop the playback controlled by TransformPlayBacker.
            isPracticeRecording = false;
        }
    }

    /// <summary>
    /// Coroutine that manages the practice recording loop.
    /// It starts playback, waits for a delay, then triggers a recording segment, repeating indefinitely.
    /// </summary>
    private IEnumerator StartPractice()
    {
        // Start the playback in mode A (assuming A is the continuous playback mode).
        transformPlayBacker.playMode = TransformPlayBacker.PlayMode.A;
        transformPlayBacker.StartPlayBack();

        while (true)
        {
            // Wait for the specified recording delay, then start a recording segment for the specified duration.
            // The `StartRecord` method itself is a coroutine that yields until recording is complete.
            yield return transformRecorder.StartRecord(recordDelayBeats, recordDurationBeats);
        }
    }
}