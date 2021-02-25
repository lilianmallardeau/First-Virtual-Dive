using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastScript : MonoBehaviour
{

    public static bool okHit;
    public static bool monterHit;
    public static bool descendreHit;
    public static bool mipressionHit;
    public static bool reserveHit;
    public static bool plusAirHit;
    public static bool notOkHit;
    public static bool coldHit;
    public static bool menuHit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * 2, Color.yellow);
        okHit = false;
        monterHit = false;
        descendreHit = false;
        mipressionHit = false;
        reserveHit = false;
        plusAirHit = false;
        notOkHit = false;
        coldHit = false;
        menuHit = false;

    RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);

        int layer_mask = LayerMask.GetMask("UI");

        if (Physics.Raycast(ray, out hit, 2f, layer_mask, QueryTriggerInteraction.Ignore))
        {
            if (hit.transform.name == "OK")
            {
                okHit = true;
            }
            if (hit.transform.name == "Monter")
            {
                monterHit = true;
            }
            if (hit.transform.name == "Descendre")
            {
                descendreHit = true;
            }
            if (hit.transform.name == "ça ne va pas")
            {
                notOkHit = true;
            }
            if (hit.transform.name == "J'ai froid")
            {
                coldHit = true;
            }
            if (hit.transform.name == "mi-pression")
            {
                mipressionHit = true;
            }
            if (hit.transform.name == "Je suis sur la réserve")
            {
                reserveHit = true;
            }
            if (hit.transform.name == "Je n'ai plus d'air")
            {
                plusAirHit = true;
            }
            if (hit.transform.name == "Regarder le menu")
            {
                menuHit = true;
            }
        }
    }
}
