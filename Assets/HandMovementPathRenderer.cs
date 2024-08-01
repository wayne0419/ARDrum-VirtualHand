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

    public Transform LeftHandDrumStickTipAnchor; // 新增 LeftHandDrumStickTipAnchor
    public Transform RightHandDrumStickTipAnchor; // 新增 RightHandDrumStickTipAnchor

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
            lineObj1.transform.parent = this.transform; // 将 LineRenderer1 添加为 HandMovementPathRenderer 的子对象
            lineRenderer1 = lineObj1.AddComponent<LineRenderer>();
        }
        InitializeLineRenderer(lineRenderer1);

        if (lineRenderer2 == null)
        {
            GameObject lineObj2 = new GameObject("LineRenderer2");
            lineObj2.transform.parent = this.transform; // 将 LineRenderer2 添加为 HandMovementPathRenderer 的子对象
            lineRenderer2 = lineObj2.AddComponent<LineRenderer>();
        }
        InitializeLineRenderer(lineRenderer2);
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

        lineRenderer1.positionCount = positions1.Count;
        lineRenderer1.SetPositions(positions1.ToArray());

        lineRenderer2.positionCount = positions2.Count;
        lineRenderer2.SetPositions(positions2.ToArray());
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
}
