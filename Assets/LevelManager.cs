using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [System.Serializable]
    public class Stage
    {
        public string stageName; // 阶段名称
        public List<LevelController> levels; // 存储所有关卡的列表
    }

    public List<Stage> stages;
    public RealTimeInputTracker inputTracker; // 引用 RealTimeInputTracker
    private int currentFocusedStageIndex;  // 当前 focused 的 stage 索引
    public float correctRatePassThreshold = 0.9f; // 通过的正确率阈值

    // 新增的 Action，用于在晋级到下一个 stage 时调用
    public Action OnStageAdvanced;

    private void Start()
    {
        // 确保 stages 列表不为空
        if (stages == null || stages.Count == 0)
        {
            Debug.LogError("LevelManager: Stages list is empty or null.");
            return;
        }

        // 确保 inputTracker 不为空
        if (inputTracker == null)
        {
            Debug.LogError("LevelManager: RealTimeInputTracker reference is null.");
            return;
        }

        // 订阅 RealTimeInputTracker 的事件
        inputTracker.OnFinishCalculateCorrectRate += CheckFocusedLevelsCorrectRate;

        // 初始化 currentFocusedStageIndex
        currentFocusedStageIndex = 0;

        // 初始化阶段和关卡的状态
        for (int i = 0; i < stages.Count; i++)
        {
            Stage stage = stages[i];

            if (i == currentFocusedStageIndex)
            {
                // 设置第一个 stage 的所有 level 为 focused 状态
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
                // 其他 stage 的 level 设置为 locked 状态
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

    private void CheckFocusedLevelsCorrectRate()
    {
        if (currentFocusedStageIndex >= 0 && currentFocusedStageIndex < stages.Count)
        {
            Stage currentStage = stages[currentFocusedStageIndex];

            foreach (var controller in currentStage.levels)
            {
                if (controller != null && controller.focused)
                {
                    bool isPassed = false;

                    // 根据 TrackCorrectRate 检查对应的正确率
                    switch (controller.trackCorrectRate)
                    {
                        case LevelController.TrackCorrectRate.RightHand4Beat:
                            isPassed = inputTracker.rightHandFourBeatCorrectRate >= correctRatePassThreshold;
                            break;
                        case LevelController.TrackCorrectRate.RightHand8Beat:
                            isPassed = inputTracker.rightHandEightBeatCorrectRate >= correctRatePassThreshold;
                            break;
                        case LevelController.TrackCorrectRate.RightHand16Beat:
                            isPassed = inputTracker.rightHandSixteenBeatCorrectRate >= correctRatePassThreshold;
                            break;
                        case LevelController.TrackCorrectRate.BothHand4Beat:
                            isPassed = inputTracker.bothHandFourBeatCorrectRate >= correctRatePassThreshold;
                            break;
                        case LevelController.TrackCorrectRate.BothHand8Beat:
                            isPassed = inputTracker.bothHandEightBeatCorrectRate >= correctRatePassThreshold;
                            break;
                        case LevelController.TrackCorrectRate.BothHand16Beat:
                            isPassed = inputTracker.bothHandSixteenBeatCorrectRate >= correctRatePassThreshold;
                            break;
                        case LevelController.TrackCorrectRate.RightHandRightFeet4Beat:
                            isPassed = inputTracker.rightHandRightFeetFourBeatCorrectRate >= correctRatePassThreshold;
                            break;
                        case LevelController.TrackCorrectRate.RightHandRightFeet8Beat:
                            isPassed = inputTracker.rightHandRightFeetEightBeatCorrectRate >= correctRatePassThreshold;
                            break;
                        case LevelController.TrackCorrectRate.RightHandRightFeet16Beat:
                            isPassed = inputTracker.rightHandRightFeetSixteenBeatCorrectRate >= correctRatePassThreshold;
                            break;
                        case LevelController.TrackCorrectRate.RightHandLeftHandRightFeet4Beat:
                            isPassed = inputTracker.rightHandLeftHandRightFeetFourBeatCorrectRate >= correctRatePassThreshold;
                            break;
                        case LevelController.TrackCorrectRate.RightHandLeftHandRightFeet8Beat:
                            isPassed = inputTracker.rightHandLeftHandRightFeetEightBeatCorrectRate >= correctRatePassThreshold;
                            break;
                        case LevelController.TrackCorrectRate.RightHandLeftHandRightFeet16Beat:
                            isPassed = inputTracker.rightHandLeftHandRightFeetSixteenBeatCorrectRate >= correctRatePassThreshold;
                            break;
                    }

                    // 如果超过阈值，设置为通过
                    if (isPassed)
                    {
                        controller.SetPassed();
                    }
                }
            }

            // 检查当前 focused stage 的所有 level 是否都通过
            CheckFocusedLevelPassed();
        }
    }

    private void CheckFocusedLevelPassed()
    {
        if (currentFocusedStageIndex >= 0 && currentFocusedStageIndex < stages.Count)
        {
            Stage currentStage = stages[currentFocusedStageIndex];

            // 检查所有 level 是否都通过
            bool allPassed = true;
            foreach (var controller in currentStage.levels)
            {
                if (controller != null && !controller.passed)
                {
                    allPassed = false;
                    break;
                }
            }

            if (allPassed)
            {
                // 当前 stage 所有 level 都通过了
                // 将当前 stage 的 level 设置为未聚焦
                foreach (var controller in currentStage.levels)
                {
                    if (controller != null)
                    {
                        controller.SetUnFocused();
                    }
                }

                // 聚焦下一个 stage
                currentFocusedStageIndex++;
                if (currentFocusedStageIndex < stages.Count)
                {
                    Stage nextStage = stages[currentFocusedStageIndex];
                    foreach (var controller in nextStage.levels)
                    {
                        if (controller != null)
                        {
                            controller.SetUnLocked();
                            controller.SetFocused();
                        }
                    }

                    // 将所有 drumNotes 和 hitSegments 设置为 unskipped 状态
                    inputTracker.transformPlayBacker.drumSheet.SetDrumNoteSkipStateForBeatRange(-1f, 100f, false);

                    // 在晋级到下一个 stage 时调用 Action
                    OnStageAdvanced?.Invoke();
                }
            }
        }
    }
}
