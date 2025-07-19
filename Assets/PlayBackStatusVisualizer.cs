using UnityEngine;

public class PlayBackStatusVisualizer : MonoBehaviour
{
    public TransformPlayBacker transformPlayBacker; // Reference to the TransformPlayBacker component to check its playback status.
    public Renderer sphereRenderer; // Reference to the Renderer component of the sphere (or any visual indicator).

    private Color standbyColor = Color.green; // Color to display when playback is not active (standby).
    private Color playBackColor = Color.red;  // Color to display when playback is active.

    void Update()
    {
        // Ensure both the TransformPlayBacker and the sphereRenderer are assigned.
        if (transformPlayBacker != null && sphereRenderer != null)
        {
            // Change the sphere's color based on the playback status.
            if (transformPlayBacker.isPlaying)
            {
                sphereRenderer.material.color = playBackColor; // Set to red when playing.
            }
            else
            {
                sphereRenderer.material.color = standbyColor; // Set to green when in standby.
            }
        }
    }
}