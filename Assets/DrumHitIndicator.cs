using System.Collections;
using UnityEngine;

public class DrumHitIndicator : MonoBehaviour
{
    [System.Serializable]
    public class DrumIndicator
    {
        public GameObject indicator; // 用于显示效果的 GameObject
        public float fadeDuration = 0.5f; // 控制渐变回透明的速度
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

    public Color leftHandHitColor = Color.red;
    public Color rightHandHitColor = Color.blue;
    public Color defaultHitColor = Color.white;

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

    public void TriggerBassDrum(string limb)
    {
        if (bassDrumCoroutine != null) StopCoroutine(bassDrumCoroutine);
        bassDrumCoroutine = StartCoroutine(IndicatorEffect(bassDrumIndicator, limb));
    }

    public void TriggerSnareDrum(string limb)
    {
        if (snareDrumCoroutine != null) StopCoroutine(snareDrumCoroutine);
        snareDrumCoroutine = StartCoroutine(IndicatorEffect(snareDrumIndicator, limb));
    }

    public void TriggerClosedHiHat(string limb)
    {
        if (closedHiHatCoroutine != null) StopCoroutine(closedHiHatCoroutine);
        closedHiHatCoroutine = StartCoroutine(IndicatorEffect(closedHiHatIndicator, limb));
    }

    public void TriggerTom1(string limb)
    {
        if (tom1Coroutine != null) StopCoroutine(tom1Coroutine);
        tom1Coroutine = StartCoroutine(IndicatorEffect(tom1Indicator, limb));
    }

    public void TriggerTom2(string limb)
    {
        if (tom2Coroutine != null) StopCoroutine(tom2Coroutine);
        tom2Coroutine = StartCoroutine(IndicatorEffect(tom2Indicator, limb));
    }

    public void TriggerFloorTom(string limb)
    {
        if (floorTomCoroutine != null) StopCoroutine(floorTomCoroutine);
        floorTomCoroutine = StartCoroutine(IndicatorEffect(floorTomIndicator, limb));
    }

    public void TriggerCrash(string limb)
    {
        if (crashCoroutine != null) StopCoroutine(crashCoroutine);
        crashCoroutine = StartCoroutine(IndicatorEffect(crashIndicator, limb));
    }

    public void TriggerRide(string limb)
    {
        if (rideCoroutine != null) StopCoroutine(rideCoroutine);
        rideCoroutine = StartCoroutine(IndicatorEffect(rideIndicator, limb));
    }

    public void TriggerOpenHiHat(string limb)
    {
        if (openHiHatCoroutine != null) StopCoroutine(openHiHatCoroutine);
        openHiHatCoroutine = StartCoroutine(IndicatorEffect(openHiHatIndicator, limb));
    }

    private IEnumerator IndicatorEffect(DrumIndicator drumIndicator, string limb)
    {
        SetMaterialColor(drumIndicator.indicator, GetHitColor(limb));
        SetMaterialTransparency(drumIndicator.indicator, 1f);
        for (float t = 0f; t < 1f; t += Time.deltaTime / drumIndicator.fadeDuration)
        {
            SetMaterialTransparency(drumIndicator.indicator, Mathf.Lerp(1f, 0f, t));
            yield return null;
        }
        SetMaterialTransparency(drumIndicator.indicator, 0f);
    }

    private void SetMaterialTransparency(GameObject indicator, float alpha)
    {
        if (indicator != null)
        {
            Material material = indicator.GetComponent<Renderer>().material;
            if (material != null)
            {
                Color color = material.color;
                color.a = alpha;
                material.color = color;
            }
        }
    }

    private void SetMaterialColor(GameObject indicator, Color color)
    {
        if (indicator != null)
        {
            Material material = indicator.GetComponent<Renderer>().material;
            if (material != null)
            {
                material.color = color;
            }
        }
    }

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
