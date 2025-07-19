using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Required for TextMeshProUGUI.

public class LevelManagementAnnouncement : MonoBehaviour
{
    public LevelManager levelManager; // Reference to the LevelManager responsible for level progression.
    public TextMeshProUGUI levelPassedText; // TextMeshProUGUI component to display the "Level Passed" announcement.

    void OnEnable()
    {
        // Initially hide the level passed text when the script becomes active.
        if (levelPassedText != null)
        {
            levelPassedText.gameObject.SetActive(false);
        }

        // Subscribe to the OnLevelPassed event from the LevelManager.
        if (levelManager != null)
        {
            levelManager.OnLevelPassed += AnnounceLevelPassed;
        }
    }

    void OnDisable() // Corrected method name from DisEnable to OnDisable for Unity lifecycle.
    {
        // Unsubscribe from the OnLevelPassed event to prevent memory leaks.
        if (levelManager != null)
        {
            levelManager.OnLevelPassed -= AnnounceLevelPassed;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // This Update method is currently empty, implying no continuous logic is needed here.
    }

    /// <summary>
    /// Callback method triggered when a level is passed. It starts the visual announcement coroutine.
    /// </summary>
    void AnnounceLevelPassed()
    {
        StartCoroutine(AnnounceLevelPassedCoroutine());
    }

    /// <summary>
    /// Coroutine to visually announce that a level has been passed by making the text flash.
    /// The text will toggle visibility multiple times before finally disappearing.
    /// </summary>
    IEnumerator AnnounceLevelPassedCoroutine()
    {
        // Flash the "Level Passed" text on and off.
        levelPassedText.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        levelPassedText.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.3f);
        levelPassedText.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        levelPassedText.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.3f);
        levelPassedText.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f); // Longer display for the final flash.
        levelPassedText.gameObject.SetActive(false); // Ensure it's off at the end.
    }
}