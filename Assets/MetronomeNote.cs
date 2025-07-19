using UnityEngine;

public class MetronomeNote : MonoBehaviour
{
    public Color onColor = Color.white;     // The color of the note when it is active but not highlighted.
    public Color highlightColor = Color.yellow; // The color of the note when it is specifically highlighted (e.g., on the downbeat).

    private Renderer noteRenderer; // The Renderer component responsible for displaying the note.

    private void Awake()
    {
        // Get the Renderer component attached to this GameObject.
        noteRenderer = GetComponent<Renderer>();

        // Initialize the note's color to its default 'onColor' when the object awakes.
        if (noteRenderer != null)
        {
            noteRenderer.material.color = onColor;
        }
    }

    /// <summary>
    /// Sets the note's visual color to the predefined 'onColor'.
    /// This typically represents an active but not emphasized beat.
    /// </summary>
    public void SetOn()
    {
        if (noteRenderer != null)
        {
            noteRenderer.material.color = onColor;
        }
    }

    /// <summary>
    /// Sets the note's visual color to the predefined 'highlightColor'.
    /// This typically represents an emphasized beat, like the first beat of a measure.
    /// </summary>
    public void SetHighlight()
    {
        if (noteRenderer != null)
        {
            noteRenderer.material.color = highlightColor;
        }
    }
}