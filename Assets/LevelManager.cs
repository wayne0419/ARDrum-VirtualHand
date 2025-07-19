using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{
    [System.Serializable]
    public class Stage
    {
        public string stageName; // The name of this stage.
        public List<LevelController> levels; // A list of all LevelControllers belonging to this stage.
    }

    public List<Stage> stages; // A list of all stages in the game.
    public CorrectRateCalculator correctRateCalculator; // Reference to the CorrectRateCalculator for checking performance.
    private int currentFocusedStageIndex;  // The index of the currently focused (active) stage.
    public float correctRatePassThreshold = 0.9f; // The minimum accuracy rate required to pass a level (e.g., 0.9 for 90%).
    public float requiredBPM; // The minimum BPM (Beats Per Minute) required to pass levels in the current stage.
    public TextMeshProUGUI requiredBPMText; // TextMeshProUGUI component to display the required BPM.

    // Action event triggered when the player advances to the next stage.
    public Action OnStageAdvanced;
    // Action event triggered when a single level is passed.
    public Action OnLevelPassed;

    private void Start()
    {
        // Validate that the stages list is populated.
        if (stages == null || stages.Count == 0)
        {
            Debug.LogError("LevelManager: Stages list is empty or null. Please populate it in the Inspector.");
            return;
        }

        // Validate that the CorrectRateCalculator is assigned.
        if (correctRateCalculator == null)
        {
            Debug.LogError("LevelManager: CorrectRateCalculator reference is null. Please assign it in the Inspector.");
            return;
        }
        
        // Initialize the current focused stage to the first one.
        currentFocusedStageIndex = 0;

        // Set the initial state for all stages and their levels.
        for (int i = 0; i < stages.Count; i++)
        {
            Stage stage = stages[i];

            if (i == currentFocusedStageIndex)
            {
                // Set all levels in the initial focused stage to 'focused' state.
                foreach (var levelController in stage.levels)
                {
                    if (levelController != null)
                    {
                        levelController.SetFocused();
                    }
                }
            }
            else
            {
                // Set all levels in other stages to 'locked' state.
                foreach (var levelController in stage.levels)
                {
                    if (levelController != null)
                    {
                        levelController.SetLocked();
                    }
                }
            }
        }
    }

    void OnEnable()
    {
        // Subscribe to the OnFinishCalculateCorrectRate event from CorrectRateCalculator
        // to automatically check level pass conditions after accuracy is calculated.
        correctRateCalculator.OnFinishCalculateCorrectRate += CheckFocusedLevelsCorrectRate;

        // Initialize the displayed required BPM text.
        UpdateRequiredBPMText();
    }

    void OnDisable()
    {
        // Unsubscribe from the event to prevent memory leaks.
        correctRateCalculator.OnFinishCalculateCorrectRate -= CheckFocusedLevelsCorrectRate;
    }

    void Update()
    {
        // Handle input for adjusting BPM and navigating stages for testing/debugging.
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            requiredBPM += 5f;
            UpdateRequiredBPMText();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            requiredBPM -= 5f;
            UpdateRequiredBPMText();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            GoNextStage();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            GoPreviousStage();
        }
    }

    /// <summary>
    /// Updates the UI text displaying the required BPM.
    /// </summary>
    void UpdateRequiredBPMText()
    {
        if (requiredBPMText != null)
        {
            requiredBPMText.text = requiredBPM.ToString();
        }
    }

    /// <summary>
    /// Checks the accuracy rates of the currently focused levels against their pass thresholds.
    /// This method is called after the CorrectRateCalculator finishes its calculations.
    /// </summary>
    private void CheckFocusedLevelsCorrectRate()
    {
        // Levels cannot be passed if the current mode is "CorrectOrderMode".
        if (correctRateCalculator.inputTracker.currentMode == RealTimeInputTracker.CorrectMode.CorrectOrderMode)
        {
            return;
        }
        // Levels cannot be passed if the current playback BPM is below the required BPM.
        if (correctRateCalculator.inputTracker.transformPlayBacker.playBackBPM < requiredBPM)
        {
            return;
        }

        // Ensure there is a focused stage to check.
        if (currentFocusedStageIndex >= 0 && currentFocusedStageIndex < stages.Count)
        {
            Stage currentStage = stages[currentFocusedStageIndex];

            // Iterate through all levels in the current stage.
            foreach (var controller in currentStage.levels)
            {
                // Only check levels that are currently focused.
                if (controller != null && controller.focused)
                {
                    bool isPassed = false;

                    // Use a switch statement to check the correct rate based on the level's configured TrackCorrectRate type.
                    switch (controller.trackCorrectRate)
                    {
                        case LevelController.TrackCorrectRate.RightHand4Beat:
                            isPassed = correctRateCalculator.rightHandFourBeatCorrectRate >= correctRatePassThreshold;
                            break;
                        case LevelController.TrackCorrectRate.RightHand8Beat:
                            isPassed = correctRateCalculator.rightHandEightBeatCorrectRate >= correctRatePassThreshold;
                            break;
                        case LevelController.TrackCorrectRate.RightHand16Beat:
                            isPassed = correctRateCalculator.rightHandSixteenBeatCorrectRate >= correctRatePassThreshold;
                            break;
                        case LevelController.TrackCorrectRate.BothHand4Beat:
                            isPassed = correctRateCalculator.bothHandFourBeatCorrectRate >= correctRatePassThreshold;
                            break;
                        case LevelController.TrackCorrectRate.BothHand8Beat:
                            isPassed = correctRateCalculator.bothHandEightBeatCorrectRate >= correctRatePassThreshold;
                            break;
                        case LevelController.TrackCorrectRate.BothHand16Beat:
                            isPassed = correctRateCalculator.bothHandSixteenBeatCorrectRate >= correctRatePassThreshold;
                            break;
                        case LevelController.TrackCorrectRate.RightHandRightFeet4Beat:
                            isPassed = correctRateCalculator.rightHandRightFeetFourBeatCorrectRate >= correctRatePassThreshold;
                            break;
                        case LevelController.TrackCorrectRate.RightHandRightFeet8Beat:
                            isPassed = correctRateCalculator.rightHandRightFeetEightBeatCorrectRate >= correctRatePassThreshold;
                            break;
                        case LevelController.TrackCorrectRate.RightHandRightFeet16Beat:
                            isPassed = correctRateCalculator.rightHandRightFeetSixteenBeatCorrectRate >= correctRatePassThreshold;
                            break;
                        case LevelController.TrackCorrectRate.RightHandLeftHandRightFeet4Beat:
                            isPassed = correctRateCalculator.rightHandLeftHandRightFeetFourBeatCorrectRate >= correctRatePassThreshold;
                            break;
                        case LevelController.TrackCorrectRate.RightHandLeftHandRightFeet8Beat:
                            isPassed = correctRateCalculator.rightHandLeftHandRightFeetEightBeatCorrectRate >= correctRatePassThreshold;
                            break;
                        case LevelController.TrackCorrectRate.RightHandLeftHandRightFeet16Beat:
                            isPassed = correctRateCalculator.rightHandLeftHandRightFeetSixteenBeatCorrectRate >= correctRatePassThreshold;
                            break;
                    }

                    // If the level's pass condition is met, mark it as passed and trigger the OnLevelPassed event.
                    if (isPassed)
                    {
                        controller.SetPassed();
                        OnLevelPassed?.Invoke();
                    }
                }
            }

            // After checking all individual levels, check if the entire focused stage has been passed.
            CheckFocusedLevelPassed();
        }
    }

    /// <summary>
    /// Checks if all levels within the currently focused stage have been passed.
    /// If so, it triggers advancement to the next stage.
    /// </summary>
    private void CheckFocusedLevelPassed()
    {
        if (currentFocusedStageIndex >= 0 && currentFocusedStageIndex < stages.Count)
        {
            Stage currentStage = stages[currentFocusedStageIndex];

            bool allPassed = true;
            foreach (var controller in currentStage.levels)
            {
                if (controller != null && !controller.passed)
                {
                    allPassed = false; // If any level is not passed, the stage is not complete.
                    break;
                }
            }

            if (allPassed)
            {
                GoNextStage(); // Advance to the next stage if all levels are passed.
            }
        }
    }

    /// <summary>
    /// Transitions the game to the next stage.
    /// Unfocuses the current stage's levels and focuses the next stage's levels.
    /// Also triggers the OnStageAdvanced event.
    /// </summary>
    void GoNextStage()
    {
        // Ensure there is a next stage to go to.
        if (currentFocusedStageIndex >= 0 && currentFocusedStageIndex < stages.Count - 1)
        {
            Stage currentStage = stages[currentFocusedStageIndex];

            // Unfocus all levels in the current stage.
            foreach (var controller in currentStage.levels)
            {
                if (controller != null)
                {
                    controller.SetUnFocused();
                }
            }

            // Advance to the next stage index.
            currentFocusedStageIndex++;
            if (currentFocusedStageIndex < stages.Count)
            {
                Stage nextStage = stages[currentFocusedStageIndex];
                // Unlock and focus all levels in the new stage.
                foreach (var controller in nextStage.levels)
                {
                    if (controller != null)
                    {
                        controller.SetUnLocked();
                        controller.SetFocused();
                        // Automatically trigger the associated skip state and correct mode buttons for the new level.
                        controller.GetComponent<SetDrumNoteSkipStateButton>()?.OnMouseDown();
                        controller.GetComponent<SetHitDrumCorrectMode>()?.OnMouseDown();
                    }
                }

                // Reset all drum notes and hit segments to unskipped state for the new stage.
                // This ensures a clean slate for the new level's tracking.
                // correctRateCalculator.inputTracker.transformPlayBacker.drumSheet.SetDrumNoteSkipStateForBeatRange(-1f, 100f, false);

                // Invoke the OnStageAdvanced event.
                OnStageAdvanced?.Invoke();
            }
        }
    }

    /// <summary>
    /// Transitions the game to the previous stage.
    /// Unpasses, unfocuses, and locks the current stage's levels, then focuses the previous stage's levels.
    /// Also triggers the OnStageAdvanced event.
    /// </summary>
    void GoPreviousStage()
    {
        // Ensure there is a previous stage to go to.
        if (currentFocusedStageIndex > 0 && currentFocusedStageIndex < stages.Count)
        {
            Stage currentStage = stages[currentFocusedStageIndex];

            // Unpass, unfocus, and lock all levels in the current stage.
            foreach (var controller in currentStage.levels)
            {
                if (controller != null)
                {
                    controller.SetUnPassed();
                    controller.SetUnFocused();
                    controller.SetLocked();
                }
            }

            // Revert to the previous stage index.
            currentFocusedStageIndex--;
            if (currentFocusedStageIndex >= 0)
            {
                Stage previousStage = stages[currentFocusedStageIndex];
                // Unlock and focus all levels in the previous stage.
                foreach (var controller in previousStage.levels)
                {
                    if (controller != null)
                    {
                        controller.SetUnLocked();
                        controller.SetFocused();
                    }
                }

                // Reset all drum notes and hit segments to unskipped state for the previous stage.
                correctRateCalculator.inputTracker.transformPlayBacker.drumSheet.SetDrumNoteSkipStateForBeatRange(-1f, 100f, false);

                // Invoke the OnStageAdvanced event.
                OnStageAdvanced?.Invoke();
            }
        }
    }
}