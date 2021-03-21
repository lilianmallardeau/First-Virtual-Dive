using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeManager : MonoBehaviour
{
    [SerializeField] private GameObject popAlerte;
    [SerializeField] private Text alertText;
    
    [SerializeField] private HealthBar oxygenBar;
    [SerializeField] private HealthBar coldBar;
    [SerializeField] private HealthBar oxygenBar2;
    [SerializeField] private HealthBar coldBar2;

    public int currentLayer = 0;
    public float oxygenLevel = 100f;
    public float coldLevel = 100f;
    public bool oxygenMid = false;
    public bool oxygenLow = false;
    public bool oxygenVeryLow = false;
    public bool isVeryCold = false;

    public void Start()
    {
        StartCoroutine(Live());
    }

    public void Update()
    {
        if (oxygenLevel <= 35 && !oxygenMid)
        {
            oxygenMid = true;
            alertText.text = "Votre niveau d'Oxygène est à mi-pression.\n N'oubliez pas de prévenir votre accompagnateur.";
            popAlerte.gameObject.SetActive(true);
        }
        if (oxygenLevel <= 15 && !oxygenLow)
        {
            oxygenLow = true;
            alertText.text = "Votre niveau d'Oxygène commence à être bas.\n N'oubliez pas de prévenir votre accompagnateur.";
            popAlerte.gameObject.SetActive(true);
        } 
        if (oxygenLevel <= 5 && !oxygenVeryLow)
        {
            oxygenVeryLow = true;
            alertText.text = "Votre niveau d'Oxygène est très bas !\n Prévenez votre accompagnateur au plus vite !";
            popAlerte.gameObject.SetActive(true);
        }
        if (coldLevel <= 5 && !isVeryCold)
        {
            isVeryCold = true;
            alertText.text = "Vous avez froid !\n Prévenez votre accompagnateur au plus vite !";
            popAlerte.gameObject.SetActive(true);
        }
    }

    private IEnumerator Live()
    {
        float time1 = 0;
        float time3 = 0;
        float modifier1 = Random.Range(1.2f,1.8f);
        float modifier2 = 1f;
        float modifier3 = Random.Range(1.2f,1.8f);
        float startValue = 100f;

        while (time1 < 1800 && time3 < 1800)
        {
            if (time1 < 1800)
            {
                oxygenLevel = Mathf.Lerp(startValue, 0f, time1 / 1800);
            }
            if (time3 < 1800)
            {
                coldLevel = Mathf.Lerp(startValue, 0f, time3 / 1800);
            }
            
            switch (currentLayer) {
                case 0:
                    modifier2 = 1f;
                    break;
                case 1:
                    modifier2 = 1.5f;
                    break;
                case 2:
                    modifier2 = 2f;
                    break;
                default:
                    break;
            }
            time1 += Time.deltaTime * modifier1 * modifier2;
            time3 += Time.deltaTime * modifier3 * modifier2;

            oxygenBar.HealthChange(oxygenLevel);
            coldBar.HealthChange(coldLevel);
            oxygenBar2.HealthChange(oxygenLevel);
            coldBar2.HealthChange(coldLevel);
            yield return null;
        }
        oxygenLevel = 0f;
        coldLevel = 0f;
    }

}
