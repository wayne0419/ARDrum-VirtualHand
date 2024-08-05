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
    public Color snareDrumColor = Color.red;
    public Color bassDrumColor = Color.blue;
    public Color closedHiHatColor = Color.green;
    public Color tom1Color = Color.yellow;
    public Color tom2Color = Color.magenta;
    public Color floorTomColor = Color.cyan;
    public Color crashColor = Color.grey;
    public Color rideColor = Color.white;
    public Color openHiHatColor = Color.black;

    private List<GameObject> points = new List<GameObject>();

    private void OnEnable()
    {
        if (reviewManager == null)
        {
            Debug.LogWarning("ReviewManager is not assigned.");
            return;
        }

        // 绘制每种鼓的打击点
        DrawDrumHits(reviewManager.userSnareDrumHits, snareDrumColor, 0);
        DrawDrumHits(reviewManager.userBassDrumHits, bassDrumColor, 1);
        DrawDrumHits(reviewManager.userClosedHiHatHits, closedHiHatColor, 2);
        DrawDrumHits(reviewManager.userTom1Hits, tom1Color, 3);
        DrawDrumHits(reviewManager.userTom2Hits, tom2Color, 4);
        DrawDrumHits(reviewManager.userFloorTomHits, floorTomColor, 5);
        DrawDrumHits(reviewManager.userCrashHits, crashColor, 6);
        DrawDrumHits(reviewManager.userRideHits, rideColor, 7);
        DrawDrumHits(reviewManager.userOpenHiHatHits, openHiHatColor, 8);

        DrawDrumHits(reviewManager.targetSnareDrumHits, snareDrumColor, 0.5f);
        DrawDrumHits(reviewManager.targetBassDrumHits, bassDrumColor, 1.5f);
        DrawDrumHits(reviewManager.targetClosedHiHatHits, closedHiHatColor, 2.5f);
        DrawDrumHits(reviewManager.targetTom1Hits, tom1Color, 3.5f);
        DrawDrumHits(reviewManager.targetTom2Hits, tom2Color, 4.5f);
        DrawDrumHits(reviewManager.targetFloorTomHits, floorTomColor, 5.5f);
        DrawDrumHits(reviewManager.targetCrashHits, crashColor, 6.5f);
        DrawDrumHits(reviewManager.targetRideHits, rideColor, 7.5f);
        DrawDrumHits(reviewManager.targetOpenHiHatHits, openHiHatColor, 8.5f);
    }

    private void DrawDrumHits(List<ReviewManager.TransformDataIndex> drumHits, Color color, float yIndex)
    {
        foreach (var hit in drumHits)
        {
            Vector3 position = new Vector3(hit.data.timestamp * timeScale, yOffset * yIndex, 0);
            GameObject point = Instantiate(pointPrefab, position, Quaternion.identity);
            point.GetComponent<Renderer>().material.color = color;
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
