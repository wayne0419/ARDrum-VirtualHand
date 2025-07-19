using System.Collections.Generic;
using UnityEngine;

public class HandMovementPathRenderer : MonoBehaviour
{
    public TransformPlayBacker transformPlayBacker; // Reference to the playback controller.
    public LineRenderer lineRenderer1; // LineRenderer for the left hand's movement path.
    public LineRenderer lineRenderer2; // LineRenderer for the right hand's movement path.
    public float lineWidth = 0.1f; // The width of the rendered path lines.
    public int preHitNumber = 5;   // Number of hit events to look back for defining the start of the path segment.
    public int postHitNumber = 5;  // Number of hit events to look forward for defining the end of the path segment.
    public int resolution = 5;     // The sampling interval for path points. A value of 5 means one point is sampled every 5 data frames.

    public Transform LeftHandDrumStickTipAnchor;  // Anchor to calculate the left drumstick tip position relative to the controller.
    public Transform RightHandDrumStickTipAnchor; // Anchor to calculate the right drumstick tip position relative to the controller.

    public GameObject leftHandHighPoint;   // The GameObject used as a visual marker for the left hand's highest point in the path segment.
    public GameObject rightHandHighPoint;  // The GameObject used as a visual marker for the right hand's highest point.
    public GameObject leftHandStartPoint;  // The GameObject used as a visual marker for the left hand's path start point.
    public GameObject rightHandStartPoint; // The GameObject used as a visual marker for the right hand's path start point.
    public GameObject leftHandEndPoint;    // The GameObject used as a visual marker for the left hand's path end point.
    public GameObject rightHandEndPoint;   // The GameObject used as a visual marker for the right hand's path end point.

    public bool highlightHighestPointEnabled = true; // Toggles the visibility of the key point markers (start, end, high point).

    // Stores the position and timestamp of the key points of the current path segment for external access (e.g., by HandMovementFeedback).
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

        // Initialize LineRenderers if they are not assigned.
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

        // Ensure the highlight GameObjects are assigned in the Inspector.
        if (leftHandHighPoint == null)
        {
            Debug.LogError("Left hand high point is not assigned.");
        }

        if (rightHandHighPoint == null)
        {
            Debug.LogError("Right hand high point is not assigned.");
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

        // Find the start index for the left-hand path by looking back `preHitNumber` hit events.
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

        // Find the end index for the left-hand path by looking forward `postHitNumber` hit events.
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

        // Find the start index for the right-hand path by looking back `preHitNumber` hit events.
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

        // Find the end index for the right-hand path by looking forward `postHitNumber` hit events.
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

        // Collect path points for the left hand based on the calculated range and resolution.
        for (int i = startIndex1; i <= endIndex1; i += resolution)
        {
            var data = transformPlayBacker.playbackData.dataList[i];
            Vector3 leftStickTipPosition = data.position1 + data.rotation1 * LeftHandDrumStickTipAnchor.localPosition;
            positions1.Add(leftStickTipPosition);
        }

        // Collect path points for the right hand.
        for (int i = startIndex2; i <= endIndex2; i += resolution)
        {
            var data = transformPlayBacker.playbackData.dataList[i];
            Vector3 rightStickTipPosition = data.position2 + data.rotation2 * RightHandDrumStickTipAnchor.localPosition;
            positions2.Add(rightStickTipPosition);
        }

        // Set the collected points for the LineRenderers to draw the paths.
        lineRenderer1.positionCount = positions1.Count;
        lineRenderer1.SetPositions(positions1.ToArray());

        lineRenderer2.positionCount = positions2.Count;
        lineRenderer2.SetPositions(positions2.ToArray());

        // Record the highest, start, and end points of the path segment.
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

        // Highlight the key points of the path (highest, start, end) if enabled.
        if (highlightHighestPointEnabled)
        {
            HighlightPoint(leftHandHighPoint, leftHandHighestPointPosition);
            HighlightPoint(rightHandHighPoint, rightHandHighestPointPosition);
            HighlightPoint(leftHandStartPoint, leftHandStartPointPosition);
            HighlightPoint(rightHandStartPoint, rightHandStartPointPosition);
            HighlightPoint(leftHandEndPoint, leftHandEndPointPosition);
            HighlightPoint(rightHandEndPoint, rightHandEndPointPosition);
        }
        else
        {
            // Deactivate highlight objects if highlighting is disabled.
            if (leftHandHighPoint != null)
            {
                leftHandHighPoint.SetActive(false);
            }

            if (rightHandHighPoint != null)
            {
                rightHandHighPoint.SetActive(false);
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