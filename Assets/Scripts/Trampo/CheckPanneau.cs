using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckPanneau : MonoBehaviour
{

    [SerializeField] private Image background;
    private Color checkColor = Color.green;


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
            background.color = checkColor;
        }
    }
    public void checkMonter()
    {
        if (RaycastScript.monterHit == true)
        {
            background.color = checkColor;
        }

    }
    public void checkDescendre()
    {
        if (RaycastScript.descendreHit == true)
        {
            background.color = checkColor;
        }
    }
    public void checkNotOK()
    {
        if (RaycastScript.notOkHit == true)
        {
            background.color = checkColor;
        }
    }
    public void checkCold()
    {
        if (RaycastScript.coldHit == true)
        {
            background.color = checkColor;
        }
    }
    public void checkMiPression()
    {
        if (RaycastScript.mipressionHit == true)
        {
            background.color = checkColor;
        }
    }
    public void checkReserve()
    {
        if (RaycastScript.reserveHit == true)
        {
            background.color = checkColor;
        }
    }

    public void checkPlusAir()
    {
        if (RaycastScript.plusAirHit == true)
        {
            background.color = checkColor;
        }
    }
    public void checkMenu()
    {
        if (RaycastScript.menuHit == true)
        {
            background.color = checkColor;
        }
    }
}
