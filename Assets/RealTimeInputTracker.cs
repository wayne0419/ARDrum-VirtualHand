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

    public TransformPlayBacker transformPlayBacker;
    public GameObject HitDrumInputCorrectMarker;
    public GameObject HitDrumInputLevel1ErrorMarker;
    public float correctTimeTolerance = 0.1f;
    public float level1ErrorTimeTolerance = 0.2f;
    public float level1ErrorShift = 0.1f;

    public Transform markerHolder;

    public InputAction bassDrumHit;
    public InputAction snareDrumHit;
    public InputAction closedHiHatHit;
    public InputAction tom1Hit;
    public InputAction tom2Hit;
    public InputAction floorTomHit;
    public InputAction crashHit;
    public InputAction rideHit;
    public InputAction openHiHatHit;

    private bool isTracking = false;
    private List<HitDrumInputData> inputLog;
    private List<TrackedHitSegment> trackedHitSegments;

    private int expectedHitIndex; // 期待的击打顺序索引

    public event Action OnFinishTracking;

    private void Awake()
    {
        inputLog = new List<HitDrumInputData>();
    }

    private void OnEnable()
    {
        if (transformPlayBacker != null)
        {
            transformPlayBacker.OnPlayTransformDataStart += StartTracking;
            transformPlayBacker.OnPlayTransformDataEnd += StopTracking;
        }
    }

    private void OnDisable()
    {
        if (transformPlayBacker != null)
        {
            transformPlayBacker.OnPlayTransformDataStart -= StartTracking;
            transformPlayBacker.OnPlayTransformDataEnd -= StopTracking;
        }
    }

    private void StartTracking()
    {
        isTracking = true;
        inputLog.Clear();

        if (markerHolder != null)
        {
            foreach (Transform child in markerHolder)
            {
                Destroy(child.gameObject);
            }
        }

        trackedHitSegments = new List<TrackedHitSegment>();
        foreach (var segment in transformPlayBacker.hitSegments)
        {
            trackedHitSegments.Add(new TrackedHitSegment
            {
                limbUsed = segment.limbUsed,
                drumHit = segment.drumHit,
                startIdx = segment.startIdx,
                endIdx = segment.endIdx,
                skip = segment.skip,
                associatedNote = segment.associatedNote,
                matched = false,
                correct = false,
                level1TimeError = false
            });
        }

        expectedHitIndex = 0; // 初始化预期的击打顺序索引

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

    private void StopTracking()
    {
        isTracking = false;
        OnFinishTracking?.Invoke();

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

    private void CheckAndLogInput(InputAction inputAction, DrumType drumType)
    {
        if (inputAction.triggered)
        {
            float timestamp = transformPlayBacker.playbackData.dataList[transformPlayBacker.currentIndex].timestamp 
                              * (transformPlayBacker.playbackData.bpm / transformPlayBacker.playBackBPM);
            float hitValue = inputAction.ReadValue<float>();

            inputLog.Add(new HitDrumInputData
            {
                drumType = drumType,
                timestamp = timestamp,
                hitValue = hitValue
            });

            if (currentMode == CorrectMode.CorrectRhythmMode)
            {
                CheckCorrectRhythmMode(drumType, timestamp);
            }
            else if (currentMode == CorrectMode.CorrectOrderMode)
            {
                CheckCorrectOrderMode(drumType);
            }
        }
    }

    private void CheckCorrectRhythmMode(DrumType drumType, float timestamp)
    {
        foreach (var segment in trackedHitSegments)
        {
            if (!segment.matched && !segment.skip && segment.drumHit == drumType)
            {
                float segmentTimestamp = transformPlayBacker.playbackData.dataList[segment.endIdx].timestamp;
                float adjustedTimestamp = segmentTimestamp * transformPlayBacker.playbackData.bpm / transformPlayBacker.playBackBPM;
                float timeDifference = Mathf.Abs(timestamp - adjustedTimestamp);

                if (timeDifference < correctTimeTolerance)
                {
                    segment.matched = true;
                    segment.correct = true;

                    if (HitDrumInputCorrectMarker != null && segment.associatedNote != null)
                    {
                        Vector3 notePosition = segment.associatedNote.transform.position;
                        Instantiate(HitDrumInputCorrectMarker, notePosition, Quaternion.identity, markerHolder);
                    }
                    break;
                }
                else if (timeDifference < level1ErrorTimeTolerance)
                {
                    segment.matched = true;
                    segment.level1TimeError = true;

                    if (HitDrumInputLevel1ErrorMarker != null && segment.associatedNote != null)
                    {
                        Vector3 notePosition = segment.associatedNote.transform.position;

                        if (timestamp < adjustedTimestamp)
                        {
                            notePosition.x -= level1ErrorShift;
                        }
                        else
                        {
                            notePosition.x += level1ErrorShift;
                        }

                        Instantiate(HitDrumInputLevel1ErrorMarker, notePosition, Quaternion.identity, markerHolder);
                    }
                    break;
                }
            }
        }
    }

    private void CheckCorrectOrderMode(DrumType drumType)
    {
        if (expectedHitIndex < trackedHitSegments.Count)
        {
            var expectedSegment = trackedHitSegments[expectedHitIndex];

            if (expectedSegment.drumHit == drumType)
            {
                expectedSegment.correct = true;
                expectedHitIndex++;

                if (HitDrumInputCorrectMarker != null && expectedSegment.associatedNote != null)
                {
                    Vector3 notePosition = expectedSegment.associatedNote.transform.position;
                    Instantiate(HitDrumInputCorrectMarker, notePosition, Quaternion.identity, markerHolder);
                }
            }
            else
            {
                // 如果输入不符合顺序，可以根据需求添加错误处理逻辑
                Debug.Log("Incorrect order of drum hits.");
            }
        }
    }

    public List<TrackedHitSegment> GetTrackedHitSegments()
    {
        return trackedHitSegments;
    }

    [Serializable]
    private class HitDrumInputData
    {
        public DrumType drumType;
        public float timestamp;
        public float hitValue;
    }

    [Serializable]
    public class TrackedHitSegment : TransformPlayBacker.HitSegment
    {
        public bool matched = false;
        public bool correct = false;
        public bool level1TimeError = false;
    }
}
