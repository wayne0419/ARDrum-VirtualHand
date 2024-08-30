using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RealTimeInputTracker : MonoBehaviour
{
    // 新增的枚举类型来表示正确性判断模式
    public enum CorrectMode
    {
        CorrectRhythmMode, // 基于时间误差的模式
        CorrectOrderMode   // 基于顺序的模式
    }

    public CorrectMode currentMode = CorrectMode.CorrectRhythmMode; // 当前的判断模式

    // 引用 TransformPlayBacker，用于检测播放状态
    public TransformPlayBacker transformPlayBacker;
    public GameObject HitDrumInputCorrectMarker; // 预制体，用于标记正确的击打输入
    public GameObject HitDrumInputLevel1ErrorMarker; // 预制体，用于标记 level 1 错误输入
    public GameObject HitDrumInputErrorMarker; // 预制体，用于标记 Error
    public GameObject HitDrumInputMissMarker; // 预制体，用于标记 Miss

    public float correctTimeTolerance = 0.1f; // 配对时的时间误差容许值，单位为秒
    public float level1ErrorTimeTolerance = 0.2f; // level 1 错误的时间误差容许值，单位为秒
    public float level1ErrorShift = 0.1f; // 当出现 level 1 错误时，标记位置的偏移量
    
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
    public List<HitDrumInputData> inputLog; // 存储击打输入数据的日志
    private List<TrackedHitSegment> trackedHitSegments; // 存储复制并跟踪的 HitSegment


    // 事件：当 StopTracking() 結束时触发
    public event Action OnFinishTracking;

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
            transformPlayBacker.OnPlayTransformDataEnd += StopTracking;
        }
    }

    private void OnDisable()
    {
        // 取消订阅 TransformPlayBacker 的播放事件
        if (transformPlayBacker != null)
        {
            transformPlayBacker.OnPlayTransformDataStart -= StartTracking;
            transformPlayBacker.OnPlayTransformDataEnd -= StopTracking;
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

    // 当 TransformPlayBacker 停止播放时，停止输入跟踪
    private void StopTracking()
    {
        isTracking = false;
        OnFinishTracking?.Invoke();

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
        if (Input.GetKeyDown(KeyCode.Keypad7)) {
            markerHolder.gameObject.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Keypad4)) {
            markerHolder.gameObject.SetActive(false);
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

            // 根据当前模式执行不同的正确性判断
            if (currentMode == CorrectMode.CorrectRhythmMode)
            {
                CheckHitDrumCorrectRhythmMode(drumType, timestamp);
            }
            else if (currentMode == CorrectMode.CorrectOrderMode)
            {
                CheckHitDrumCorrectOrderMode(drumType);
            }
        }
    }

    // 检查是否在 CorrectRhythmMode 中正确击打
    private void CheckHitDrumCorrectRhythmMode(DrumType drumType, float timestamp)
    {
        bool matched = false;  // 用于跟踪是否有匹配的 segment

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
                    matched = true;
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
                    matched = true;
                    break; // 配对后跳出循环
                }
            }
        }

        // 如果没有任何匹配的 segment，则生成 MissMarker
        if (!matched)
        {
            if (HitDrumInputMissMarker != null && transformPlayBacker.drumSheetCursor != null)
            {
                Vector3 cursorPosition = transformPlayBacker.drumSheetCursor.transform.position;
                if (drumType == DrumType.Crash) {
                    cursorPosition.y = transformPlayBacker.drumSheet.drumSheetCrashRowAnchor.transform.position.y;
                }
                else if (drumType == DrumType.OpenHiHat) {
                    cursorPosition.y = transformPlayBacker.drumSheet.drumSheetOpenHiHatRowAnchor.transform.position.y;
                }
                else if (drumType == DrumType.ClosedHiHat) {
                    cursorPosition.y = transformPlayBacker.drumSheet.drumSheetClosedHiHatRowAnchor.transform.position.y;
                }
                else if (drumType == DrumType.Ride) {
                    cursorPosition.y = transformPlayBacker.drumSheet.drumSheetRideRowAnchor.transform.position.y;
                }
                else if (drumType == DrumType.Tom1) {
                    cursorPosition.y = transformPlayBacker.drumSheet.drumSheetTom1RowAnchor.transform.position.y;
                }
                else if (drumType == DrumType.Tom2) {
                    cursorPosition.y = transformPlayBacker.drumSheet.drumSheetTom2RowAnchor.transform.position.y;
                }
                else if (drumType == DrumType.FloorTom) {
                    cursorPosition.y = transformPlayBacker.drumSheet.drumSheetFloorTomRowAnchor.transform.position.y;
                }
                else if (drumType == DrumType.SnareDrum) {
                    cursorPosition.y = transformPlayBacker.drumSheet.drumSheetSnareRowAnchor.transform.position.y;
                }
                else if (drumType == DrumType.BassDrum) {
                    cursorPosition.y = transformPlayBacker.drumSheet.drumSheetBassRowAnchor.transform.position.y;
                }
                Instantiate(HitDrumInputMissMarker, cursorPosition, Quaternion.identity, markerHolder);
            }
        }
    }


    // 检查是否在 CorrectOrderMode 中正确击打
    private void CheckHitDrumCorrectOrderMode(DrumType drumType)
    {
        // 計算 currentBeatPosition : 目前unskipped && unmatched 中最小的 beatPosiiton
        float currentBeatPosition = float.PositiveInfinity;
        foreach (var segment in trackedHitSegments)
        {
            if (!segment.matched && !segment.skip && segment.associatedNote.beatPosition < currentBeatPosition) {
                currentBeatPosition = segment.associatedNote.beatPosition;
            }
        }

        // 檢查所有 beatPosition == currentBeatPosition 是否有匹配
        bool findMatched = false;
        foreach (var segment in trackedHitSegments)
        {
            if (!segment.matched && !segment.skip && segment.associatedNote.beatPosition == currentBeatPosition) {
                // 允许任何具有相同 beatPosition 的 segment 被击打
                if (segment.drumHit == drumType)
                {
                    segment.matched = true;
                    segment.correct = true; // 标记为正确

                    // 在 associatedNote 的位置生成 HitDrumInputCorrectMarker
                    if (HitDrumInputCorrectMarker != null && segment.associatedNote != null)
                    {
                        Vector3 notePosition = segment.associatedNote.transform.position;
                        Instantiate(HitDrumInputCorrectMarker, notePosition, Quaternion.identity, markerHolder);
                    }
                    findMatched = true;
                    break;
                }
            }
        }

        // 如果完全沒有匹配，就把下一個segment標記為 matched 但是錯誤
        if (!findMatched) {
            foreach (var segment in trackedHitSegments)
            {
                if (!segment.matched && !segment.skip && segment.associatedNote.beatPosition == currentBeatPosition) {
                    segment.matched = true;
                    segment.correct = false;

                    // 在 associatedNote 的位置生成 HitDrumInputErrorMarker
                    if (HitDrumInputErrorMarker != null && segment.associatedNote != null)
                    {
                        Vector3 notePosition = segment.associatedNote.transform.position;
                        Instantiate(HitDrumInputErrorMarker, notePosition, Quaternion.identity, markerHolder);
                    }
                    break;
                }
            }
        }
        
    }

    public List<TrackedHitSegment> GetTrackedHitSegments()
    {
        return trackedHitSegments;
    }

    // 序列化的类，用于存储每次击打的输入数据
    [Serializable]
    public class HitDrumInputData
    {
        public DrumType drumType; // 鼓的类型
        public float timestamp; // 时间戳
        public float hitValue; // 击打力度值
    }

    // 扩展的 HitSegment 类，用于跟踪输入配对情况
    [Serializable]
    public class TrackedHitSegment : TransformPlayBacker.HitSegment
    {
        public bool matched = false; // 是否已配对
        public bool correct = false; // 是否为正确配对
        public bool level1TimeError = false; // 是否为 level 1 时间错误
    }
}
