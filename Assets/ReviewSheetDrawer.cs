using System.Collections.Generic;
using UnityEngine;

public class ReviewSheetDrawer : MonoBehaviour
{
    public ReviewManager reviewManager; // Reference to the ReviewManager, which provides user and target hit data.
    public Transform reviewSheetContainer; // The parent Transform under which all visual hit points will be instantiated.

    // Visual settings for rendering the hit points.
    public float timeScale = 1.0f; // Multiplier for the timestamp to control horizontal spacing of points.
    public float yOffset = 2.0f;   // Vertical spacing between different drum rows.
    public float pointSizeX = 0.2f; // X-axis scale for each instantiated hit point.
    public float pointSizeY = 0.2f; // Y-axis scale for each instantiated hit point.
    public float pointSizeZ = 0.2f; // Z-axis scale for each instantiated hit point.
    public GameObject pointPrefab; // The prefab GameObject to instantiate for each drum hit point.

    // Colors for distinguishing user and target hits.
    public Color userColor = Color.blue;   // Color for the user's drum hit points.
    public Color targetColor = Color.red; // Color for the target's drum hit points.

    private List<GameObject> points = new List<GameObject>(); // A list to keep track of all instantiated hit point GameObjects.

    private void OnEnable()
    {
        // Validate essential references.
        if (reviewManager == null)
        {
            Debug.LogWarning("ReviewManager is not assigned. Cannot draw review sheet.");
            return;
        }

        if (reviewSheetContainer == null)
        {
            Debug.LogWarning("ReviewSheetContainer is not assigned. Cannot draw review sheet.");
            return;
        }

        // Draw the user's drum hits for each drum type.
        // The `yIndex` parameter provides a vertical offset to separate different drum types visually.
        DrawDrumHits(reviewManager.userSnareDrumHits, userColor, 0);
        DrawDrumHits(reviewManager.userBassDrumHits, userColor, 1);
        DrawDrumHits(reviewManager.userClosedHiHatHits, userColor, 2);
        DrawDrumHits(reviewManager.userTom1Hits, userColor, 3);
        DrawDrumHits(reviewManager.userTom2Hits, userColor, 4);
        DrawDrumHits(reviewManager.userFloorTomHits, userColor, 5);
        DrawDrumHits(reviewManager.userCrashHits, userColor, 6);
        DrawDrumHits(reviewManager.userRideHits, userColor, 7);
        DrawDrumHits(reviewManager.userOpenHiHatHits, userColor, 8);

        // Draw the target's drum hits for each drum type.
        // A slight offset (e.g., 0.5f) is added to `yIndex` to place target hits slightly above/below user hits on the same row.
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

    /// <summary>
    /// Instantiates and positions visual points for a given list of drum hits.
    /// </summary>
    /// <param name="drumHits">A list of `TransformDataIndex` representing drum hit events.</param>
    /// <param name="color">The color to apply to the instantiated points.</param>
    /// <param name="yIndex">A vertical index used to calculate the Y position of the points, separating drum types.</param>
    private void DrawDrumHits(List<ReviewManager.TransformDataIndex> drumHits, Color color, float yIndex)
    {
        foreach (var hit in drumHits)
        {
            // Calculate the local position for the point. X is based on timestamp and timeScale, Y on yIndex and yOffset.
            Vector3 position = new Vector3(hit.data.timestamp * timeScale, yOffset * yIndex, 0);
            
            // Instantiate the point prefab as a child of the reviewSheetContainer.
            GameObject point = Instantiate(pointPrefab, reviewSheetContainer);
            point.transform.localPosition = position; // Set local position relative to the container.
            
            // Set the color of the point's material.
            Renderer pointRenderer = point.GetComponent<Renderer>();
            if (pointRenderer != null)
            {
                pointRenderer.material.color = color;
            }
            
            // Set the scale of the point.
            point.transform.localScale = new Vector3(pointSizeX, pointSizeY, pointSizeZ);
            
            points.Add(point); // Add the instantiated point to the list for later cleanup.
        }
    }

    private void OnDisable()
    {
        // Destroy all instantiated points when the script is disabled to clean up the scene.
        foreach (var point in points)
        {
            Destroy(point);
        }
        points.Clear(); // Clear the list.
    }
}