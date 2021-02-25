using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeManager : MonoBehaviour
{
    public float oxygenLevel = 100f;
    public float pressureLevel = 100f;
    public float coldLevel = 100f;

    private bool oxygenLow = false;
    private bool oxygenVeryLow = false;
    private bool pressureHigh = false;
    private bool pressureVeryHigh = false;
    private bool isCold = false;
    private bool isVeryCold = false;

    public void Start()
    {
        StartCoroutine(Live());
    }

    public void Update()
    {
        if (oxygenLevel <= 30) oxygenLow = true;
        if (oxygenLevel <= 10) oxygenVeryLow = true;
        if (pressureLevel <= 30) pressureHigh = true;
        if (pressureLevel <= 10) pressureVeryHigh = true;
        if (coldLevel <= 30) isCold = true;
        if (coldLevel <= 10) isVeryCold = true;
    }

    private IEnumerator Live()
    {
        float time = 0;
        float startValue = 100f;

        while (time < 1800)
        {
            oxygenLevel = Mathf.Lerp(startValue, 0f, time / 1800);
            pressureLevel = Mathf.Lerp(startValue, 0f, time / 1800);
            coldLevel = Mathf.Lerp(startValue, 0f, time / 1800);
            time += Time.deltaTime;
            yield return null;
        }
        oxygenLevel = 0f;
        pressureLevel = 0f;
        coldLevel = 0f;
    }

}
