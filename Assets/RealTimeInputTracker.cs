using System;
using System.Collections.Generic;
using TMPro; // 添加 TextMeshPro 的引用
using UnityEngine;
using UnityEngine.InputSystem;

public class RealTimeInputTracker : MonoBehaviour
{
    // 引用 TransformPlayBacker，用于检测播放状态
    public TransformPlayBacker transformPlayBacker;
    public GameObject HitDrumInputCorrectMarker; // 预制体，用于标记正确的击打输入
    public GameObject HitDrumInputLevel1ErrorMarker; // 预制体，用于标记 level 1 错误输入
    public float correctTimeTolerance = 0.1f; // 配对时的时间误差容许值，单位为秒
    public float level1ErrorTimeTolerance = 0.2f; // level 1 错误的时间误差容许值，单位为秒
    public float level1ErrorShift = 0.1f; // 当出现 level 1 错误时，标记位置的偏移量
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
    public TextMeshProUGUI correctRateText; // TextMeshProUGUI 用于显示总正确率
    public TextMeshProUGUI level1CorrectRateText; // TextMeshProUGUI 用于显示总包含 level 1 错误的正确率
    public TextMeshProUGUI mainRhythmCorrectRateText; // TextMeshProUGUI 用于显示主节奏的正确率
    public TextMeshProUGUI mainRhythmLevel1CorrectRateText; // TextMeshProUGUI 用于显示主节奏包含 level 1 错误的正确率
    public TextMeshProUGUI oneThreeRhythmCorrectRateText; // TextMeshProUGUI 用于显示1和3拍节奏的正确率
    public TextMeshProUGUI oneThreeRhythmLevel1CorrectRateText; // TextMeshProUGUI 用于显示1和3拍节奏包含 level 1 错误的正确率
    public TextMeshProUGUI twoFourRhythmCorrectRateText; // TextMeshProUGUI 用于显示2和4拍节奏的正确率
    public TextMeshProUGUI twoFourRhythmLevel1CorrectRateText; // TextMeshProUGUI 用于显示2和4拍节奏包含 level 1 错误的正确率
    public TextMeshProUGUI rightHandFourBeatCorrectRateText; // TextMeshProUGUI 用于显示右手四拍节奏的正确率
    public TextMeshProUGUI rightHandFourBeatLevel1CorrectRateText; // TextMeshProUGUI 用于显示右手四拍节奏包含 level 1 错误的正确率
    public TextMeshProUGUI rightHandEightBeatCorrectRateText; // TextMeshProUGUI 用于显示右手八拍节奏的正确率
    public TextMeshProUGUI rightHandEightBeatLevel1CorrectRateText; // TextMeshProUGUI 用于显示右手八拍节奏包含 level 1 错误的正确率
    public Transform markerHolder; // 用于存放生成的 marker 的父级对象

    // 鼓击打的 InputActions
    public InputAction bassDrumHit;
    public InputAction snareDrumHit;
    public InputAction closedHiHatHit;
    public InputAction tom1Hit;
    public InputAction tom2Hit;
    public InputAction floorTomHit;
    public InputAction crashHit;
    public InputAction rideHit;
    public InputAction openHiHatHit;

    private bool isTracking = false; // 用于跟踪播放状态
    private List<HitDrumInputData> inputLog; // 存储击打输入数据的日志
    private List<TrackedHitSegment> trackedHitSegments; // 存储复制并跟踪的 HitSegment

    private void Awake()
    {
        inputLog = new List<HitDrumInputData>(); // 初始化输入日志列表
    }

    private void OnEnable()
    {
        // 订阅 TransformPlayBacker 的播放事件
        if (transformPlayBacker != null)
        {
            transformPlayBacker.OnPlayTransformDataStart += StartTracking;
            transformPlayBacker.OnPlayTransformDataEnd += StopTrackingAndCalculateCorrectRate;
        }
    }

    private void OnDisable()
    {
        // 取消订阅 TransformPlayBacker 的播放事件
        if (transformPlayBacker != null)
        {
            transformPlayBacker.OnPlayTransformDataStart -= StartTracking;
            transformPlayBacker.OnPlayTransformDataEnd -= StopTrackingAndCalculateCorrectRate;
        }
    }

    // 当 TransformPlayBacker 开始播放时，启动输入跟踪
    private void StartTracking()
    {
        isTracking = true;
        inputLog.Clear(); // 清除之前的数据

        // 清除之前生成的所有标记
        if (markerHolder != null)
        {
            foreach (Transform child in markerHolder)
            {
                Destroy(child.gameObject);
            }
        }

        // 初始化并复制 TransformPlayBacker 的 HitSegments
        trackedHitSegments = new List<TrackedHitSegment>();
        foreach (var segment in transformPlayBacker.hitSegments)
        {
            trackedHitSegments.Add(new TrackedHitSegment
            {
                limbUsed = segment.limbUsed,
                drumHit = segment.drumHit,
                startIdx = segment.startIdx,
                endIdx = segment.endIdx,
                skip = segment.skip, // 保留 skip 状态
                associatedNote = segment.associatedNote,
                matched = false,
                correct = false,
                level1TimeError = false
            });
        }

        // 启用所有 InputActions
        bassDrumHit.Enable();
        snareDrumHit.Enable();
        closedHiHatHit.Enable();
        tom1Hit.Enable();
        tom2Hit.Enable();
        floorTomHit.Enable();
        crashHit.Enable();
        rideHit.Enable();
        openHiHatHit.Enable();
    }

    // 当 TransformPlayBacker 停止播放时，停止输入跟踪并计算正确率
    private void StopTrackingAndCalculateCorrectRate()
    {
        isTracking = false;

        // 计算正确率
        CalculateCorrectRates();

        // 更新显示的正确率
        if (correctRateText != null)
        {
            correctRateText.text = $"Correct Rate: {correctRate:P2}";
        }

        if (level1CorrectRateText != null)
        {
            level1CorrectRateText.text = $"Level 1 Correct Rate: {level1CorrectRate:P2}";
        }

        if (mainRhythmCorrectRateText != null)
        {
            mainRhythmCorrectRateText.text = $"Main Rhythm Correct Rate: {mainRhythmCorrectRate:P2}";
        }

        if (mainRhythmLevel1CorrectRateText != null)
        {
            mainRhythmLevel1CorrectRateText.text = $"Main Rhythm Level 1 Correct Rate: {mainRhythmLevel1CorrectRate:P2}";
        }

        if (oneThreeRhythmCorrectRateText != null)
        {
            oneThreeRhythmCorrectRateText.text = $"1 & 3 Rhythm Correct Rate: {oneThreeRhythmCorrectRate:P2}";
        }

        if (oneThreeRhythmLevel1CorrectRateText != null)
        {
            oneThreeRhythmLevel1CorrectRateText.text = $"1 & 3 Rhythm Level 1 Correct Rate: {oneThreeRhythmLevel1CorrectRate:P2}";
        }

        if (twoFourRhythmCorrectRateText != null)
        {
            twoFourRhythmCorrectRateText.text = $"2 & 4 Rhythm Correct Rate: {twoFourRhythmCorrectRate:P2}";
        }

        if (twoFourRhythmLevel1CorrectRateText != null)
        {
            twoFourRhythmLevel1CorrectRateText.text = $"2 & 4 Rhythm Level 1 Correct Rate: {twoFourRhythmLevel1CorrectRate:P2}";
        }

        if (rightHandFourBeatCorrectRateText != null)
        {
            rightHandFourBeatCorrectRateText.text = $"Right Hand Four Beat Correct Rate: {rightHandFourBeatCorrectRate:P2}";
        }

        if (rightHandFourBeatLevel1CorrectRateText != null)
        {
            rightHandFourBeatLevel1CorrectRateText.text = $"Right Hand Four Beat Level 1 Correct Rate: {rightHandFourBeatLevel1CorrectRate:P2}";
        }

        if (rightHandEightBeatCorrectRateText != null)
        {
            rightHandEightBeatCorrectRateText.text = $"Right Hand Eight Beat Correct Rate: {rightHandEightBeatCorrectRate:P2}";
        }

        if (rightHandEightBeatLevel1CorrectRateText != null)
        {
            rightHandEightBeatLevel1CorrectRateText.text = $"Right Hand Eight Beat Level 1 Correct Rate: {rightHandEightBeatLevel1CorrectRate:P2}";
        }

        // 禁用所有 InputActions
        bassDrumHit.Disable();
        snareDrumHit.Disable();
        closedHiHatHit.Disable();
        tom1Hit.Disable();
        tom2Hit.Disable();
        floorTomHit.Disable();
        crashHit.Disable();
        rideHit.Disable();
        openHiHatHit.Disable();
    }

    private void Update()
    {
        if (isTracking)
        {
            // 检查每个 InputAction 是否触发并记录事件
            CheckAndLogInput(bassDrumHit, DrumType.BassDrum);
            CheckAndLogInput(snareDrumHit, DrumType.SnareDrum);
            CheckAndLogInput(closedHiHatHit, DrumType.ClosedHiHat);
            CheckAndLogInput(tom1Hit, DrumType.Tom1);
            CheckAndLogInput(tom2Hit, DrumType.Tom2);
            CheckAndLogInput(floorTomHit, DrumType.FloorTom);
            CheckAndLogInput(crashHit, DrumType.Crash);
            CheckAndLogInput(rideHit, DrumType.Ride);
            CheckAndLogInput(openHiHatHit, DrumType.OpenHiHat);
        }
    }

    // 检查 InputAction 是否触发并记录击打数据
    private void CheckAndLogInput(InputAction inputAction, DrumType drumType)
    {
        if (inputAction.triggered)
        {
            // 获取当前索引的时间戳，并考虑播放速度
            float timestamp = transformPlayBacker.playbackData.dataList[transformPlayBacker.currentIndex].timestamp 
                              * (transformPlayBacker.playbackData.bpm / transformPlayBacker.playBackBPM);
            float hitValue = inputAction.ReadValue<float>();

            // 记录输入数据
            inputLog.Add(new HitDrumInputData
            {
                drumType = drumType,
                timestamp = timestamp,
                hitValue = hitValue
            });

            // 检查输入时间戳是否接近任何未配对的且未被跳过的 HitSegment 的 endIdx 时间戳
            foreach (var segment in trackedHitSegments)
            {
                if (!segment.matched && !segment.skip && segment.drumHit == drumType)
                {
                    float segmentTimestamp = transformPlayBacker.playbackData.dataList[segment.endIdx].timestamp;

                    // 根据播放速度调整时间误差的计算
                    float adjustedTimestamp = segmentTimestamp * transformPlayBacker.playbackData.bpm / transformPlayBacker.playBackBPM;

                    float timeDifference = Mathf.Abs(timestamp - adjustedTimestamp);

                    // 使用 correctTimeTolerance 作为容许误差
                    if (timeDifference < correctTimeTolerance)
                    {
                        segment.matched = true;
                        segment.correct = true; // 标记为正确

                        // 在 associatedNote 的位置生成 HitDrumInputCorrectMarker
                        if (HitDrumInputCorrectMarker != null && segment.associatedNote != null)
                        {
                            Vector3 notePosition = segment.associatedNote.transform.position;
                            Instantiate(HitDrumInputCorrectMarker, notePosition, Quaternion.identity, markerHolder);
                        }

                        break; // 配对后跳出循环
                    }
                    // 使用 level1ErrorTimeTolerance 作为容许误差
                    else if (timeDifference < level1ErrorTimeTolerance)
                    {
                        segment.matched = true;
                        segment.level1TimeError = true; // 标记为 level 1 时间错误

                        if (HitDrumInputLevel1ErrorMarker != null && segment.associatedNote != null)
                        {
                            Vector3 notePosition = segment.associatedNote.transform.position;

                            // 根据 hitDrumInput 时间相对于 segment 的时间早晚来决定偏移方向
                            if (timestamp < adjustedTimestamp)
                            {
                                notePosition.x -= level1ErrorShift; // 时间太早，向 -x 方向偏移
                            }
                            else
                            {
                                notePosition.x += level1ErrorShift; // 时间太晚，向 +x 方向偏移
                            }

                            Instantiate(HitDrumInputLevel1ErrorMarker, notePosition, Quaternion.identity, markerHolder);
                        }

                        break; // 配对后跳出循环
                    }
                }
            }
        }
    }

    // 计算正确率和 level 1 正确率
    private void CalculateCorrectRates()
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

        foreach (var segment in trackedHitSegments)
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
                (segment.associatedNote.beatPosition == 1 || segment.associatedNote.beatPosition == 1.5f ||
                 segment.associatedNote.beatPosition == 2 || segment.associatedNote.beatPosition == 2.5f ||
                 segment.associatedNote.beatPosition == 3 || segment.associatedNote.beatPosition == 3.5f ||
                 segment.associatedNote.beatPosition == 4 || segment.associatedNote.beatPosition == 4.5f))
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
        }

        // 修改后的逻辑：如果没有段落，默认正确率为 100%
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
    }

    // 序列化的类，用于存储每次击打的输入数据
    [Serializable]
    private class HitDrumInputData
    {
        public DrumType drumType; // 鼓的类型
        public float timestamp; // 时间戳
        public float hitValue; // 击打力度值
    }

    // 扩展的 HitSegment 类，用于跟踪输入配对情况
    [Serializable]
    private class TrackedHitSegment : TransformPlayBacker.HitSegment
    {
        public bool matched = false; // 是否已配对
        public bool correct = false; // 是否为正确配对
        public bool level1TimeError = false; // 是否为 level 1 时间错误
    }
}
