using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [System.Serializable]
    public class Level
    {
        public string levelName; 
        public LevelController levelController;
    }

    [System.Serializable]
    public class Stage
    {
        public string stageName;
        public List<Level> levels;
    }

    public List<Stage> stages;
    public RealTimeInputTracker inputTracker; // 引用 RealTimeInputTracker
    private int currentFocusedStageIndex;  // 当前 focused 的 stage 索引

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
                foreach (var level in stage.levels)
                {
                    if (level.levelController != null)
                    {
                        level.levelController.SetFocused();
                    }
                }
            }
            else
            {
                // 其他 stage 的 level 设置为 locked 状态
                foreach (var level in stage.levels)
                {
                    if (level.levelController != null)
                    {
                        level.levelController.SetLocked();
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
            bool allLevelsPassed = true; // 标记当前阶段的所有 level 是否都通过

            foreach (var level in currentStage.levels)
            {
                LevelController controller = level.levelController;
                
                if (controller != null && controller.focused)
                {
                    bool isPassed = false;

                    // 根据 TrackCorrectRate 检查对应的正确率
                    switch (controller.trackCorrectRate)
                    {
                        case LevelController.TrackCorrectRate.RightHand4Beat:
                            isPassed = inputTracker.rightHandFourBeatCorrectRate > 0.9f;
                            break;
                        case LevelController.TrackCorrectRate.RightHand8Beat:
                            isPassed = inputTracker.rightHandEightBeatCorrectRate > 0.9f;
                            break;
                        case LevelController.TrackCorrectRate.RightHand16Beat:
                            isPassed = inputTracker.rightHandSixteenBeatCorrectRate > 0.9f;
                            break;
                        case LevelController.TrackCorrectRate.BothHand4Beat:
                            isPassed = inputTracker.bothHandFourBeatCorrectRate > 0.9f;
                            break;
                        case LevelController.TrackCorrectRate.BothHand8Beat:
                            isPassed = inputTracker.bothHandEightBeatCorrectRate > 0.9f;
                            break;
                        case LevelController.TrackCorrectRate.BothHand16Beat:
                            isPassed = inputTracker.bothHandSixteenBeatCorrectRate > 0.9f;
                            break;
                        case LevelController.TrackCorrectRate.RightHandRightFeet4Beat:
                            isPassed = inputTracker.rightHandRightFeetFourBeatCorrectRate > 0.9f;
                            break;
                        case LevelController.TrackCorrectRate.RightHandRightFeet8Beat:
                            isPassed = inputTracker.rightHandRightFeetEightBeatCorrectRate > 0.9f;
                            break;
                        case LevelController.TrackCorrectRate.RightHandRightFeet16Beat:
                            isPassed = inputTracker.rightHandRightFeetSixteenBeatCorrectRate > 0.9f;
                            break;
                        case LevelController.TrackCorrectRate.RightHandLeftHandRightFeet4Beat:
                            isPassed = inputTracker.rightHandLeftHandRightFeetFourBeatCorrectRate > 0.9f;
                            break;
                        case LevelController.TrackCorrectRate.RightHandLeftHandRightFeet8Beat:
                            isPassed = inputTracker.rightHandLeftHandRightFeetEightBeatCorrectRate > 0.9f;
                            break;
                        case LevelController.TrackCorrectRate.RightHandLeftHandRightFeet16Beat:
                            isPassed = inputTracker.rightHandLeftHandRightFeetSixteenBeatCorrectRate > 0.9f;
                            break;
                    }

                    // 如果超过阈值，设置为通过
                    if (isPassed)
                    {
                        controller.SetPassed();
                    }
                    else
                    {
                        allLevelsPassed = false; // 只要有一个 level 没有通过，设置为 false
                    }
                }
            }

            // 如果当前阶段的所有 level 都通过了
            if (allLevelsPassed && currentFocusedStageIndex < stages.Count - 1)
            {
                // 将当前阶段的所有 level 设为 unfocused
                foreach (var level in currentStage.levels)
                {
                    if (level.levelController != null)
                    {
                        level.levelController.SetUnFocused();
                    }
                }

                // 设定下一个阶段的所有 level 为 unlocked 和 focused
                currentFocusedStageIndex++;
                Stage nextStage = stages[currentFocusedStageIndex];

                foreach (var level in nextStage.levels)
                {
                    if (level.levelController != null)
                    {
                        level.levelController.SetUnLocked();
                        level.levelController.SetFocused();
                    }
                }
            }
        }
    }
}
