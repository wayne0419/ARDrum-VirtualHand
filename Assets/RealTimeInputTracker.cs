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
    public TextMeshProUGUI rightHandSixteenBeatCorrectRateText; // TextMeshProUGUI 用于显示右手十六拍节奏的正确率
    public TextMeshProUGUI rightHandSixteenBeatLevel1CorrectRateText; // TextMeshProUGUI 用于显示右手十六拍节奏包含 level 1 错误的正确率
    public TextMeshProUGUI bothHandFourBeatCorrectRateText; // TextMeshProUGUI 用于显示双手4拍节奏的正确率
    public TextMeshProUGUI bothHandFourBeatLevel1CorrectRateText; // TextMeshProUGUI 用于显示双手4拍节奏包含 level 1 错误的正确率
    public TextMeshProUGUI bothHandEightBeatCorrectRateText; // TextMeshProUGUI 用于显示双手8拍节奏的正确率
    public TextMeshProUGUI bothHandEightBeatLevel1CorrectRateText; // TextMeshProUGUI 用于显示双手8拍节奏包含 level 1 错误的正确率
    public TextMeshProUGUI bothHandSixteenBeatCorrectRateText; // TextMeshProUGUI 用于显示双手十六拍节奏的正确率
    public TextMeshProUGUI bothHandSixteenBeatLevel1CorrectRateText; // TextMeshProUGUI 用于显示双手十六拍节奏包含 level 1 错误的正确率
    public TextMeshProUGUI rightHandRightFeetFourBeatCorrectRateText; // TextMeshProUGUI 用于显示右手右腳4拍节奏的正确率
    public TextMeshProUGUI rightHandRightFeetFourBeatLevel1CorrectRateText; // TextMeshProUGUI 用于显示右手右腳4拍节奏包含 level 1 错误的正确率
    public TextMeshProUGUI rightHandRightFeetEightBeatCorrectRateText; // TextMeshProUGUI 用于显示右手右腳8拍节奏的正确率
    public TextMeshProUGUI rightHandRightFeetEightBeatLevel1CorrectRateText; // TextMeshProUGUI 用于显示右手右腳8拍节奏包含 level 1 错误的正确率
    public TextMeshProUGUI rightHandRightFeetSixteenBeatCorrectRateText; // TextMeshProUGUI 用于显示右手右腳十六拍节奏的正确率
    public TextMeshProUGUI rightHandRightFeetSixteenBeatLevel1CorrectRateText; // TextMeshProUGUI 用于显示右手右腳十六拍节奏包含 level 1 错误的正确率
    public TextMeshProUGUI rightHandLeftHandRightFeetFourBeatCorrectRateText; // TextMeshProUGUI 用于显示右手左手右腳4拍节奏的正确率
    public TextMeshProUGUI rightHandLeftHandRightFeetFourBeatLevel1CorrectRateText; // TextMeshProUGUI 用于显示右手左手右腳4拍节奏包含 level 1 错误的正确率
    public TextMeshProUGUI rightHandLeftHandRightFeetEightBeatCorrectRateText; // TextMeshProUGUI 用于显示右手左手右腳8拍节奏的正确率
    public TextMeshProUGUI rightHandLeftHandRightFeetEightBeatLevel1CorrectRateText; // TextMeshProUGUI 用于显示右手左手右腳8拍节奏包含 level 1 错误的正确率
    public TextMeshProUGUI rightHandLeftHandRightFeetSixteenBeatCorrectRateText; // TextMeshProUGUI 用于显示右手左手右腳十六拍节奏的正确率
    public TextMeshProUGUI rightHandLeftHandRightFeetSixteenBeatLevel1CorrectRateText; // TextMeshProUGUI 用于显示右手左手右腳十六拍节奏包含 level 1 错误的正确率
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
            correctRateText.text = $"Correct Rate: {correctRate:P0}";
        }

        if (level1CorrectRateText != null)
        {
            level1CorrectRateText.text = $"Level 1 Correct Rate: {level1CorrectRate:P0}";
        }

        if (mainRhythmCorrectRateText != null)
        {
            mainRhythmCorrectRateText.text = $"Main Rhythm Correct Rate: {mainRhythmCorrectRate:P0}";
        }

        if (mainRhythmLevel1CorrectRateText != null)
        {
            mainRhythmLevel1CorrectRateText.text = $"Main Rhythm Level 1 Correct Rate: {mainRhythmLevel1CorrectRate:P0}";
        }

        if (oneThreeRhythmCorrectRateText != null)
        {
            oneThreeRhythmCorrectRateText.text = $"1 & 3 Rhythm Correct Rate: {oneThreeRhythmCorrectRate:P0}";
        }

        if (oneThreeRhythmLevel1CorrectRateText != null)
        {
            oneThreeRhythmLevel1CorrectRateText.text = $"1 & 3 Rhythm Level 1 Correct Rate: {oneThreeRhythmLevel1CorrectRate:P0}";
        }

        if (twoFourRhythmCorrectRateText != null)
        {
            twoFourRhythmCorrectRateText.text = $"2 & 4 Rhythm Correct Rate: {twoFourRhythmCorrectRate:P0}";
        }

        if (twoFourRhythmLevel1CorrectRateText != null)
        {
            twoFourRhythmLevel1CorrectRateText.text = $"2 & 4 Rhythm Level 1 Correct Rate: {twoFourRhythmLevel1CorrectRate:P0}";
        }

        if (rightHandFourBeatCorrectRateText != null)
        {
            rightHandFourBeatCorrectRateText.text = $"{rightHandFourBeatCorrectRate:P0}";
        }

        if (rightHandFourBeatLevel1CorrectRateText != null)
        {
            rightHandFourBeatLevel1CorrectRateText.text = $"{rightHandFourBeatLevel1CorrectRate:P0}";
        }

        if (rightHandEightBeatCorrectRateText != null)
        {
            rightHandEightBeatCorrectRateText.text = $"{rightHandEightBeatCorrectRate:P0}";
        }

        if (rightHandEightBeatLevel1CorrectRateText != null)
        {
            rightHandEightBeatLevel1CorrectRateText.text = $"{rightHandEightBeatLevel1CorrectRate:P0}";
        }

        if (rightHandSixteenBeatCorrectRateText != null)
        {
            rightHandSixteenBeatCorrectRateText.text = $"{rightHandSixteenBeatCorrectRate:P0}";
        }

        if (rightHandSixteenBeatLevel1CorrectRateText != null)
        {
            rightHandSixteenBeatLevel1CorrectRateText.text = $"{rightHandSixteenBeatLevel1CorrectRate:P0}";
        }

        if (bothHandFourBeatCorrectRateText != null)
        {
            bothHandFourBeatCorrectRateText.text = $"{bothHandFourBeatCorrectRate:P0}";
        }

        if (bothHandFourBeatLevel1CorrectRateText != null)
        {
            bothHandFourBeatLevel1CorrectRateText.text = $"{bothHandFourBeatLevel1CorrectRate:P0}";
        }

        if (bothHandEightBeatCorrectRateText != null)
        {
            bothHandEightBeatCorrectRateText.text = $"{bothHandEightBeatCorrectRate:P0}";
        }

        if (bothHandEightBeatLevel1CorrectRateText != null)
        {
            bothHandEightBeatLevel1CorrectRateText.text = $"{bothHandEightBeatLevel1CorrectRate:P0}";
        }

        if (bothHandSixteenBeatCorrectRateText != null)
        {
            bothHandSixteenBeatCorrectRateText.text = $"{bothHandSixteenBeatCorrectRate:P0}";
        }

        if (bothHandSixteenBeatLevel1CorrectRateText != null)
        {
            bothHandSixteenBeatLevel1CorrectRateText.text = $"{bothHandSixteenBeatLevel1CorrectRate:P0}";
        }

        if (rightHandRightFeetFourBeatCorrectRateText != null)
        {
            rightHandRightFeetFourBeatCorrectRateText.text = $"{rightHandRightFeetFourBeatCorrectRate:P0}";
        }

        if (rightHandRightFeetFourBeatLevel1CorrectRateText != null)
        {
            rightHandRightFeetFourBeatLevel1CorrectRateText.text = $"{rightHandRightFeetFourBeatLevel1CorrectRate:P0}";
        }

        if (rightHandRightFeetEightBeatCorrectRateText != null)
        {
            rightHandRightFeetEightBeatCorrectRateText.text = $"{rightHandRightFeetEightBeatCorrectRate:P0}";
        }

        if (rightHandRightFeetEightBeatLevel1CorrectRateText != null)
        {
            rightHandRightFeetEightBeatLevel1CorrectRateText.text = $"{rightHandRightFeetEightBeatLevel1CorrectRate:P0}";
        }

        if  (rightHandRightFeetSixteenBeatCorrectRateText != null)
        {
            rightHandRightFeetSixteenBeatCorrectRateText.text = $"{rightHandRightFeetSixteenBeatCorrectRate:P0}";
        }

        if (rightHandRightFeetSixteenBeatLevel1CorrectRateText != null)
        {
            rightHandRightFeetSixteenBeatLevel1CorrectRateText.text = $"{rightHandRightFeetSixteenBeatLevel1CorrectRate:P0}";
        }

        if (rightHandLeftHandRightFeetFourBeatCorrectRateText != null)
        {
            rightHandLeftHandRightFeetFourBeatCorrectRateText.text = $"{rightHandLeftHandRightFeetFourBeatCorrectRate:P0}";
        }

        if (rightHandLeftHandRightFeetFourBeatLevel1CorrectRateText != null)
        {
            rightHandLeftHandRightFeetFourBeatLevel1CorrectRateText.text = $"{rightHandLeftHandRightFeetFourBeatLevel1CorrectRate:P0}";
        }

        if (rightHandLeftHandRightFeetEightBeatCorrectRateText != null)
        {
            rightHandLeftHandRightFeetEightBeatCorrectRateText.text = $"{rightHandLeftHandRightFeetEightBeatCorrectRate:P0}";
        }

        if (rightHandLeftHandRightFeetEightBeatLevel1CorrectRateText != null)
        {
            rightHandLeftHandRightFeetEightBeatLevel1CorrectRateText.text = $"{rightHandLeftHandRightFeetEightBeatLevel1CorrectRate:P0}";
        }

        if  (rightHandLeftHandRightFeetSixteenBeatCorrectRateText != null)
        {
            rightHandLeftHandRightFeetSixteenBeatCorrectRateText.text = $"{rightHandLeftHandRightFeetSixteenBeatCorrectRate:P0}";
        }

        if (rightHandLeftHandRightFeetSixteenBeatLevel1CorrectRateText != null)
        {
            rightHandLeftHandRightFeetSixteenBeatLevel1CorrectRateText.text = $"{rightHandLeftHandRightFeetSixteenBeatLevel1CorrectRate:P0}";
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
            if ((segment.limbUsed == "righthand" || segment.limbUsed == "lefthand") && segment.associatedNote != null &&
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
            if ((segment.limbUsed == "righthand" || segment.limbUsed == "lefthand") && segment.associatedNote != null &&
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
