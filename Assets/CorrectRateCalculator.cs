using System;
using System.Collections.Generic;
using TMPro; // 添加 TextMeshPro 的引用
using UnityEngine;

public class CorrectRateCalculator : MonoBehaviour
{
    // 引用 RealTimeInputTracker 以獲取擊打數據
    public RealTimeInputTracker inputTracker;
    public event Action OnFinishCalculateCorrectRate;

    // 各种节奏的正确率
    public float correctRate = 0f; // 总正确率
    public float level1CorrectRate = 0f; // 总包含 level 1 错误的正确率
    public float mainRhythmCorrectRate = 0f; // 主节奏的正确率
    public float mainRhythmLevel1CorrectRate = 0f; // 主节奏包含 level 1 错误的正确率
    public float oneThreeRhythmCorrectRate = 0f; // 1和3拍节奏的正确率
    public float oneThreeRhythmLevel1CorrectRate = 0f; // 1和3拍节奏包含 level 1 错误的正确率
    public float twoFourRhythmCorrectRate = 0f; // 2和4拍节奏的正确率
    public float twoFourRhythmLevel1CorrectRate = 0f; // 2和4拍节奏包含 level 1 错误的正确率
    public float rightHandFourBeatCorrectRate = 0f; // 右手四拍节奏的正确率
    public float rightHandFourBeatLevel1CorrectRate = 0f; // 右手四拍节奏包含 level 1 错误的正确率
    public float rightHandEightBeatCorrectRate = 0f; // 右手八拍节奏的正确率
    public float rightHandEightBeatLevel1CorrectRate = 0f; // 右手八拍节奏包含 level 1 错误的正确率
    public float rightHandSixteenBeatCorrectRate = 0f; // 右手十六拍节奏的正确率
    public float rightHandSixteenBeatLevel1CorrectRate = 0f; // 右手十六拍节奏包含 level 1 错误的正确率
    public float bothHandFourBeatCorrectRate = 0f; // 双手四拍节奏的正确率
    public float bothHandFourBeatLevel1CorrectRate = 0f; // 双手四拍节奏包含 level 1 错误的正确率
    public float bothHandEightBeatCorrectRate = 0f; // 双手八拍节奏的正确率
    public float bothHandEightBeatLevel1CorrectRate = 0f; // 双手八拍节奏包含 level 1 错误的正确率
    public float bothHandSixteenBeatCorrectRate = 0f; // 双手十六拍节奏的正确率
    public float bothHandSixteenBeatLevel1CorrectRate = 0f; // 双手十六拍节奏包含 level 1 错误的正确率
    public float rightHandRightFeetFourBeatCorrectRate = 0f; // 右手右腳四拍节奏的正确率
    public float rightHandRightFeetFourBeatLevel1CorrectRate = 0f; // 右手右腳四拍节奏包含 level 1 错误的正确率
    public float rightHandRightFeetEightBeatCorrectRate = 0f; // 右手右腳八拍节奏的正确率
    public float rightHandRightFeetEightBeatLevel1CorrectRate = 0f; // 右手右腳八拍节奏包含 level 1 错误的正确率
    public float rightHandRightFeetSixteenBeatCorrectRate = 0f; // 右手右腳十六拍节奏的正确率
    public float rightHandRightFeetSixteenBeatLevel1CorrectRate = 0f; // 右手右腳十六拍节奏包含 level 1 错误的正确率

    public float rightHandLeftHandRightFeetFourBeatCorrectRate = 0f; // 右手左手右腳四拍节奏的正确率
    public float rightHandLeftHandRightFeetFourBeatLevel1CorrectRate = 0f; // 右手左手右腳四拍节奏包含 level 1 错误的正确率
    public float rightHandLeftHandRightFeetEightBeatCorrectRate = 0f; // 右手左手右腳八拍节奏的正确率
    public float rightHandLeftHandRightFeetEightBeatLevel1CorrectRate = 0f; // 右手左手右腳八拍节奏包含 level 1 错误的正确率
    public float rightHandLeftHandRightFeetSixteenBeatCorrectRate = 0f; // 右手左手右腳十六拍节奏的正确率
    public float rightHandLeftHandRightFeetSixteenBeatLevel1CorrectRate = 0f; // 右手左手右腳十六拍节奏包含 level 1 错误的正确率

    private void OnEnable()
    {
        if (inputTracker != null)
        {
            // 订阅 OnFinishTracking 事件
            inputTracker.OnFinishTracking += CalculateCorrectRates;
        }
    }

    private void OnDisable()
    {
        if (inputTracker != null)
        {
            // 取消订阅 OnFinishTracking 事件
            inputTracker.OnFinishTracking -= CalculateCorrectRates;
        }
    }

    // 计算正确率和 level 1 正确率
    public void CalculateCorrectRates()
    {
        int totalSegments = 0;
        int correctSegments = 0;
        int level1CorrectSegments = 0;

        int totalMainRhythmSegments = 0;
        int correctMainRhythmSegments = 0;
        int level1CorrectMainRhythmSegments = 0;

        int totalOneThreeRhythmSegments = 0;
        int correctOneThreeRhythmSegments = 0;
        int level1CorrectOneThreeRhythmSegments = 0;

        int totalTwoFourRhythmSegments = 0;
        int correctTwoFourRhythmSegments = 0;
        int level1CorrectTwoFourRhythmSegments = 0;

        int totalRightHandFourBeatSegments = 0;
        int correctRightHandFourBeatSegments = 0;
        int level1CorrectRightHandFourBeatSegments = 0;

        int totalRightHandEightBeatSegments = 0;
        int correctRightHandEightBeatSegments = 0;
        int level1CorrectRightHandEightBeatSegments = 0;

        int totalRightHandSixteenBeatSegments = 0;
        int correctRightHandSixteenBeatSegments = 0;
        int level1CorrectRightHandSixteenBeatSegments = 0;

        int totalBothHandFourBeatSegments = 0;
        int correctBothHandFourBeatSegments = 0;
        int level1CorrectBothHandFourBeatSegments = 0;

        int totalBothHandEightBeatSegments = 0;
        int correctBothHandEightBeatSegments = 0;
        int level1CorrectBothHandEightBeatSegments = 0;

        int totalBothHandSixteenBeatSegments = 0;
        int correctBothHandSixteenBeatSegments = 0;
        int level1CorrectBothHandSixteenBeatSegments = 0;

        int totalRightHandRightFeetFourBeatSegments = 0;
        int correctRightHandRightFeetFourBeatSegments = 0;
        int level1CorrectRightHandRightFeetFourBeatSegments = 0;

        int totalRightHandRightFeetEightBeatSegments = 0;
        int correctRightHandRightFeetEightBeatSegments = 0;
        int level1CorrectRightHandRightFeetEightBeatSegments = 0;

        int totalRightHandRightFeetSixteenBeatSegments = 0;
        int correctRightHandRightFeetSixteenBeatSegments = 0;
        int level1CorrectRightHandRightFeetSixteenBeatSegments = 0;

        int totalRightHandLeftHandRightFeetFourBeatSegments = 0;
        int correctRightHandLeftHandRightFeetFourBeatSegments = 0;
        int level1CorrectRightHandLeftHandRightFeetFourBeatSegments = 0;

        int totalRightHandLeftHandRightFeetEightBeatSegments = 0;
        int correctRightHandLeftHandRightFeetEightBeatSegments = 0;
        int level1CorrectRightHandLeftHandRightFeetEightBeatSegments = 0;

        int totalRightHandLeftHandRightFeetSixteenBeatSegments = 0;
        int correctRightHandLeftHandRightFeetSixteenBeatSegments = 0;
        int level1CorrectRightHandLeftHandRightFeetSixteenBeatSegments = 0;

        // 遍历所有跟踪的 HitSegment
        foreach (var segment in inputTracker.GetTrackedHitSegments())
        {
            if (!segment.skip)
            {
                totalSegments++;
                if (segment.correct)
                {
                    correctSegments++;
                    level1CorrectSegments++;
                }
                else if (segment.level1TimeError)
                {
                    level1CorrectSegments++;
                }

                // 计算主节奏的正确率
                if (segment.associatedNote != null && (segment.associatedNote.beatPosition == 1 || segment.associatedNote.beatPosition == 2 || 
                    segment.associatedNote.beatPosition == 3 || segment.associatedNote.beatPosition == 4))
                {
                    totalMainRhythmSegments++;
                    if (segment.correct)
                    {
                        correctMainRhythmSegments++;
                        level1CorrectMainRhythmSegments++;
                    }
                    else if (segment.level1TimeError)
                    {
                        level1CorrectMainRhythmSegments++;
                    }
                }

                // 计算 1 和 3 拍节奏的正确率
                if (segment.associatedNote != null && ((segment.associatedNote.beatPosition >= 1 && segment.associatedNote.beatPosition < 2) ||
                                                       (segment.associatedNote.beatPosition >= 3 && segment.associatedNote.beatPosition < 4)))
                {
                    totalOneThreeRhythmSegments++;
                    if (segment.correct)
                    {
                        correctOneThreeRhythmSegments++;
                        level1CorrectOneThreeRhythmSegments++;
                    }
                    else if (segment.level1TimeError)
                    {
                        level1CorrectOneThreeRhythmSegments++;
                    }
                }

                // 计算 2 和 4 拍节奏的正确率
                if (segment.associatedNote != null && ((segment.associatedNote.beatPosition >= 2 && segment.associatedNote.beatPosition < 3) ||
                                                       (segment.associatedNote.beatPosition >= 4 && segment.associatedNote.beatPosition < 5)))
                {
                    totalTwoFourRhythmSegments++;
                    if (segment.correct)
                    {
                        correctTwoFourRhythmSegments++;
                        level1CorrectTwoFourRhythmSegments++;
                    }
                    else if (segment.level1TimeError)
                    {
                        level1CorrectTwoFourRhythmSegments++;
                    }
                }
            }

            // 计算右手四拍节奏的正确率
            if (segment.limbUsed == "righthand" && segment.associatedNote != null && 
                (segment.associatedNote.beatPosition == 1 || segment.associatedNote.beatPosition == 2 || 
                 segment.associatedNote.beatPosition == 3 || segment.associatedNote.beatPosition == 4))
            {
                totalRightHandFourBeatSegments++;
                if (segment.correct)
                {
                    correctRightHandFourBeatSegments++;
                    level1CorrectRightHandFourBeatSegments++;
                }
                else if (segment.level1TimeError)
                {
                    level1CorrectRightHandFourBeatSegments++;
                }
            }

            // 计算右手八拍节奏的正确率
            if (segment.limbUsed == "righthand" && segment.associatedNote != null &&
                (segment.associatedNote.beatPosition == 1 || segment.associatedNote.beatPosition == 1.5 || 
                 segment.associatedNote.beatPosition == 2 || segment.associatedNote.beatPosition == 2.5 ||
                 segment.associatedNote.beatPosition == 3 || segment.associatedNote.beatPosition == 3.5 ||
                 segment.associatedNote.beatPosition == 4 || segment.associatedNote.beatPosition == 4.5))
            {
                totalRightHandEightBeatSegments++;
                if (segment.correct)
                {
                    correctRightHandEightBeatSegments++;
                    level1CorrectRightHandEightBeatSegments++;
                }
                else if (segment.level1TimeError)
                {
                    level1CorrectRightHandEightBeatSegments++;
                }
            }

            // 计算右手十六拍节奏的正确率
            if (segment.limbUsed == "righthand" && segment.associatedNote != null &&
                (segment.associatedNote.beatPosition == 1 || segment.associatedNote.beatPosition == 1.25 ||
                 segment.associatedNote.beatPosition == 1.5 || segment.associatedNote.beatPosition == 1.75 ||
                 segment.associatedNote.beatPosition == 2 || segment.associatedNote.beatPosition == 2.25 ||
                 segment.associatedNote.beatPosition == 2.5 || segment.associatedNote.beatPosition == 2.75 ||
                 segment.associatedNote.beatPosition == 3 || segment.associatedNote.beatPosition == 3.25 ||
                 segment.associatedNote.beatPosition == 3.5 || segment.associatedNote.beatPosition == 3.75 ||
                 segment.associatedNote.beatPosition == 4 || segment.associatedNote.beatPosition == 4.25 ||
                 segment.associatedNote.beatPosition == 4.5 || segment.associatedNote.beatPosition == 4.75))
            {
                totalRightHandSixteenBeatSegments++;
                if (segment.correct)
                {
                    correctRightHandSixteenBeatSegments++;
                    level1CorrectRightHandSixteenBeatSegments++;
                }
                else if (segment.level1TimeError)
                {
                    level1CorrectRightHandSixteenBeatSegments++;
                }
            }

            // 计算双手四拍节奏的正确率
            if ((segment.limbUsed == "righthand" || segment.limbUsed == "lefthand") && segment.associatedNote != null &&
                (segment.associatedNote.beatPosition == 1 || segment.associatedNote.beatPosition == 2 || 
                 segment.associatedNote.beatPosition == 3 || segment.associatedNote.beatPosition == 4))
            {
                totalBothHandFourBeatSegments++;
                if (segment.correct)
                {
                    correctBothHandFourBeatSegments++;
                    level1CorrectBothHandFourBeatSegments++;
                }
                else if (segment.level1TimeError)
                {
                    level1CorrectBothHandFourBeatSegments++;
                }
            }

            // 计算双手八拍节奏的正确率
            if ((segment.limbUsed == "righthand" || segment.limbUsed == "lefthand") && segment.associatedNote != null &&
                (segment.associatedNote.beatPosition == 1 || segment.associatedNote.beatPosition == 1.5 || 
                 segment.associatedNote.beatPosition == 2 || segment.associatedNote.beatPosition == 2.5 ||
                 segment.associatedNote.beatPosition == 3 || segment.associatedNote.beatPosition == 3.5 ||
                 segment.associatedNote.beatPosition == 4 || segment.associatedNote.beatPosition == 4.5))
            {
                totalBothHandEightBeatSegments++;
                if (segment.correct)
                {
                    correctBothHandEightBeatSegments++;
                    level1CorrectBothHandEightBeatSegments++;
                }
                else if (segment.level1TimeError)
                {
                    level1CorrectBothHandEightBeatSegments++;
                }
            }

            // 计算双手十六拍节奏的正确率
            if ((segment.limbUsed == "righthand" || segment.limbUsed == "lefthand") && segment.associatedNote != null &&
                (segment.associatedNote.beatPosition == 1 || segment.associatedNote.beatPosition == 1.25 ||
                 segment.associatedNote.beatPosition == 1.5 || segment.associatedNote.beatPosition == 1.75 ||
                 segment.associatedNote.beatPosition == 2 || segment.associatedNote.beatPosition == 2.25 ||
                 segment.associatedNote.beatPosition == 2.5 || segment.associatedNote.beatPosition == 2.75 ||
                 segment.associatedNote.beatPosition == 3 || segment.associatedNote.beatPosition == 3.25 ||
                 segment.associatedNote.beatPosition == 3.5 || segment.associatedNote.beatPosition == 3.75 ||
                 segment.associatedNote.beatPosition == 4 || segment.associatedNote.beatPosition == 4.25 ||
                 segment.associatedNote.beatPosition == 4.5 || segment.associatedNote.beatPosition == 4.75))
            {
                totalBothHandSixteenBeatSegments++;
                if (segment.correct)
                {
                    correctBothHandSixteenBeatSegments++;
                    level1CorrectBothHandSixteenBeatSegments++;
                }
                else if (segment.level1TimeError)
                {
                    level1CorrectBothHandSixteenBeatSegments++;
                }
            }

            // 计算右手右腳四拍节奏的正确率
            if ((segment.limbUsed == "righthand" || segment.limbUsed == "rightfeet") && segment.associatedNote != null &&
                (segment.associatedNote.beatPosition == 1 || segment.associatedNote.beatPosition == 2 || 
                 segment.associatedNote.beatPosition == 3 || segment.associatedNote.beatPosition == 4))
            {
                totalRightHandRightFeetFourBeatSegments++;
                if (segment.correct)
                {
                    correctRightHandRightFeetFourBeatSegments++;
                    level1CorrectRightHandRightFeetFourBeatSegments++;
                }
                else if (segment.level1TimeError)
                {
                    level1CorrectRightHandRightFeetFourBeatSegments++;
                }
            }

            // 计算右手右腳八拍节奏的正确率
            if ((segment.limbUsed == "righthand" || segment.limbUsed == "rightfeet") && segment.associatedNote != null &&
                (segment.associatedNote.beatPosition == 1 || segment.associatedNote.beatPosition == 1.5 || 
                 segment.associatedNote.beatPosition == 2 || segment.associatedNote.beatPosition == 2.5 ||
                 segment.associatedNote.beatPosition == 3 || segment.associatedNote.beatPosition == 3.5 ||
                 segment.associatedNote.beatPosition == 4 || segment.associatedNote.beatPosition == 4.5))
            {
                totalRightHandRightFeetEightBeatSegments++;
                if (segment.correct)
                {
                    correctRightHandRightFeetEightBeatSegments++;
                    level1CorrectRightHandRightFeetEightBeatSegments++;
                }
                else if (segment.level1TimeError)
                {
                    level1CorrectRightHandRightFeetEightBeatSegments++;
                }
            }

            // 计算右手右腳十六拍节奏的正确率
            if ((segment.limbUsed == "righthand" || segment.limbUsed == "rightfeet") && segment.associatedNote != null &&
                (segment.associatedNote.beatPosition == 1 || segment.associatedNote.beatPosition == 1.25 ||
                 segment.associatedNote.beatPosition == 1.5 || segment.associatedNote.beatPosition == 1.75 ||
                 segment.associatedNote.beatPosition == 2 || segment.associatedNote.beatPosition == 2.25 ||
                 segment.associatedNote.beatPosition == 2.5 || segment.associatedNote.beatPosition == 2.75 ||
                 segment.associatedNote.beatPosition == 3 || segment.associatedNote.beatPosition == 3.25 ||
                 segment.associatedNote.beatPosition == 3.5 || segment.associatedNote.beatPosition == 3.75 ||
                 segment.associatedNote.beatPosition == 4 || segment.associatedNote.beatPosition == 4.25 ||
                 segment.associatedNote.beatPosition == 4.5 || segment.associatedNote.beatPosition == 4.75))
            {
                totalRightHandRightFeetSixteenBeatSegments++;
                if (segment.correct)
                {
                    correctRightHandRightFeetSixteenBeatSegments++;
                    level1CorrectRightHandRightFeetSixteenBeatSegments++;
                }
                else if (segment.level1TimeError)
                {
                    level1CorrectRightHandRightFeetSixteenBeatSegments++;
                }
            }

            // 计算右手左手右腳四拍节奏的正确率
            if ((segment.limbUsed == "righthand" || segment.limbUsed == "lefthand" || segment.limbUsed == "rightfeet") && segment.associatedNote != null &&
                (segment.associatedNote.beatPosition == 1 || segment.associatedNote.beatPosition == 2 || 
                 segment.associatedNote.beatPosition == 3 || segment.associatedNote.beatPosition == 4))
            {
                totalRightHandLeftHandRightFeetFourBeatSegments++;
                if (segment.correct)
                {
                    correctRightHandLeftHandRightFeetFourBeatSegments++;
                    level1CorrectRightHandLeftHandRightFeetFourBeatSegments++;
                }
                else if (segment.level1TimeError)
                {
                    level1CorrectRightHandLeftHandRightFeetFourBeatSegments++;
                }
            }

            // 计算右手左手右腳八拍节奏的正确率
            if ((segment.limbUsed == "righthand" || segment.limbUsed == "lefthand" || segment.limbUsed == "rightfeet") && segment.associatedNote != null &&
                (segment.associatedNote.beatPosition == 1 || segment.associatedNote.beatPosition == 1.5 || 
                 segment.associatedNote.beatPosition == 2 || segment.associatedNote.beatPosition == 2.5 ||
                 segment.associatedNote.beatPosition == 3 || segment.associatedNote.beatPosition == 3.5 ||
                 segment.associatedNote.beatPosition == 4 || segment.associatedNote.beatPosition == 4.5))
            {
                totalRightHandLeftHandRightFeetEightBeatSegments++;
                if (segment.correct)
                {
                    correctRightHandLeftHandRightFeetEightBeatSegments++;
                    level1CorrectRightHandLeftHandRightFeetEightBeatSegments++;
                }
                else if (segment.level1TimeError)
                {
                    level1CorrectRightHandLeftHandRightFeetEightBeatSegments++;
                }
            }

            // 计算右手左手右腳十六拍节奏的正确率
            if ((segment.limbUsed == "righthand" || segment.limbUsed == "lefthand" || segment.limbUsed == "rightfeet") && segment.associatedNote != null &&
                (segment.associatedNote.beatPosition == 1 || segment.associatedNote.beatPosition == 1.25 ||
                 segment.associatedNote.beatPosition == 1.5 || segment.associatedNote.beatPosition == 1.75 ||
                 segment.associatedNote.beatPosition == 2 || segment.associatedNote.beatPosition == 2.25 ||
                 segment.associatedNote.beatPosition == 2.5 || segment.associatedNote.beatPosition == 2.75 ||
                 segment.associatedNote.beatPosition == 3 || segment.associatedNote.beatPosition == 3.25 ||
                 segment.associatedNote.beatPosition == 3.5 || segment.associatedNote.beatPosition == 3.75 ||
                 segment.associatedNote.beatPosition == 4 || segment.associatedNote.beatPosition == 4.25 ||
                 segment.associatedNote.beatPosition == 4.5 || segment.associatedNote.beatPosition == 4.75))
            {
                totalRightHandLeftHandRightFeetSixteenBeatSegments++;
                if (segment.correct)
                {
                    correctRightHandLeftHandRightFeetSixteenBeatSegments++;
                    level1CorrectRightHandLeftHandRightFeetSixteenBeatSegments++;
                }
                else if (segment.level1TimeError)
                {
                    level1CorrectRightHandLeftHandRightFeetSixteenBeatSegments++;
                }
            }

        }

        // 如果没有段落，默认正确率为 100%
        correctRate = (totalSegments > 0) ? (float)correctSegments / totalSegments : 1f;
        level1CorrectRate = (totalSegments > 0) ? (float)level1CorrectSegments / totalSegments : 1f;

        mainRhythmCorrectRate = (totalMainRhythmSegments > 0) ? (float)correctMainRhythmSegments / totalMainRhythmSegments : 1f;
        mainRhythmLevel1CorrectRate = (totalMainRhythmSegments > 0) ? (float)level1CorrectMainRhythmSegments / totalMainRhythmSegments : 1f;

        oneThreeRhythmCorrectRate = (totalOneThreeRhythmSegments > 0) ? (float)correctOneThreeRhythmSegments / totalOneThreeRhythmSegments : 1f;
        oneThreeRhythmLevel1CorrectRate = (totalOneThreeRhythmSegments > 0) ? (float)level1CorrectOneThreeRhythmSegments / totalOneThreeRhythmSegments : 1f;

        twoFourRhythmCorrectRate = (totalTwoFourRhythmSegments > 0) ? (float)correctTwoFourRhythmSegments / totalTwoFourRhythmSegments : 1f;
        twoFourRhythmLevel1CorrectRate = (totalTwoFourRhythmSegments > 0) ? (float)level1CorrectTwoFourRhythmSegments / totalTwoFourRhythmSegments : 1f;

        rightHandFourBeatCorrectRate = (totalRightHandFourBeatSegments > 0) ? (float)correctRightHandFourBeatSegments / totalRightHandFourBeatSegments : 1f;
        rightHandFourBeatLevel1CorrectRate = (totalRightHandFourBeatSegments > 0) ? (float)level1CorrectRightHandFourBeatSegments / totalRightHandFourBeatSegments : 1f;

        rightHandEightBeatCorrectRate = (totalRightHandEightBeatSegments > 0) ? (float)correctRightHandEightBeatSegments / totalRightHandEightBeatSegments : 1f;
        rightHandEightBeatLevel1CorrectRate = (totalRightHandEightBeatSegments > 0) ? (float)level1CorrectRightHandEightBeatSegments / totalRightHandEightBeatSegments : 1f;

        rightHandSixteenBeatCorrectRate = (totalRightHandSixteenBeatSegments > 0) ? (float)correctRightHandSixteenBeatSegments / totalRightHandSixteenBeatSegments : 1f;
        rightHandSixteenBeatLevel1CorrectRate = (totalRightHandSixteenBeatSegments > 0) ? (float)level1CorrectRightHandSixteenBeatSegments / totalRightHandSixteenBeatSegments : 1f;

        bothHandFourBeatCorrectRate = (totalBothHandFourBeatSegments > 0) ? (float)correctBothHandFourBeatSegments / totalBothHandFourBeatSegments : 1f;
        bothHandFourBeatLevel1CorrectRate = (totalBothHandFourBeatSegments > 0) ? (float)level1CorrectBothHandFourBeatSegments / totalBothHandFourBeatSegments : 1f;

        bothHandEightBeatCorrectRate = (totalBothHandEightBeatSegments > 0) ? (float)correctBothHandEightBeatSegments / totalBothHandEightBeatSegments : 1f;
        bothHandEightBeatLevel1CorrectRate = (totalBothHandEightBeatSegments > 0) ? (float)level1CorrectBothHandEightBeatSegments / totalBothHandEightBeatSegments : 1f;

        bothHandSixteenBeatCorrectRate = (totalBothHandSixteenBeatSegments > 0) ? (float)correctBothHandSixteenBeatSegments / totalBothHandSixteenBeatSegments : 1f;
        bothHandSixteenBeatLevel1CorrectRate = (totalBothHandSixteenBeatSegments > 0) ? (float)level1CorrectBothHandSixteenBeatSegments / totalBothHandSixteenBeatSegments : 1f;

        rightHandRightFeetFourBeatCorrectRate = (totalRightHandRightFeetFourBeatSegments > 0) ? (float)correctRightHandRightFeetFourBeatSegments / totalRightHandRightFeetFourBeatSegments : 1f;
        rightHandRightFeetFourBeatLevel1CorrectRate = (totalRightHandRightFeetFourBeatSegments > 0) ? (float)level1CorrectRightHandRightFeetFourBeatSegments / totalRightHandRightFeetFourBeatSegments : 1f;

        rightHandRightFeetEightBeatCorrectRate = (totalRightHandRightFeetEightBeatSegments > 0) ? (float)correctRightHandRightFeetEightBeatSegments / totalRightHandRightFeetEightBeatSegments : 1f;
        rightHandRightFeetEightBeatLevel1CorrectRate = (totalRightHandRightFeetEightBeatSegments > 0) ? (float)level1CorrectRightHandRightFeetEightBeatSegments / totalRightHandRightFeetEightBeatSegments : 1f;

        rightHandRightFeetSixteenBeatCorrectRate = (totalRightHandRightFeetSixteenBeatSegments > 0) ? (float)correctRightHandRightFeetSixteenBeatSegments / totalRightHandRightFeetSixteenBeatSegments : 1f;
        rightHandRightFeetSixteenBeatLevel1CorrectRate = (totalRightHandRightFeetSixteenBeatSegments > 0) ? (float)level1CorrectRightHandRightFeetSixteenBeatSegments / totalRightHandRightFeetSixteenBeatSegments : 1f;

        rightHandLeftHandRightFeetFourBeatCorrectRate = (totalRightHandLeftHandRightFeetFourBeatSegments > 0) ? (float)correctRightHandLeftHandRightFeetFourBeatSegments / totalRightHandLeftHandRightFeetFourBeatSegments : 1f;
        rightHandLeftHandRightFeetFourBeatLevel1CorrectRate = (totalRightHandLeftHandRightFeetFourBeatSegments > 0) ? (float)level1CorrectRightHandLeftHandRightFeetFourBeatSegments / totalRightHandLeftHandRightFeetFourBeatSegments : 1f;

        rightHandLeftHandRightFeetEightBeatCorrectRate = (totalRightHandLeftHandRightFeetEightBeatSegments > 0) ? (float)correctRightHandLeftHandRightFeetEightBeatSegments / totalRightHandLeftHandRightFeetEightBeatSegments : 1f;
        rightHandLeftHandRightFeetEightBeatLevel1CorrectRate = (totalRightHandLeftHandRightFeetEightBeatSegments > 0) ? (float)level1CorrectRightHandLeftHandRightFeetEightBeatSegments / totalRightHandLeftHandRightFeetEightBeatSegments : 1f;

        rightHandLeftHandRightFeetSixteenBeatCorrectRate = (totalRightHandLeftHandRightFeetSixteenBeatSegments > 0) ? (float)correctRightHandLeftHandRightFeetSixteenBeatSegments / totalRightHandLeftHandRightFeetSixteenBeatSegments : 1f;
        rightHandLeftHandRightFeetSixteenBeatLevel1CorrectRate = (totalRightHandLeftHandRightFeetSixteenBeatSegments > 0) ? (float)level1CorrectRightHandLeftHandRightFeetSixteenBeatSegments / totalRightHandLeftHandRightFeetSixteenBeatSegments : 1f;

        // 触发结束事件
        OnFinishCalculateCorrectRate?.Invoke();
    }
}
