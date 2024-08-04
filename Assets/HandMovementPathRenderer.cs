using System.Collections.Generic;
using UnityEngine;

public class HandMovementPathRenderer : MonoBehaviour
{
    public TransformPlayBacker transformPlayBacker;
    public LineRenderer lineRenderer1;
    public LineRenderer lineRenderer2;
    public float lineWidth = 0.1f;
    public int preHitNumber = 5;
    public int postHitNumber = 5;
    public int resolution = 5; // 每隔多少点采样一个

    public Transform LeftHandDrumStickTipAnchor;
    public Transform RightHandDrumStickTipAnchor;

    public GameObject leftHandHighlightPoint; // 用于左手高亮的对象
    public GameObject rightHandHighlightPoint; // 用于右手高亮的对象
    public GameObject leftHandStartPoint; // 用于左手路径开始点的对象
    public GameObject rightHandStartPoint; // 用于右手路径开始点的对象
    public GameObject leftHandEndPoint; // 用于左手路径结束点的对象
    public GameObject rightHandEndPoint; // 用于右手路径结束点的对象

    public bool highlightHighestPointEnabled = true; // 控制是否启用高亮

    // 记录路径范围的最高点、开始点和结束点的位置和时间戳
    public Vector3 leftHandHighestPointPosition;
    public Vector3 leftHandStartPointPosition;
    public Vector3 leftHandEndPointPosition;
    public float leftHandHighestPointTimestamp;
    public float leftHandStartPointTimestamp;
    public float leftHandEndPointTimestamp;

    public Vector3 rightHandHighestPointPosition;
    public Vector3 rightHandStartPointPosition;
    public Vector3 rightHandEndPointPosition;
    public float rightHandHighestPointTimestamp;
    public float rightHandStartPointTimestamp;
    public float rightHandEndPointTimestamp;

    private void OnEnable()
    {
        if (transformPlayBacker == null)
        {
            Debug.LogError("TransformPlayBacker is not assigned.");
            return;
        }

        if (lineRenderer1 == null)
        {
            GameObject lineObj1 = new GameObject("LineRenderer1");
            lineObj1.transform.parent = this.transform;
            lineRenderer1 = lineObj1.AddComponent<LineRenderer>();
        }
        InitializeLineRenderer(lineRenderer1);

        if (lineRenderer2 == null)
        {
            GameObject lineObj2 = new GameObject("LineRenderer2");
            lineObj2.transform.parent = this.transform;
            lineRenderer2 = lineObj2.AddComponent<LineRenderer>();
        }
        InitializeLineRenderer(lineRenderer2);

        // 确保高亮对象已被分配
        if (leftHandHighlightPoint == null)
        {
            Debug.LogError("Left hand highlight point is not assigned.");
        }

        if (rightHandHighlightPoint == null)
        {
            Debug.LogError("Right hand highlight point is not assigned.");
        }

        if (leftHandStartPoint == null)
        {
            Debug.LogError("Left hand start point is not assigned.");
        }

        if (rightHandStartPoint == null)
        {
            Debug.LogError("Right hand start point is not assigned.");
        }

        if (leftHandEndPoint == null)
        {
            Debug.LogError("Left hand end point is not assigned.");
        }

        if (rightHandEndPoint == null)
        {
            Debug.LogError("Right hand end point is not assigned.");
        }
    }

    private void InitializeLineRenderer(LineRenderer lineRenderer)
    {
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;
        lineRenderer.positionCount = 0;
    }

    private void Update()
    {
        if (transformPlayBacker.isPlaying)
        {
            RenderPaths();
        }
    }

    private void RenderPaths()
    {
        List<Vector3> positions1 = new List<Vector3>();
        List<Vector3> positions2 = new List<Vector3>();

        if (transformPlayBacker.currentIndex < 0 || transformPlayBacker.currentIndex >= transformPlayBacker.playbackData.dataList.Count)
        {
            return;
        }

        int currentIndex = transformPlayBacker.currentIndex;
        float currentTime = transformPlayBacker.playbackData.dataList[currentIndex].timestamp;

        // 找到 preHitNumber 个左手击打事件之前的索引
        int startIndex1 = currentIndex;
        int leftHandHitCount = 0;
        while (startIndex1 > 0 && leftHandHitCount < preHitNumber)
        {
            startIndex1--;
            if (IsLeftHandHit(transformPlayBacker.playbackData.dataList[startIndex1]))
            {
                leftHandHitCount++;
            }
        }

        // 找到 postHitNumber 个左手击打事件之后的索引
        int endIndex1 = currentIndex;
        leftHandHitCount = 0;
        while (endIndex1 < transformPlayBacker.playbackData.dataList.Count - 1 && leftHandHitCount < postHitNumber)
        {
            endIndex1++;
            if (IsLeftHandHit(transformPlayBacker.playbackData.dataList[endIndex1]))
            {
                leftHandHitCount++;
            }
        }

        // 找到 preHitNumber 个右手击打事件之前的索引
        int startIndex2 = currentIndex;
        int rightHandHitCount = 0;
        while (startIndex2 > 0 && rightHandHitCount < preHitNumber)
        {
            startIndex2--;
            if (IsRightHandHit(transformPlayBacker.playbackData.dataList[startIndex2]))
            {
                rightHandHitCount++;
            }
        }

        // 找到 postHitNumber 个右手击打事件之后的索引
        int endIndex2 = currentIndex;
        rightHandHitCount = 0;
        while (endIndex2 < transformPlayBacker.playbackData.dataList.Count - 1 && rightHandHitCount < postHitNumber)
        {
            endIndex2++;
            if (IsRightHandHit(transformPlayBacker.playbackData.dataList[endIndex2]))
            {
                rightHandHitCount++;
            }
        }

        // 收集左手路径点
        for (int i = startIndex1; i <= endIndex1; i += resolution)
        {
            var data = transformPlayBacker.playbackData.dataList[i];
            Vector3 leftStickTipPosition = data.position1 + data.rotation1 * LeftHandDrumStickTipAnchor.localPosition;
            positions1.Add(leftStickTipPosition);
        }

        // 收集右手路径点
        for (int i = startIndex2; i <= endIndex2; i += resolution)
        {
            var data = transformPlayBacker.playbackData.dataList[i];
            Vector3 rightStickTipPosition = data.position2 + data.rotation2 * RightHandDrumStickTipAnchor.localPosition;
            positions2.Add(rightStickTipPosition);
        }

        // 设置 LineRenderer 的路径点
        lineRenderer1.positionCount = positions1.Count;
        lineRenderer1.SetPositions(positions1.ToArray());

        lineRenderer2.positionCount = positions2.Count;
        lineRenderer2.SetPositions(positions2.ToArray());

        // 记录路径范围的最高点、开始点和结束点
        if (positions1.Count > 0)
        {
            leftHandStartPointPosition = positions1[0];
            leftHandStartPointTimestamp = transformPlayBacker.playbackData.dataList[startIndex1].timestamp;
            leftHandEndPointPosition = positions1[positions1.Count - 1];
            leftHandEndPointTimestamp = transformPlayBacker.playbackData.dataList[endIndex1].timestamp;
            (leftHandHighestPointPosition, leftHandHighestPointTimestamp) = GetHighestPointAndTimestamp(positions1, startIndex1, endIndex1);
        }

        if (positions2.Count > 0)
        {
            rightHandStartPointPosition = positions2[0];
            rightHandStartPointTimestamp = transformPlayBacker.playbackData.dataList[startIndex2].timestamp;
            rightHandEndPointPosition = positions2[positions2.Count - 1];
            rightHandEndPointTimestamp = transformPlayBacker.playbackData.dataList[endIndex2].timestamp;
            (rightHandHighestPointPosition, rightHandHighestPointTimestamp) = GetHighestPointAndTimestamp(positions2, startIndex2, endIndex2);
        }

        // 高亮路径的最高点、开始点和结束点
        if (highlightHighestPointEnabled)
        {
            HighlightPoint(leftHandHighlightPoint, leftHandHighestPointPosition);
            HighlightPoint(rightHandHighlightPoint, rightHandHighestPointPosition);
            HighlightPoint(leftHandStartPoint, leftHandStartPointPosition);
            HighlightPoint(rightHandStartPoint, rightHandStartPointPosition);
            HighlightPoint(leftHandEndPoint, leftHandEndPointPosition);
            HighlightPoint(rightHandEndPoint, rightHandEndPointPosition);
        }
        else
        {
            if (leftHandHighlightPoint != null)
            {
                leftHandHighlightPoint.SetActive(false);
            }

            if (rightHandHighlightPoint != null)
            {
                rightHandHighlightPoint.SetActive(false);
            }

            if (leftHandStartPoint != null)
            {
                leftHandStartPoint.SetActive(false);
            }

            if (rightHandStartPoint != null)
            {
                rightHandStartPoint.SetActive(false);
            }

            if (leftHandEndPoint != null)
            {
                leftHandEndPoint.SetActive(false);
            }

            if (rightHandEndPoint != null)
            {
                rightHandEndPoint.SetActive(false);
            }
        }
    }

    private bool IsLeftHandHit(TransformPlayBacker.TransformData data)
    {
        return (data.bassDrumHit.limb == "lefthand" && data.bassDrumHit.value > 0) ||
               (data.snareDrumHit.limb == "lefthand" && data.snareDrumHit.value > 0) ||
               (data.closedHiHatHit.limb == "lefthand" && data.closedHiHatHit.value > 0) ||
               (data.tom1Hit.limb == "lefthand" && data.tom1Hit.value > 0) ||
               (data.tom2Hit.limb == "lefthand" && data.tom2Hit.value > 0) ||
               (data.floorTomHit.limb == "lefthand" && data.floorTomHit.value > 0) ||
               (data.crashHit.limb == "lefthand" && data.crashHit.value > 0) ||
               (data.rideHit.limb == "lefthand" && data.rideHit.value > 0) ||
               (data.openHiHatHit.limb == "lefthand" && data.openHiHatHit.value > 0);
    }

    private bool IsRightHandHit(TransformPlayBacker.TransformData data)
    {
        return (data.bassDrumHit.limb == "righthand" && data.bassDrumHit.value > 0) ||
               (data.snareDrumHit.limb == "righthand" && data.snareDrumHit.value > 0) ||
               (data.closedHiHatHit.limb == "righthand" && data.closedHiHatHit.value > 0) ||
               (data.tom1Hit.limb == "righthand" && data.tom1Hit.value > 0) ||
               (data.tom2Hit.limb == "righthand" && data.tom2Hit.value > 0) ||
               (data.floorTomHit.limb == "righthand" && data.floorTomHit.value > 0) ||
               (data.crashHit.limb == "righthand" && data.crashHit.value > 0) ||
               (data.rideHit.limb == "righthand" && data.rideHit.value > 0) ||
               (data.openHiHatHit.limb == "righthand" && data.openHiHatHit.value > 0);
    }

    private (Vector3, float) GetHighestPointAndTimestamp(List<Vector3> positions, int startIndex, int endIndex)
    {
        Vector3 highestPoint = positions[0];
        float highestTimestamp = transformPlayBacker.playbackData.dataList[startIndex].timestamp;

        for (int i = 0; i < positions.Count; i++)
        {
            if (positions[i].y > highestPoint.y)
            {
                highestPoint = positions[i];
                highestTimestamp = transformPlayBacker.playbackData.dataList[startIndex + i].timestamp;
            }
        }
        return (highestPoint, highestTimestamp);
    }

    private void HighlightPoint(GameObject highlight, Vector3 position)
    {
        if (highlight != null)
        {
            highlight.transform.position = position;
            highlight.SetActive(true);
        }
    }
}
