using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Ender : MonoBehaviour
{
    [SerializeField] private Entertainor entertainor;
    [SerializeField] private GameObject menuEnd;
    [SerializeField] private GameObject menuEnd2;
    [SerializeField] private GameObject menuEnd3;
    [SerializeField] private float minimalLength = 300f;
    [SerializeField] private Text messageFin1;
    [SerializeField] private Text lengthN;
    [SerializeField] private Text lengthN2;
    [SerializeField] private Text goodsN;
    [SerializeField] private Text okN;
    [SerializeField] private Text notokN;
    [SerializeField] private Text coldN;
    [SerializeField] private Text mipressionN;
    [SerializeField] private Text reserveN;
    [SerializeField] private Text noairN;
    [SerializeField] private Text totalN;
    [SerializeField] private Text noteN;
    [SerializeField] private Text forgetN;

    private double diveLength = 0f;
    private double finalLength = 0f;
    private bool won = false;

    public void Awake() {
        diveLength = 0f;
    }

    public void Update() {
        diveLength += Time.deltaTime;
    }

    public void Suivant()
    {
        // TODO : resoudre problème appuyer sur le bouton trop vite
        menuEnd.SetActive(false);
        menuEnd2.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(1);
    }

    public void Stop()
    {
        Application.Quit();
    }

    public IEnumerator endCoroutine()
    {
        finalLength = diveLength;
        TimeSpan span = TimeSpan.FromSeconds(finalLength);
        lengthN.text = span.ToString("m'm's's'");
        lengthN2.text = span.ToString("m'm's's'");

        if (finalLength >= minimalLength) {

            menuEnd.SetActive(true);
            int totalGoods = entertainor.okGood + entertainor.notOkGood + entertainor.coldGood + entertainor.midPressureGood + entertainor.reserveGood + entertainor.noAirGood;
            int totalMistakes = entertainor.okMistakes + entertainor.notOkMistakes + entertainor.coldMistakes + entertainor.midPressureMistakes + entertainor.reserveMistakes + entertainor.noAirMistakes + entertainor.forgotToAnswer;
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

            for (int i = 0; i <= entertainor.forgotToAnswer; i++)
            {
                forgetN.text = i.ToString();
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
                    won = true;
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
            if (won) messageFin1.text = "Félicitations ! Vous venez de réussir votre baptême de plongée virtuelle !\n\nVous êtes désormais paré pour passer votre vrai baptême !\n\nMerci d'avoir participé à First Virtual Dive !";
            else messageFin1.gameObject.SetActive(true);
        }

        else menuEnd3.SetActive(true);

        
        yield return null;
    }
}
