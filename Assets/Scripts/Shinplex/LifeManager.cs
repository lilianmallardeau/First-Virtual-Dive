using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeManager : MonoBehaviour
{
    [SerializeField] private Canvas popAlerte;
    [SerializeField] private Text alertText;
    
    [SerializeField] private HealthBar oxygenBar;
    [SerializeField] private HealthBar coldBar;

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
        if (oxygenLevel <= 35 && !oxygenLow)
        {
            oxygenLow = true;
            alertText.text = "Votre niveau d'Oxygène est bas !\n Pensez à prévenir votre accompagnateur.";
            popAlerte.gameObject.SetActive(true);
        }
        if (oxygenLevel <= 10 && !oxygenVeryLow)
        {
            oxygenVeryLow = true;
            alertText.text = "Votre niveau d'Oxygène est extrêmement bas !\n Prévenez votre accompagnateur au plus vite !";
            popAlerte.gameObject.SetActive(true);
        } 
        if (pressureLevel <= 35 && !pressureHigh)
        {
            pressureHigh = true;
            alertText.text = "La pression dans vos oreilles est élevée !\n Pensez à prévenir votre accompagnateur.";
            popAlerte.gameObject.SetActive(true);
        }
        if (pressureLevel <= 10 && !pressureVeryHigh)
        {
            pressureVeryHigh = true;
            alertText.text = "La pression dans vos oreilles est extrêmement élevée !\n Prévenez votre accompagnateur au plus vite !";
            popAlerte.gameObject.SetActive(true);
        }
        if (coldLevel <= 35 && !isCold)
        { 
            isCold = true;
            alertText.text = "Vous commencez à avoir froid !\n Pensez à prévenir votre accompagnateur.";
            popAlerte.gameObject.SetActive(true);
        }
        if (coldLevel <= 10 && !isVeryCold)
        {
            isVeryCold = true;
            alertText.text = "Vous avez très froid !\n Prévenez votre accompagnateur au plus vite !";
            popAlerte.gameObject.SetActive(true);
        }
    }

    private IEnumerator Live()
    {
        float time1 = 0;
        float time2 = 0;
        float time3 = 0;
        float modifier1 = Random.Range(0.8f,1.8f);
        float modifier2 = Random.Range(0.8f,1.8f);
        float modifier3 = Random.Range(0.8f,1.8f);
        float startValue = 100f;

        while (time1 < 1800 && time2 < 1800 && time3 < 1800)
        {
            if (time1 < 1800)
            {
                oxygenLevel = Mathf.Lerp(startValue, 0f, time1 / 1800);
            }
            if (time2 < 1800)
            {
                pressureLevel = Mathf.Lerp(startValue, 0f, time1 / 1800);
            }
            if (time3 < 1800)
            {
                coldLevel = Mathf.Lerp(startValue, 0f, time1 / 1800);
            }
            
            time1 += Time.deltaTime * modifier1;
            time2 += Time.deltaTime * modifier2;
            time3 += Time.deltaTime * modifier3;

            oxygenBar.HealthChange(oxygenLevel);
            coldBar.HealthChange(coldLevel);
            yield return null;
        }
        oxygenLevel = 0f;
        pressureLevel = 0f;
        coldLevel = 0f;
    }

}
