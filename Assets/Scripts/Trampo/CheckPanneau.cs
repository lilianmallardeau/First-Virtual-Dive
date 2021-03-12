using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class CheckPanneau : MonoBehaviour
{
    [SerializeField] private Canvas panneau;
    [SerializeField] private Image background;
    [SerializeField] private AudioSource checksound;
    [SerializeField] private Text allreadyCheck;
    private Color checkColor = Color.green;

    private bool notCheck1 = true;
    private bool notCheck2 = true;
    private bool notCheck3 = true;
    private bool notCheck4 = true;
    private bool notCheck5 = true;
    private bool notCheck6 = true;
    private bool notCheck7 = true;
    private bool notCheck8 = true;
    private bool notCheck9 = true;

    public static int allCheck = 0;
    private bool fadeAway = true;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (allCheck == 8 && fadeAway) StartCoroutine(FadeImage());
    }

    public void checkOK()
    {
        if (RaycastScript.okHit)
        {
            if (notCheck1)
            {
                background.color = checkColor;
                checksound.Play();
                notCheck1 = false;
                allCheck += 1;
            }
            else
            {
                StartCoroutine(AllreadyCheck());
            }
        }
    }
    public void checkMonter()
    {
        if (RaycastScript.monterHit)
        {
            if (notCheck2)
            {
                background.color = checkColor;
                checksound.Play();
                allCheck += 1;
                notCheck2 = false;
            }
            else
            {
                StartCoroutine(AllreadyCheck());
            }

        }

    }
    public void checkDescendre()
    {
        if (RaycastScript.descendreHit)
        {
            if (notCheck3)
            {
                background.color = checkColor;
                checksound.Play();
                allCheck += 1;
                notCheck3 = false;
            }
            else
            {
                StartCoroutine(AllreadyCheck());
            }
        }
    }
    public void checkNotOK()
    {
        if (RaycastScript.notOkHit)
        {
            if (notCheck4)
            {
                background.color = checkColor;
                checksound.Play();
                allCheck += 1;
                notCheck4 = false;
            }
            else
            {
                StartCoroutine(AllreadyCheck());
            }
        }
    }
    public void checkCold()
    {
        if (RaycastScript.coldHit)
        {
            if (notCheck5)
            {
                background.color = checkColor;
                checksound.Play();
                allCheck += 1;
                notCheck5 = false;
            }
            else
            {
                StartCoroutine(AllreadyCheck());
            }
        }
    }
    public void checkMiPression()
    {
        if (RaycastScript.mipressionHit)
        {
            if (notCheck6)
            {
                background.color = checkColor;
                checksound.Play();
                allCheck += 1;
                notCheck6 = false;
            }
            else
            {
                StartCoroutine(AllreadyCheck());
            }
        }
    }
    public void checkReserve()
    {
        if (RaycastScript.reserveHit)
        {
            if (notCheck7)
            {
                background.color = checkColor;
                checksound.Play();
                allCheck += 1;
                notCheck7 = false;
            }
            else
            {
                StartCoroutine(AllreadyCheck());
            }
        }
    }

    public void checkPlusAir()
    {
        if (RaycastScript.plusAirHit)
        {
            if (notCheck8)
            {
                background.color = checkColor;
                checksound.Play();
                allCheck += 1;
                notCheck8 = false;
            }
            else
            {
                StartCoroutine(AllreadyCheck());
            }
        }
    }
    public void checkMenu()
    {
        if (RaycastScript.menuHit)
        {
            if (notCheck9)
            {
                background.color = checkColor;
                checksound.Play();
                allCheck += 1;
                notCheck9 = false;

            }
            else
            {
                StartCoroutine(AllreadyCheck());
            }
        }
    }



    IEnumerator AllreadyCheck()
    {
        allreadyCheck.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        allreadyCheck.gameObject.SetActive(false);
    }

    IEnumerator FadeImage()
    {
        fadeAway = false;
        float timer = 2;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            panneau.gameObject.GetComponent<CanvasGroup>().alpha = timer/2;
            yield return null;
        }
        panneau.gameObject.GetComponent<CanvasGroup>().alpha = 0;


    }
}
