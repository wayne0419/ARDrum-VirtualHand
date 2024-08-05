using System.Collections.Generic;
using UnityEngine;

public class ReviewSheetDrawer : MonoBehaviour
{
    public ReviewManager reviewManager;

    // 线条渲染器设置
    public float timeScale = 1.0f; // 时间刻度，用于调整视觉化的时间间隔
    public float yOffset = 2.0f; // 每个鼓的垂直偏移量
    public float pointSize = 0.2f; // 打击点的大小
    public GameObject pointPrefab; // 预制体用于打击点

    // 用于视觉化的颜色
    public Color userColor = Color.blue; // 用户打击点的颜色
    public Color targetColor = Color.red; // 目标打击点的颜色

    private List<GameObject> points = new List<GameObject>();

    private void OnEnable()
    {
        if (reviewManager == null)
        {
            Debug.LogWarning("ReviewManager is not assigned.");
            return;
        }

        // 绘制用户打击点
        DrawDrumHits(reviewManager.userSnareDrumHits, userColor, 0);
        DrawDrumHits(reviewManager.userBassDrumHits, userColor, 1);
        DrawDrumHits(reviewManager.userClosedHiHatHits, userColor, 2);
        DrawDrumHits(reviewManager.userTom1Hits, userColor, 3);
        DrawDrumHits(reviewManager.userTom2Hits, userColor, 4);
        DrawDrumHits(reviewManager.userFloorTomHits, userColor, 5);
        DrawDrumHits(reviewManager.userCrashHits, userColor, 6);
        DrawDrumHits(reviewManager.userRideHits, userColor, 7);
        DrawDrumHits(reviewManager.userOpenHiHatHits, userColor, 8);

        // 绘制目标打击点
        DrawDrumHits(reviewManager.targetSnareDrumHits, targetColor, 0.5f);
        DrawDrumHits(reviewManager.targetBassDrumHits, targetColor, 1.5f);
        DrawDrumHits(reviewManager.targetClosedHiHatHits, targetColor, 2.5f);
        DrawDrumHits(reviewManager.targetTom1Hits, targetColor, 3.5f);
        DrawDrumHits(reviewManager.targetTom2Hits, targetColor, 4.5f);
        DrawDrumHits(reviewManager.targetFloorTomHits, targetColor, 5.5f);
        DrawDrumHits(reviewManager.targetCrashHits, targetColor, 6.5f);
        DrawDrumHits(reviewManager.targetRideHits, targetColor, 7.5f);
        DrawDrumHits(reviewManager.targetOpenHiHatHits, targetColor, 8.5f);
    }

    private void DrawDrumHits(List<ReviewManager.TransformDataIndex> drumHits, Color color, float yIndex)
    {
        foreach (var hit in drumHits)
        {
            Vector3 position = new Vector3(hit.data.timestamp * timeScale, yOffset * yIndex, 0);
            GameObject point = Instantiate(pointPrefab, position, Quaternion.identity, this.transform);
            point.GetComponent<Renderer>().material.color = color;
            point.transform.localScale = new Vector3(pointSize, pointSize, pointSize);
            points.Add(point);
        }
    }

    private void OnDisable()
    {
        foreach (var point in points)
        {
            Destroy(point);
        }
        points.Clear();
    }
}
