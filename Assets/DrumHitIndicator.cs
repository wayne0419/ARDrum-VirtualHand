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

    public void TriggerBassDrum()
    {
        if (bassDrumCoroutine != null) StopCoroutine(bassDrumCoroutine);
        bassDrumCoroutine = StartCoroutine(IndicatorEffect(bassDrumIndicator));
    }

    public void TriggerSnareDrum()
    {
        if (snareDrumCoroutine != null) StopCoroutine(snareDrumCoroutine);
        snareDrumCoroutine = StartCoroutine(IndicatorEffect(snareDrumIndicator));
    }

    public void TriggerClosedHiHat()
    {
        if (closedHiHatCoroutine != null) StopCoroutine(closedHiHatCoroutine);
        closedHiHatCoroutine = StartCoroutine(IndicatorEffect(closedHiHatIndicator));
    }

    public void TriggerTom1()
    {
        if (tom1Coroutine != null) StopCoroutine(tom1Coroutine);
        tom1Coroutine = StartCoroutine(IndicatorEffect(tom1Indicator));
    }

    public void TriggerTom2()
    {
        if (tom2Coroutine != null) StopCoroutine(tom2Coroutine);
        tom2Coroutine = StartCoroutine(IndicatorEffect(tom2Indicator));
    }

    public void TriggerFloorTom()
    {
        if (floorTomCoroutine != null) StopCoroutine(floorTomCoroutine);
        floorTomCoroutine = StartCoroutine(IndicatorEffect(floorTomIndicator));
    }

    public void TriggerCrash()
    {
        if (crashCoroutine != null) StopCoroutine(crashCoroutine);
        crashCoroutine = StartCoroutine(IndicatorEffect(crashIndicator));
    }

    public void TriggerRide()
    {
        if (rideCoroutine != null) StopCoroutine(rideCoroutine);
        rideCoroutine = StartCoroutine(IndicatorEffect(rideIndicator));
    }

    public void TriggerOpenHiHat()
    {
        if (openHiHatCoroutine != null) StopCoroutine(openHiHatCoroutine);
        openHiHatCoroutine = StartCoroutine(IndicatorEffect(openHiHatIndicator));
    }

    private IEnumerator IndicatorEffect(DrumIndicator drumIndicator)
    {
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
}
