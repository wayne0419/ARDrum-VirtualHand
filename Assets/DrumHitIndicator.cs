using System.Collections;
using UnityEngine;

public class DrumHitIndicator : MonoBehaviour
{
    [System.Serializable]
    public class DrumIndicator
    {
        public GameObject indicator; // The GameObject representing the visual indicator for the drum part.
        public float fadeDuration = 0.5f; // Duration in seconds for the indicator to fade back to transparent.
    }

    public DrumIndicator bassDrumIndicator;
    public DrumIndicator snareDrumIndicator;
    public DrumIndicator closedHiHatIndicator;
    public DrumIndicator tom1Indicator;
    public DrumIndicator tom2Indicator;
    public DrumIndicator floorTomIndicator;
    public DrumIndicator crashIndicator;
    public DrumIndicator rideIndicator;
    public DrumIndicator openHiHatIndicator;

    public Color leftHandHitColor = Color.red;   // Color to apply when a left-hand hit is detected.
    public Color rightHandHitColor = Color.blue;  // Color to apply when a right-hand hit is detected.
    public Color defaultHitColor = Color.white;   // Default color for hits if limb is not specified or recognized.

    private Coroutine bassDrumCoroutine;
    private Coroutine snareDrumCoroutine;
    private Coroutine closedHiHatCoroutine;
    private Coroutine tom1Coroutine;
    private Coroutine tom2Coroutine;
    private Coroutine floorTomCoroutine;
    private Coroutine crashCoroutine;
    private Coroutine rideCoroutine;
    private Coroutine openHiHatCoroutine;

    void Start()
    {
        Initialize();
    }

    /// <summary>
    /// Initializes all drum indicators by setting their material transparency to fully transparent.
    /// This ensures they are invisible at the start.
    /// </summary>
    public void Initialize()
    {
        SetMaterialTransparency(bassDrumIndicator.indicator, 0f);
        SetMaterialTransparency(snareDrumIndicator.indicator, 0f);
        SetMaterialTransparency(closedHiHatIndicator.indicator, 0f);
        SetMaterialTransparency(tom1Indicator.indicator, 0f);
        SetMaterialTransparency(tom2Indicator.indicator, 0f);
        SetMaterialTransparency(floorTomIndicator.indicator, 0f);
        SetMaterialTransparency(crashIndicator.indicator, 0f);
        SetMaterialTransparency(rideIndicator.indicator, 0f);
        SetMaterialTransparency(openHiHatIndicator.indicator, 0f);
    }

    /// <summary>
    /// Triggers the visual indicator effect for the Bass Drum.
    /// Stops any ongoing effect for this drum before starting a new one.
    /// </summary>
    /// <param name="limb">The limb used to hit the drum (e.g., "lefthand", "righthand").</param>
    public void TriggerBassDrum(string limb)
    {
        if (bassDrumCoroutine != null) StopCoroutine(bassDrumCoroutine);
        bassDrumCoroutine = StartCoroutine(IndicatorEffect(bassDrumIndicator, limb));
    }

    /// <summary>
    /// Triggers the visual indicator effect for the Snare Drum.
    /// Stops any ongoing effect for this drum before starting a new one.
    /// </summary>
    /// <param name="limb">The limb used to hit the drum (e.g., "lefthand", "righthand").</param>
    public void TriggerSnareDrum(string limb)
    {
        if (snareDrumCoroutine != null) StopCoroutine(snareDrumCoroutine);
        snareDrumCoroutine = StartCoroutine(IndicatorEffect(snareDrumIndicator, limb));
    }

    /// <summary>
    /// Triggers the visual indicator effect for the Closed Hi-Hat.
    /// Stops any ongoing effect for this drum before starting a new one.
    /// </summary>
    /// <param name="limb">The limb used to hit the drum (e.g., "lefthand", "righthand").</param>
    public void TriggerClosedHiHat(string limb)
    {
        if (closedHiHatCoroutine != null) StopCoroutine(closedHiHatCoroutine);
        closedHiHatCoroutine = StartCoroutine(IndicatorEffect(closedHiHatIndicator, limb));
    }

    /// <summary>
    /// Triggers the visual indicator effect for Tom 1.
    /// Stops any ongoing effect for this drum before starting a new one.
    /// </summary>
    /// <param name="limb">The limb used to hit the drum (e.g., "lefthand", "righthand").</param>
    public void TriggerTom1(string limb)
    {
        if (tom1Coroutine != null) StopCoroutine(tom1Coroutine);
        tom1Coroutine = StartCoroutine(IndicatorEffect(tom1Indicator, limb));
    }

    /// <summary>
    /// Triggers the visual indicator effect for Tom 2.
    /// Stops any ongoing effect for this drum before starting a new one.
    /// </summary>
    /// <param name="limb">The limb used to hit the drum (e.g., "lefthand", "righthand").</param>
    public void TriggerTom2(string limb)
    {
        if (tom2Coroutine != null) StopCoroutine(tom2Coroutine);
        tom2Coroutine = StartCoroutine(IndicatorEffect(tom2Indicator, limb));
    }

    /// <summary>
    /// Triggers the visual indicator effect for the Floor Tom.
    /// Stops any ongoing effect for this drum before starting a new one.
    /// </summary>
    /// <param name="limb">The limb used to hit the drum (e.g., "lefthand", "righthand").</param>
    public void TriggerFloorTom(string limb)
    {
        if (floorTomCoroutine != null) StopCoroutine(floorTomCoroutine);
        floorTomCoroutine = StartCoroutine(IndicatorEffect(floorTomIndicator, limb));
    }

    /// <summary>
    /// Triggers the visual indicator effect for the Crash Cymbal.
    /// Stops any ongoing effect for this drum before starting a new one.
    /// </summary>
    /// <param name="limb">The limb used to hit the drum (e.g., "lefthand", "righthand").</param>
    public void TriggerCrash(string limb)
    {
        if (crashCoroutine != null) StopCoroutine(crashCoroutine);
        crashCoroutine = StartCoroutine(IndicatorEffect(crashIndicator, limb));
    }

    /// <summary>
    /// Triggers the visual indicator effect for the Ride Cymbal.
    /// Stops any ongoing effect for this drum before starting a new one.
    /// </summary>
    /// <param name="limb">The limb used to hit the drum (e.g., "lefthand", "righthand").</param>
    public void TriggerRide(string limb)
    {
        if (rideCoroutine != null) StopCoroutine(rideCoroutine);
        rideCoroutine = StartCoroutine(IndicatorEffect(rideIndicator, limb));
    }

    /// <summary>
    /// Triggers the visual indicator effect for the Open Hi-Hat.
    /// Stops any ongoing effect for this drum before starting a new one.
    /// </summary>
    /// <param name="limb">The limb used to hit the drum (e.g., "lefthand", "righthand").</param>
    public void TriggerOpenHiHat(string limb)
    {
        if (openHiHatCoroutine != null) StopCoroutine(openHiHatCoroutine);
        openHiHatCoroutine = StartCoroutine(IndicatorEffect(openHiHatIndicator, limb));
    }

    /// <summary>
    /// Coroutine to handle the visual effect of a drum hit:
    /// - Sets the indicator's color based on the hitting limb.
    /// - Makes the indicator fully visible.
    /// - Fades the indicator back to fully transparent over a specified duration.
    /// </summary>
    /// <param name="drumIndicator">The DrumIndicator object containing the indicator GameObject and fade duration.</param>
    /// <param name="limb">The string representing the limb that hit the drum.</param>
    private IEnumerator IndicatorEffect(DrumIndicator drumIndicator, string limb)
    {
        SetMaterialColor(drumIndicator.indicator, GetHitColor(limb));
        SetMaterialTransparency(drumIndicator.indicator, 1f); // Make fully visible instantly.
        for (float t = 0f; t < 1f; t += Time.deltaTime / drumIndicator.fadeDuration)
        {
            // Linearly interpolate transparency from 1 (fully visible) to 0 (fully transparent).
            SetMaterialTransparency(drumIndicator.indicator, Mathf.Lerp(1f, 0f, t));
            yield return null; // Wait for the next frame.
        }
        SetMaterialTransparency(drumIndicator.indicator, 0f); // Ensure it's fully transparent at the end.
    }

    /// <summary>
    /// Sets the alpha (transparency) of the material attached to the given indicator GameObject.
    /// Assumes the GameObject has a Renderer component and a material.
    /// </summary>
    /// <param name="indicator">The GameObject whose material transparency will be set.</param>
    /// <param name="alpha">The desired alpha value (0f for fully transparent, 1f for fully opaque).</param>
    private void SetMaterialTransparency(GameObject indicator, float alpha)
    {
        if (indicator != null)
        {
            Renderer renderer = indicator.GetComponent<Renderer>();
            if (renderer != null && renderer.material != null)
            {
                Color color = renderer.material.color;
                color.a = alpha;
                renderer.material.color = color;
            }
        }
    }

    /// <summary>
    /// Sets the base color of the material attached to the given indicator GameObject.
    /// Assumes the GameObject has a Renderer component and a material.
    /// </summary>
    /// <param name="indicator">The GameObject whose material color will be set.</param>
    /// <param name="color">The desired color to set, including its alpha component.</param>
    private void SetMaterialColor(GameObject indicator, Color color)
    {
        if (indicator != null)
        {
            Renderer renderer = indicator.GetComponent<Renderer>();
            if (renderer != null && renderer.material != null)
            {
                renderer.material.color = color;
            }
        }
    }

    /// <summary>
    /// Returns a predefined color based on the specified limb.
    /// </summary>
    /// <param name="limb">The string identifier for the limb (e.g., "lefthand", "righthand").</param>
    /// <returns>The color associated with the limb, or defaultHitColor if unrecognized.</returns>
    private Color GetHitColor(string limb)
    {
        switch (limb)
        {
            case "lefthand":
                return leftHandHitColor;
            case "righthand":
                return rightHandHitColor;
            default:
                return defaultHitColor;
        }
    }
}