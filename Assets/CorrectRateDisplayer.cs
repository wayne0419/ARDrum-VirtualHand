using System;
using System.Collections.Generic;
using TMPro; // Include TextMeshPro namespace for UI text components.
using UnityEngine;

public class CorrectRateDisplayer : MonoBehaviour
{
    // Reference to the CorrectRateCalculator, which provides the calculated accuracy rates.
    public CorrectRateCalculator correctRateCalculator;

    // TextMeshProUGUI components for displaying various accuracy rates in the UI.
    public TextMeshProUGUI correctRateText;
    public TextMeshProUGUI level1CorrectRateText;
    public TextMeshProUGUI mainRhythmCorrectRateText;
    public TextMeshProUGUI mainRhythmLevel1CorrectRateText;
    public TextMeshProUGUI oneThreeRhythmCorrectRateText;
    public TextMeshProUGUI oneThreeRhythmLevel1CorrectRateText;
    public TextMeshProUGUI twoFourRhythmCorrectRateText;
    public TextMeshProUGUI twoFourRhythmLevel1CorrectRateText;
    public TextMeshProUGUI rightHandFourBeatCorrectRateText;
    public TextMeshProUGUI rightHandFourBeatLevel1CorrectRateText;
    public TextMeshProUGUI rightHandEightBeatCorrectRateText;
    public TextMeshProUGUI rightHandEightBeatLevel1CorrectRateText;
    public TextMeshProUGUI rightHandSixteenBeatCorrectRateText;
    public TextMeshProUGUI rightHandSixteenBeatLevel1CorrectRateText;
    public TextMeshProUGUI bothHandFourBeatCorrectRateText;
    public TextMeshProUGUI bothHandFourBeatLevel1CorrectRateText;
    public TextMeshProUGUI bothHandEightBeatCorrectRateText;
    public TextMeshProUGUI bothHandEightBeatLevel1CorrectRateText;
    public TextMeshProUGUI bothHandSixteenBeatCorrectRateText;
    public TextMeshProUGUI bothHandSixteenBeatLevel1CorrectRateText;
    public TextMeshProUGUI rightHandRightFeetFourBeatCorrectRateText;
    public TextMeshProUGUI rightHandRightFeetFourBeatLevel1CorrectRateText;
    public TextMeshProUGUI rightHandRightFeetEightBeatCorrectRateText;
    public TextMeshProUGUI rightHandRightFeetEightBeatLevel1CorrectRateText;
    public TextMeshProUGUI rightHandRightFeetSixteenBeatCorrectRateText;
    public TextMeshProUGUI rightHandRightFeetSixteenBeatLevel1CorrectRateText;
    public TextMeshProUGUI rightHandLeftHandRightFeetFourBeatCorrectRateText;
    public TextMeshProUGUI rightHandLeftHandRightFeetFourBeatLevel1CorrectRateText;
    public TextMeshProUGUI rightHandLeftHandRightFeetEightBeatCorrectRateText;
    public TextMeshProUGUI rightHandLeftHandRightFeetEightBeatLevel1CorrectRateText;
    public TextMeshProUGUI rightHandLeftHandRightFeetSixteenBeatCorrectRateText;
    public TextMeshProUGUI rightHandLeftHandRightFeetSixteenBeatLevel1CorrectRateText;

    private void OnEnable()
    {
        if (correctRateCalculator != null)
        {
            // Subscribe to the OnFinishCalculateCorrectRate event to update display when calculations are complete.
            correctRateCalculator.OnFinishCalculateCorrectRate += UpdateCorrectRateDisplay;
        }
    }

    private void OnDisable()
    {
        if (correctRateCalculator != null)
        {
            // Unsubscribe from the OnFinishCalculateCorrectRate event to prevent memory leaks and ensure proper cleanup.
            correctRateCalculator.OnFinishCalculateCorrectRate -= UpdateCorrectRateDisplay;
        }
    }

    /// <summary>
    /// Updates all TextMeshProUGUI fields with the latest accuracy rates retrieved from the CorrectRateCalculator.
    /// Rates are formatted as percentages.
    /// </summary>
    private void UpdateCorrectRateDisplay()
    {
        if (correctRateText != null)
        {
            correctRateText.text = $"{correctRateCalculator.correctRate:P0} \nCorrect";
        }

        if (level1CorrectRateText != null)
        {
            level1CorrectRateText.text = $"Level 1 Correct Rate: {correctRateCalculator.level1CorrectRate:P0}";
        }

        if (mainRhythmCorrectRateText != null)
        {
            mainRhythmCorrectRateText.text = $"Main Rhythm Correct Rate: {correctRateCalculator.mainRhythmCorrectRate:P0}";
        }

        if (mainRhythmLevel1CorrectRateText != null)
        {
            mainRhythmLevel1CorrectRateText.text = $"Main Rhythm Level 1 Correct Rate: {correctRateCalculator.mainRhythmLevel1CorrectRate:P0}";
        }

        if (oneThreeRhythmCorrectRateText != null)
        {
            oneThreeRhythmCorrectRateText.text = $"1 & 3 Rhythm Correct Rate: {correctRateCalculator.oneThreeRhythmCorrectRate:P0}";
        }

        if (oneThreeRhythmLevel1CorrectRateText != null)
        {
            oneThreeRhythmLevel1CorrectRateText.text = $"1 & 3 Rhythm Level 1 Correct Rate: {correctRateCalculator.oneThreeRhythmLevel1CorrectRate:P0}";
        }

        if (twoFourRhythmCorrectRateText != null)
        {
            twoFourRhythmCorrectRateText.text = $"2 & 4 Rhythm Correct Rate: {correctRateCalculator.twoFourRhythmCorrectRate:P0}";
        }

        if (twoFourRhythmLevel1CorrectRateText != null)
        {
            twoFourRhythmLevel1CorrectRateText.text = $"2 & 4 Rhythm Level 1 Correct Rate: {correctRateCalculator.twoFourRhythmLevel1CorrectRate:P0}";
        }

        if (rightHandFourBeatCorrectRateText != null)
        {
            rightHandFourBeatCorrectRateText.text = $"{correctRateCalculator.rightHandFourBeatCorrectRate:P0}";
        }

        if (rightHandFourBeatLevel1CorrectRateText != null)
        {
            rightHandFourBeatLevel1CorrectRateText.text = $"{correctRateCalculator.rightHandFourBeatLevel1CorrectRate:P0}";
        }

        if (rightHandEightBeatCorrectRateText != null)
        {
            rightHandEightBeatCorrectRateText.text = $"{correctRateCalculator.rightHandEightBeatCorrectRate:P0}";
        }

        if (rightHandEightBeatLevel1CorrectRateText != null)
        {
            rightHandEightBeatLevel1CorrectRateText.text = $"{correctRateCalculator.rightHandEightBeatLevel1CorrectRate:P0}";
        }

        if (rightHandSixteenBeatCorrectRateText != null)
        {
            rightHandSixteenBeatCorrectRateText.text = $"{correctRateCalculator.rightHandSixteenBeatCorrectRate:P0}";
        }

        if (rightHandSixteenBeatLevel1CorrectRateText != null)
        {
            rightHandSixteenBeatLevel1CorrectRateText.text = $"{correctRateCalculator.rightHandSixteenBeatLevel1CorrectRate:P0}";
        }

        if (bothHandFourBeatCorrectRateText != null)
        {
            bothHandFourBeatCorrectRateText.text = $"{correctRateCalculator.bothHandFourBeatCorrectRate:P0}";
        }

        if (bothHandFourBeatLevel1CorrectRateText != null)
        {
            bothHandFourBeatLevel1CorrectRateText.text = $"{correctRateCalculator.bothHandFourBeatLevel1CorrectRate:P0}";
        }

        if (bothHandEightBeatCorrectRateText != null)
        {
            bothHandEightBeatCorrectRateText.text = $"{correctRateCalculator.bothHandEightBeatCorrectRate:P0}";
        }

        if (bothHandEightBeatLevel1CorrectRateText != null)
        {
            bothHandEightBeatLevel1CorrectRateText.text = $"{correctRateCalculator.bothHandEightBeatLevel1CorrectRate:P0}";
        }

        if (bothHandSixteenBeatCorrectRateText != null)
        {
            bothHandSixteenBeatCorrectRateText.text = $"{correctRateCalculator.bothHandSixteenBeatCorrectRate:P0}";
        }

        if (bothHandSixteenBeatLevel1CorrectRateText != null)
        {
            bothHandSixteenBeatLevel1CorrectRateText.text = $"{correctRateCalculator.bothHandSixteenBeatLevel1CorrectRate:P0}";
        }

        if (rightHandRightFeetFourBeatCorrectRateText != null)
        {
            rightHandRightFeetFourBeatCorrectRateText.text = $"{correctRateCalculator.rightHandRightFeetFourBeatCorrectRate:P0}";
        }

        if (rightHandRightFeetFourBeatLevel1CorrectRateText != null)
        {
            rightHandRightFeetFourBeatLevel1CorrectRateText.text = $"{correctRateCalculator.rightHandRightFeetFourBeatLevel1CorrectRate:P0}";
        }

        if (rightHandRightFeetEightBeatCorrectRateText != null)
        {
            rightHandRightFeetEightBeatCorrectRateText.text = $"{correctRateCalculator.rightHandRightFeetEightBeatCorrectRate:P0}";
        }

        if (rightHandRightFeetEightBeatLevel1CorrectRateText != null)
        {
            rightHandRightFeetEightBeatLevel1CorrectRateText.text = $"{correctRateCalculator.rightHandRightFeetEightBeatLevel1CorrectRate:P0}";
        }

        if  (rightHandRightFeetSixteenBeatCorrectRateText != null)
        {
            rightHandRightFeetSixteenBeatCorrectRateText.text = $"{correctRateCalculator.rightHandRightFeetSixteenBeatCorrectRate:P0}";
        }

        if (rightHandRightFeetSixteenBeatLevel1CorrectRateText != null)
        {
            rightHandRightFeetSixteenBeatLevel1CorrectRateText.text = $"{correctRateCalculator.rightHandRightFeetSixteenBeatLevel1CorrectRate:P0}";
        }

        if (rightHandLeftHandRightFeetFourBeatCorrectRateText != null)
        {
            rightHandLeftHandRightFeetFourBeatCorrectRateText.text = $"{correctRateCalculator.rightHandLeftHandRightFeetFourBeatCorrectRate:P0}";
        }

        if (rightHandLeftHandRightFeetFourBeatLevel1CorrectRateText != null)
        {
            rightHandLeftHandRightFeetFourBeatLevel1CorrectRateText.text = $"{correctRateCalculator.rightHandLeftHandRightFeetFourBeatLevel1CorrectRate:P0}";
        }

        if (rightHandLeftHandRightFeetEightBeatCorrectRateText != null)
        {
            rightHandLeftHandRightFeetEightBeatCorrectRateText.text = $"{correctRateCalculator.rightHandLeftHandRightFeetEightBeatCorrectRate:P0}";
        }

        if (rightHandLeftHandRightFeetEightBeatLevel1CorrectRateText != null)
        {
            rightHandLeftHandRightFeetEightBeatLevel1CorrectRateText.text = $"{correctRateCalculator.rightHandLeftHandRightFeetEightBeatLevel1CorrectRate:P0}";
        }

        if  (rightHandLeftHandRightFeetSixteenBeatCorrectRateText != null)
        {
            rightHandLeftHandRightFeetSixteenBeatCorrectRateText.text = $"{correctRateCalculator.rightHandLeftHandRightFeetSixteenBeatCorrectRate:P0}";
        }

        if (rightHandLeftHandRightFeetSixteenBeatLevel1CorrectRateText != null)
        {
            rightHandLeftHandRightFeetSixteenBeatLevel1CorrectRateText.text = $"{correctRateCalculator.rightHandLeftHandRightFeetSixteenBeatLevel1CorrectRate:P0}";
        }
    }
}