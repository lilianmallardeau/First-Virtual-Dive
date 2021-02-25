using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entertainor : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float alertMinTimer = 15f;
    [SerializeField] private float alertMaxTimer = 25f;
    private float alertTimer;
    private bool alerting = false;

    public void Start()
    {
        alertTimer = Random.Range(alertMinTimer,alertMaxTimer);
    }

    public void Update()
    {
        alertTimer -= Time.deltaTime;
        if (alertTimer <= 0 && !alerting) {
            alerting = true; //reset alerting à false et reset le timer quand le player a fait le bon geste.
            
            // ALERTER LE JOUEUR
        }
    }

}