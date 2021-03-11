using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ender : MonoBehaviour
{
    [SerializeField] private Entertainor entertainor;
    [SerializeField] private GameObject menuEnd;
    [SerializeField] private Text goodsN;
    [SerializeField] private Text okN;
    [SerializeField] private Text notokN;
    [SerializeField] private Text coldN;
    [SerializeField] private Text mipressionN;
    [SerializeField] private Text reserveN;
    [SerializeField] private Text noairN;
    [SerializeField] private Text totalN;
    [SerializeField] private Text noteN;

    public float timer = 0;

    public void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 900f) {
            timer = -1000000f;
            StartCoroutine(endCoroutine());
        }
    }

    public IEnumerator endCoroutine()
    {
        menuEnd.SetActive(true);
        int totalGoods = entertainor.okGood + entertainor.notOkGood + entertainor.coldGood + entertainor.midPressureGood + entertainor.reserveGood + entertainor.noAirGood;
        int totalMistakes = entertainor.okMistakes + entertainor.notOkMistakes + entertainor.coldMistakes + entertainor.midPressureMistakes + entertainor.reserveMistakes + entertainor.noAirMistakes;
        yield return new WaitForSeconds(0.5f);
        
        for (int i = 0; i <= totalGoods; i++)
        {
            goodsN.text = i.ToString();
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(0.2f);

        for (int i = 0; i <= entertainor.okMistakes; i++)
        {
            okN.text = i.ToString();
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(0.2f);

        for (int i = 0; i <= entertainor.notOkMistakes; i++)
        {
            notokN.text = i.ToString();
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(0.2f);

        for (int i = 0; i <= entertainor.coldMistakes; i++)
        {
            coldN.text = i.ToString();
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(0.2f);

        for (int i = 0; i <= entertainor.midPressureMistakes; i++)
        {
            mipressionN.text = i.ToString();
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(0.2f);

        for (int i = 0; i <= entertainor.reserveMistakes; i++)
        {
            reserveN.text = i.ToString();
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(0.2f);

        for (int i = 0; i <= entertainor.noAirMistakes; i++)
        {
            noairN.text = i.ToString();
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(0.2f);

        for (int i = 0; i <= totalMistakes; i++)
        {
            totalN.text = i.ToString();
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(1f);

        switch(totalMistakes) {
            case 0:
                noteN.text = "A";
                break;
            case 1:
                noteN.text = "B";
                break;
            case 2:
                noteN.text = "B";
                break;
            case 3:
                noteN.text = "C";
                break;
            case 4:
                noteN.text = "C";
                break;
            case 5:
                noteN.text = "D";
                break;
            case 6:
                noteN.text = "D";
                break;
            default:
                noteN.text = "E";
                break; 
        }
        yield return null;
    }
}
