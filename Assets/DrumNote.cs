using UnityEngine;

public class DrumNote : MonoBehaviour
{
    public DrumType drumType; // Specifies the type of drum this note represents.
    public Color skippedColor; // The color applied to the note when it is marked as skipped.
    public Color defaultColor; // The default color of the note.
    public TransformPlayBacker.HitSegment associatedSegment; // The HitSegment data associated with this visual drum note.
    public float beatPosition; // The beat position in the song that this drum note corresponds to.

    private Renderer noteRenderer; // The renderer component responsible for displaying the drum note.

    void Awake()
    {
        noteRenderer = GetComponent<Renderer>();
        if (noteRenderer != null)
        {
            noteRenderer.material.color = defaultColor; // Initialize the note's color to its default.
        }
    }

    /// <summary>
    /// Sets the material color of the drum note to the predefined skippedColor.
    /// </summary>
    public void SetSkippedColor()
    {
        if (noteRenderer != null)
        {
            noteRenderer.material.color = skippedColor;
        }
    }

    /// <summary>
    /// Sets the material color of the drum note back to its predefined defaultColor.
    /// </summary>
    public void SetDefaultColor()
    {
        if (noteRenderer != null)
        {
            noteRenderer.material.color = defaultColor;
        }
    }

    /// <summary>
    /// Marks the associated HitSegment as 'skipped' and updates the visual drum note's color
    /// to reflect its skipped status.
    /// </summary>
    public void SetSkip()
    {
        if (associatedSegment != null)
        {
            associatedSegment.skip = true;
            SetSkippedColor(); // Change the note's visual appearance to skipped.
        }
    }

    /// <summary>
    /// Unmarks the associated HitSegment as 'skipped' and reverts the visual drum note's color
    /// to its default appearance.
    /// </summary>
    public void SetUnSkip()
    {
        if (associatedSegment != null)
        {
            associatedSegment.skip = false;
            SetDefaultColor(); // Revert the note's visual appearance to default.
        }
    }

    /// <summary>
    /// Toggles the 'skipped' status of the associated HitSegment and updates the drum note's
    /// visual representation when the mouse clicks on this GameObject.
    /// </summary>
    void OnMouseDown()
    {
        if (associatedSegment != null)
        {
            if (associatedSegment.skip)
            {
                SetUnSkip(); // If currently skipped, unskip it.
            }
            else
            {
                SetSkip(); // If not skipped, mark it as skipped.
            }
        }
    }
}