using UnityEngine;

public class RecordStatusVisualizer : MonoBehaviour
{
    public TransformRecorder transformRecorder; // Reference to the TransformRecorder component to check its recording status.
    public Renderer sphereRenderer; // Reference to the Renderer component of the sphere (or any visual indicator).

    void Start()
    {
        // If the sphereRenderer is not assigned in the Inspector, try to get it from this GameObject.
        if (sphereRenderer == null)
        {
            sphereRenderer = GetComponent<Renderer>();
        }
        UpdateColor(); // Set the initial color based on the recording status.
    }

    void Update()
    {
        UpdateColor(); // Continuously update the color based on the recording status.
    }

    /// <summary>
    /// Updates the color of the visual indicator (sphere) based on the current recording state
    /// of the TransformRecorder.
    /// </summary>
    void UpdateColor()
    {
        // Ensure both the TransformRecorder and the sphereRenderer are assigned.
        if (transformRecorder == null || sphereRenderer == null)
        {
            return; // Exit if references are missing to prevent NullReferenceExceptions.
        }

        if (transformRecorder.isRecordingInProgress)
        {
            // If a recording process is active (either delayed or actual recording).
            if (transformRecorder.isRecording)
            {
                // Currently in the active recording phase.
                sphereRenderer.material.color = Color.red; // Indicate active recording.
            }
            else
            {
                // Currently in the recording delay phase.
                sphereRenderer.material.color = Color.yellow; // Indicate pending recording.
            }
        }
        else
        {
            // Not recording, in standby mode.
            sphereRenderer.material.color = Color.green; // Indicate standby/idle.
        }
    }
}