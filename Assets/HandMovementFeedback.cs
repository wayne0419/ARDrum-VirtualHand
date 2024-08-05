using UnityEngine;

public class HandMovementFeedback : MonoBehaviour
{
    public float positionTolerance = 0.1f;
    public float timeTolerance = 0.1f;
    public Transform userLeftDrumStickTip;
    public Transform userRightDrumStickTip;
    public TransformPlayBacker transformPlayBacker;
    public HandMovementPathRenderer handMovementPathRenderer;

    private Renderer leftHandHighPointRenderer;
    private Renderer rightHandHighPointRenderer;
    private Renderer leftHandStartPointRenderer;
    private Renderer rightHandStartPointRenderer;
    private Renderer leftHandEndPointRenderer;
    private Renderer rightHandEndPointRenderer;
    private Color leftHandOriginalColor;
    private Color rightHandOriginalColor;
    private Color leftHandStartOriginalColor;
    private Color rightHandStartOriginalColor;
    private Color leftHandEndOriginalColor;
    private Color rightHandEndOriginalColor;

    private void Start()
    {
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
        if (transformPlayBacker.isPlaying)
        {
            float currentTime = transformPlayBacker.playbackData.dataList[transformPlayBacker.currentIndex].timestamp;
            
            CheckHandPosition(userLeftDrumStickTip, handMovementPathRenderer.leftHandHighestPointPosition, handMovementPathRenderer.leftHandHighestPointTimestamp, leftHandHighPointRenderer, currentTime);
            CheckHandPosition(userRightDrumStickTip, handMovementPathRenderer.rightHandHighestPointPosition, handMovementPathRenderer.rightHandHighestPointTimestamp, rightHandHighPointRenderer, currentTime);

            CheckHandPosition(userLeftDrumStickTip, handMovementPathRenderer.leftHandStartPointPosition, handMovementPathRenderer.leftHandStartPointTimestamp, leftHandStartPointRenderer, currentTime);
            CheckHandPosition(userRightDrumStickTip, handMovementPathRenderer.rightHandStartPointPosition, handMovementPathRenderer.rightHandStartPointTimestamp, rightHandStartPointRenderer, currentTime);

            CheckHandPosition(userLeftDrumStickTip, handMovementPathRenderer.leftHandEndPointPosition, handMovementPathRenderer.leftHandEndPointTimestamp, leftHandEndPointRenderer, currentTime);
            CheckHandPosition(userRightDrumStickTip, handMovementPathRenderer.rightHandEndPointPosition, handMovementPathRenderer.rightHandEndPointTimestamp, rightHandEndPointRenderer, currentTime);

            // Reset colors when a hit occurs
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

    private void CheckHandPosition(Transform userHand, Vector3 targetPosition, float targetTimestamp, Renderer pointRenderer, float currentTime)
    {
        if (pointRenderer == null)
        {
            return;
        }

        if (Mathf.Abs(currentTime - targetTimestamp) <= timeTolerance)
        {
            if (Vector3.Distance(userHand.position, targetPosition) <= positionTolerance)
            {
                pointRenderer.material.color = Color.green;
            }
        }
    }

    private void ResetColor(Renderer pointRenderer, Color originalColor)
    {
        if (pointRenderer != null)
        {
            pointRenderer.material.color = originalColor;
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
}
