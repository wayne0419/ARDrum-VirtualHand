using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelManager : MonoBehaviour
{
    [System.Serializable]
    public class Stage
    {
        public string stageName; // 阶段名称
        public List<LevelController> levels; // 存储所有关卡的列表
    }

    public List<Stage> stages;
    public CorrectRateCalculator correctRateCalculator; // 引用 CorrectRateCalculator
    private int currentFocusedStageIndex;  // 当前 focused 的 stage 索引
    public float correctRatePassThreshold = 0.9f; // 通过的正确率阈值
    public float requiredBPM; // 需要达到的 BPM
    public TextMeshProUGUI requiredBPMText; // 显示需要达到的 BPM 的 Text

    // 新增的 Action，用于在晋级到下一个 stage 时调用
    public Action OnStageAdvanced;
    // 新增的 Action，用于在 levle pass 时调用
    public Action OnLevelPassed;

    private void Start()
    {
        // 确保 stages 列表不为空
        if (stages == null || stages.Count == 0)
        {
            Debug.LogError("LevelManager: Stages list is empty or null.");
            return;
        }

        // 确保 inputTracker 不为空
        if (correctRateCalculator == null)
        {
            Debug.LogError("LevelManager: RealTimeInputTracker reference is null.");
            return;
        }

        

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

    void OnEnable() {
        // 订阅 RealTimeInputTracker 的事件
        correctRateCalculator.OnFinishCalculateCorrectRate += CheckFocusedLevelsCorrectRate;

        // 初始化 requiredBPMText
        UpdateRequiredBPMText();
    }
    void OnDisable() {
        // 取消订阅 RealTimeInputTracker 的事件
        correctRateCalculator.OnFinishCalculateCorrectRate -= CheckFocusedLevelsCorrectRate;
    }
    void Update() {
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            requiredBPM += 5f;
            UpdateRequiredBPMText();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow)) {
            requiredBPM -= 5f;
            UpdateRequiredBPMText();
        }
        
    }
    void UpdateRequiredBPMText() {
        // 更新 requiredBPMText
        if (requiredBPMText != null) {
            requiredBPMText.text = requiredBPM.ToString();
        }
    }

    private void CheckFocusedLevelsCorrectRate()
    {
        // CorrectOrderMode 不能用來通關
        if (correctRateCalculator.inputTracker.currentMode == RealTimeInputTracker.CorrectMode.CorrectOrderMode) {
            return;
        }
        // bpm 小於 required bpm 不能通關
        if (correctRateCalculator.inputTracker.transformPlayBacker.playBackBPM < requiredBPM) {
            return;
        }

        // 检查当前聚焦的 stage 的所有 level 是否都通过
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

                    // 如果超过阈值，设置为通过
                    if (isPassed)
                    {
                        controller.SetPassed();
                        OnLevelPassed?.Invoke();
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
                    correctRateCalculator.inputTracker.transformPlayBacker.drumSheet.SetDrumNoteSkipStateForBeatRange(-1f, 100f, false);

                    // 在晋级到下一个 stage 时调用 Action
                    OnStageAdvanced?.Invoke();
                }
            }
        }
    }
}
