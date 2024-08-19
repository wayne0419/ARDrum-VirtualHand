using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RealTimeInputTracker : MonoBehaviour
{
    // 引用 TransformPlayBacker，用于检测播放状态
    public TransformPlayBacker transformPlayBacker;
    public GameObject HitDrumInputMarker; // 预制体，用于标记正确的击打输入

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

        // 清除之前生成的所有 HitDrumInputMarker
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
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
                correct = false
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
            // 计算相对于播放开始时间的时间戳
            float timestamp = Time.time - transformPlayBacker.playbackStartTime;
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

                    if (Mathf.Abs(timestamp - adjustedTimestamp) < 0.1f) // 允许0.1秒的时间误差
                    {
                        segment.matched = true;
                        segment.correct = true; // 标记为正确

                        // 在 associatedNote 的位置生成 HitDrumInputMarker
                        if (HitDrumInputMarker != null && segment.associatedNote != null)
                        {
                            Vector3 notePosition = segment.associatedNote.transform.position;
                            Instantiate(HitDrumInputMarker, notePosition, Quaternion.identity, transform);
                        }

                        break; // 配对后跳出循环
                    }
                }
            }
        }
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
    }
}
