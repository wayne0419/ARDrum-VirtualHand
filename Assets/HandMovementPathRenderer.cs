using System.Collections.Generic;
using UnityEngine;

public class HandMovementPathRenderer : MonoBehaviour
{
    public TransformPlayBacker transformPlayBacker;
    public LineRenderer lineRenderer1;
    public LineRenderer lineRenderer2;
    public Material lineMaterial;
    public float lineWidth = 0.1f;
    public float preTime = 1f;
    public float postTime = 1f;

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
        lineRenderer.material = lineMaterial;
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

        float currentTime = transformPlayBacker.playbackData.dataList[transformPlayBacker.currentIndex].timestamp;

        foreach (var data in transformPlayBacker.playbackData.dataList)
        {
            if (data.timestamp >= currentTime - preTime && data.timestamp <= currentTime + postTime)
            {
                Vector3 leftStickTipPosition = data.position1 + data.rotation1 * LeftHandDrumStickTipAnchor.localPosition;
                positions1.Add(leftStickTipPosition);

                Vector3 rightStickTipPosition = data.position2 + data.rotation2 * RightHandDrumStickTipAnchor.localPosition;
                positions2.Add(rightStickTipPosition);
            }
        }

        lineRenderer1.positionCount = positions1.Count;
        lineRenderer1.SetPositions(positions1.ToArray());

        lineRenderer2.positionCount = positions2.Count;
        lineRenderer2.SetPositions(positions2.ToArray());
    }
}
