using UnityEngine;

public class LevelController : MonoBehaviour
{
    public Color lockedColor = Color.gray;      // Color applied when the level is locked.
    public Color focusedColor = Color.yellow;   // Color applied when the level is focused (selected).
    public Color unfocusedColor = Color.white;  // Color applied when the level is not focused.
    public Color passedColor = Color.green;     // Color applied when the level has been successfully passed.
    public bool focused = false;                // Indicates if this level is currently focused.
    public bool locked = false;                 // Indicates if this level is currently locked.
    public bool passed = false;                 // Indicates if this level has been passed.
    private Renderer levelRenderer;             // The Renderer component of the level's GameObject.

    /// <summary>
    /// Defines the specific type of accuracy rate this level tracks.
    /// This enum allows different levels to be associated with different performance metrics.
    /// </summary>
    public enum TrackCorrectRate
    {
        RightHand4Beat,
        RightHand8Beat,
        RightHand16Beat,
        BothHand4Beat,
        BothHand8Beat,
        BothHand16Beat,
        RightHandRightFeet4Beat,
        RightHandRightFeet8Beat,
        RightHandRightFeet16Beat,
        RightHandLeftHandRightFeet4Beat,
        RightHandLeftHandRightFeet8Beat,
        RightHandLeftHandRightFeet16Beat
    }

    public TrackCorrectRate trackCorrectRate;   // The specific accuracy rate type this level is designed to track.

    private void Awake()
    {
        levelRenderer = GetComponent<Renderer>();
        if (levelRenderer == null)
        {
            Debug.LogError("LevelController requires a Renderer component on its GameObject.");
        }
    }

    /// <summary>
    /// Sets the level to a locked state, visually represented by the lockedColor.
    /// </summary>
    public void SetLocked()
    {
        locked = true;
        if (levelRenderer != null)
        {
            levelRenderer.material.color = lockedColor;
        }
    }

    /// <summary>
    /// Sets the level to an unlocked state. Note: This method does not change the visual color,
    /// as the visual state is typically managed by SetFocused/SetUnFocused or SetPassed.
    /// </summary>
    public void SetUnLocked()
    {
        locked = false;
    }

    /// <summary>
    /// Sets the level to a focused state, visually represented by the focusedColor.
    /// </summary>
    public void SetFocused()
    {
        focused = true;
        if (levelRenderer != null)
        {
            levelRenderer.material.color = focusedColor;
        }
    }

    /// <summary>
    /// Sets the level to an unfocused state, visually represented by the unfocusedColor.
    /// </summary>
    public void SetUnFocused()
    {
        focused = false;
        if (levelRenderer != null)
        {
            levelRenderer.material.color = unfocusedColor;
        }
    }

    /// <summary>
    /// Sets the level to a passed state, visually represented by the passedColor.
    /// </summary>
    public void SetPassed()
    {
        passed = true;
        if (levelRenderer != null)
        {
            levelRenderer.material.color = passedColor; // Apply the passed color.
        }
    }

    /// <summary>
    /// Sets the level to an unpassed state. Note: This method does not change the visual color,
    /// as the visual state is typically managed by SetFocused/SetUnFocused or SetLocked.
    /// </summary>
    public void SetUnPassed()
    {
        passed = false;
    }

    /// <summary>
    /// Placeholder method to set the level's appearance based on an arbitrary integer value.
    /// The specific logic for how 'value' affects appearance would be implemented here.
    /// </summary>
    /// <param name="value">An integer value to influence the level's appearance.</param>
    public void SetAppearanceByValue(int value)
    {
        // Implement logic to change appearance based on 'value' here.
    }
}