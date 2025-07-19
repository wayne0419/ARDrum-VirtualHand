using UnityEngine;

public class HandMovementFeedback : MonoBehaviour
{
    public float positionTolerance = 0.1f; // The maximum allowed distance error for a position to be considered "correct".
    public float timeTolerance = 0.1f;     // The maximum allowed time error for a timestamp to be considered "correct".
    public Transform userLeftDrumStickTip;  // Reference to the Transform of the user's left drumstick tip.
    public Transform userRightDrumStickTip; // Reference to the Transform of the user's right drumstick tip.
    public TransformPlayBacker transformPlayBacker; // Reference to the TransformPlayBacker for playback data and current time.
    public HandMovementPathRenderer handMovementPathRenderer; // Reference to the HandMovementPathRenderer for target points.

    private Renderer leftHandHighPointRenderer;    // Renderer for the visual indicator of the left hand's highest point.
    private Renderer rightHandHighPointRenderer;   // Renderer for the visual indicator of the right hand's highest point.
    private Renderer leftHandStartPointRenderer;   // Renderer for the visual indicator of the left hand's start point.
    private Renderer rightHandStartPointRenderer;  // Renderer for the visual indicator of the right hand's start point.
    private Renderer leftHandEndPointRenderer;     // Renderer for the visual indicator of the left hand's end point.
    private Renderer rightHandEndPointRenderer;    // Renderer for the visual indicator of the right hand's end point.
    private Color leftHandOriginalColor;           // Original color of the left hand high point indicator.
    private Color rightHandOriginalColor;          // Original color of the right hand high point indicator.
    private Color leftHandStartOriginalColor;      // Original color of the left hand start point indicator.
    private Color rightHandStartOriginalColor;     // Original color of the right hand start point indicator.
    private Color leftHandEndOriginalColor;        // Original color of the left hand end point indicator.
    private Color rightHandEndOriginalColor;       // Original color of the right hand end point indicator.

    private void Start()
    {
        // Initialize renderers and store original colors for all hand movement indicators.
        if (handMovementPathRenderer.leftHandHighPoint != null)
        {
            leftHandHighPointRenderer = handMovementPathRenderer.leftHandHighPoint.GetComponent<Renderer>();
            leftHandOriginalColor = leftHandHighPointRenderer.material.color;
        }

        if (handMovementPathRenderer.rightHandHighPoint != null)
        {
            rightHandHighPointRenderer = handMovementPathRenderer.rightHandHighPoint.GetComponent<Renderer>();
            rightHandOriginalColor = rightHandHighPointRenderer.material.color;
        }

        if (handMovementPathRenderer.leftHandStartPoint != null)
        {
            leftHandStartPointRenderer = handMovementPathRenderer.leftHandStartPoint.GetComponent<Renderer>();
            leftHandStartOriginalColor = leftHandStartPointRenderer.material.color;
        }

        if (handMovementPathRenderer.rightHandStartPoint != null)
        {
            rightHandStartPointRenderer = handMovementPathRenderer.rightHandStartPoint.GetComponent<Renderer>();
            rightHandStartOriginalColor = rightHandStartPointRenderer.material.color;
        }

        if (handMovementPathRenderer.leftHandEndPoint != null)
        {
            leftHandEndPointRenderer = handMovementPathRenderer.leftHandEndPoint.GetComponent<Renderer>();
            leftHandEndOriginalColor = leftHandEndPointRenderer.material.color;
        }

        if (handMovementPathRenderer.rightHandEndPoint != null)
        {
            rightHandEndPointRenderer = handMovementPathRenderer.rightHandEndPoint.GetComponent<Renderer>();
            rightHandEndOriginalColor = rightHandEndPointRenderer.material.color;
        }
    }

    private void Update()
    {
        // Provide feedback only when the playback is active.
        if (transformPlayBacker.isPlaying)
        {
            // Get the current timestamp from the playback data.
            float currentTime = transformPlayBacker.playbackData.dataList[transformPlayBacker.currentIndex].timestamp;
            
            // Check the user's left and right hand positions against the target high points.
            CheckHandPosition(userLeftDrumStickTip, handMovementPathRenderer.leftHandHighestPointPosition, handMovementPathRenderer.leftHandHighestPointTimestamp, leftHandHighPointRenderer, currentTime);
            CheckHandPosition(userRightDrumStickTip, handMovementPathRenderer.rightHandHighestPointPosition, handMovementPathRenderer.rightHandHighestPointTimestamp, rightHandHighPointRenderer, currentTime);

            // Check the user's left and right hand positions against the target start points.
            CheckHandPosition(userLeftDrumStickTip, handMovementPathRenderer.leftHandStartPointPosition, handMovementPathRenderer.leftHandStartPointTimestamp, leftHandStartPointRenderer, currentTime);
            CheckHandPosition(userRightDrumStickTip, handMovementPathRenderer.rightHandStartPointPosition, handMovementPathRenderer.rightHandStartPointTimestamp, rightHandStartPointRenderer, currentTime);

            // Check the user's left and right hand positions against the target end points.
            CheckHandPosition(userLeftDrumStickTip, handMovementPathRenderer.leftHandEndPointPosition, handMovementPathRenderer.leftHandEndPointTimestamp, leftHandEndPointRenderer, currentTime);
            CheckHandPosition(userRightDrumStickTip, handMovementPathRenderer.rightHandEndPointPosition, handMovementPathRenderer.rightHandEndPointTimestamp, rightHandEndPointRenderer, currentTime);

            // Reset indicator colors to their original state when a drum hit occurs for the respective hand.
            if (IsLeftHandHit(transformPlayBacker.playbackData.dataList[transformPlayBacker.currentIndex]))
            {
                ResetColor(leftHandHighPointRenderer, leftHandOriginalColor);
                ResetColor(leftHandStartPointRenderer, leftHandStartOriginalColor);
                ResetColor(leftHandEndPointRenderer, leftHandEndOriginalColor);
            }

            if (IsRightHandHit(transformPlayBacker.playbackData.dataList[transformPlayBacker.currentIndex]))
            {
                ResetColor(rightHandHighPointRenderer, rightHandOriginalColor);
                ResetColor(rightHandStartPointRenderer, rightHandStartOriginalColor);
                ResetColor(rightHandEndPointRenderer, rightHandEndOriginalColor);
            }
        }
    }

    /// <summary>
    /// Checks if the user's hand position is within tolerance of a target position at a specific timestamp.
    /// If both conditions are met, the indicator's color is set to green.
    /// </summary>
    /// <param name="userHand">The Transform of the user's drumstick tip.</param>
    /// <param name="targetPosition">The target Vector3 position for the hand.</param>
    /// <param name="targetTimestamp">The target timestamp for the hand position.</param>
    /// <param name="pointRenderer">The Renderer of the visual indicator to change color.</param>
    /// <param name="currentTime">The current playback timestamp.</param>
    private void CheckHandPosition(Transform userHand, Vector3 targetPosition, float targetTimestamp, Renderer pointRenderer, float currentTime)
    {
        if (pointRenderer == null)
        {
            return; // Exit if the renderer is not assigned.
        }

        // Check if the current time is within the allowed time tolerance of the target timestamp.
        if (Mathf.Abs(currentTime - targetTimestamp) <= timeTolerance)
        {
            // If within time tolerance, check if the user's hand position is within the allowed position tolerance.
            if (Vector3.Distance(userHand.position, targetPosition) <= positionTolerance)
            {
                pointRenderer.material.color = Color.green; // Indicate a correct position and time.
            }
        }
    }

    /// <summary>
    /// Resets the color of a given renderer to its original color.
    /// </summary>
    /// <param name="pointRenderer">The Renderer whose color needs to be reset.</param>
    /// <param name="originalColor">The color to reset to.</param>
    private void ResetColor(Renderer pointRenderer, Color originalColor)
    {
        if (pointRenderer != null)
        {
            pointRenderer.material.color = originalColor;
        }
    }

    /// <summary>
    /// Determines if a left-hand drum hit occurred in the given TransformData.
    /// </summary>
    /// <param name="data">The TransformData object containing hit information.</param>
    /// <returns>True if any left-hand drum hit is detected, false otherwise.</returns>
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

    /// <summary>
    /// Determines if a right-hand drum hit occurred in the given TransformData.
    /// </summary>
    /// <param name="data">The TransformData object containing hit information.</param>
    /// <returns>True if any right-hand drum hit is detected, false otherwise.</returns>
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