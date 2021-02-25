using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class CheckPanneau : MonoBehaviour
{

    [SerializeField] private Image background;
    [SerializeField] private AudioSource checksound;
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


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void checkOK()
    {
        if (RaycastScript.okHit == true)
        {
            if (notCheck1 == true)
            {
                background.color = checkColor;
                checksound.Play();
                notCheck1 = false;
            }
        }
    }
    public void checkMonter()
    {
        if (RaycastScript.monterHit == true)
        {
            if (notCheck2 == true)
            {
                background.color = checkColor;
                checksound.Play();
                notCheck2 = false;
            }

        }

    }
    public void checkDescendre()
    {
        if (RaycastScript.descendreHit == true)
        {
            if (notCheck3 == true)
            {
                background.color = checkColor;
                checksound.Play();
                notCheck3 = false;
            }
        }
    }
    public void checkNotOK()
    {
        if (RaycastScript.notOkHit == true)
        {
            if (notCheck4 == true)
            {
                background.color = checkColor;
                checksound.Play();
                notCheck4 = false;
            }
        }
    }
    public void checkCold()
    {
        if (RaycastScript.coldHit == true)
        {
            if (notCheck5 == true)
            {
                background.color = checkColor;
                checksound.Play();
                notCheck5 = false;
            }
        }
    }
    public void checkMiPression()
    {
        if (RaycastScript.mipressionHit == true)
        {
            if (notCheck6 == true)
            {
                background.color = checkColor;
                checksound.Play();
                notCheck6 = false;
            }
        }
    }
    public void checkReserve()
    {
        if (RaycastScript.reserveHit == true)
        {
            if (notCheck7 == true)
            {
                background.color = checkColor;
                checksound.Play();
                notCheck7 = false;
            }
        }
    }

    public void checkPlusAir()
    {
        if (RaycastScript.plusAirHit == true)
        {
            if (notCheck8 == true)
            {
                background.color = checkColor;
                checksound.Play();
                notCheck8 = false;
            }
        }
    }
    public void checkMenu()
    {
        if (RaycastScript.menuHit == true)
        {
            if (notCheck9 == true)
            {
                background.color = checkColor;
                checksound.Play();
                notCheck9 = false;
            }
        }
    }
}
