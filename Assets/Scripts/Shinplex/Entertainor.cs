using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entertainor : MonoBehaviour
{
    [SerializeField] private Transform player;
    
    [SerializeField] private float alertMinTimer = 15f;
    [SerializeField] private float alertMaxTimer = 25f;
    [SerializeField] private float moveSpeedMax = 8f;
    [SerializeField] private float distFromPlayer = 5f;
    [SerializeField] private GameObject canvasPivot;

    public float moveSpeed = 0f;
    private float distanceFromObjective = 0f;
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
            alerting = true; 
            canvasPivot.SetActive(true);
        }

        if (alerting) 
        {
            //reset alerting à false et reset le timer quand le player a fait le bon geste.
            canvasPivot.SetActive(false);
        }

        canvasPivot.transform.LookAt(player.position);
    }

    public void FixedUpdate()
    {
        Vector3 objective = new Vector3(player.position.x + 6, player.position.y + 3, player.position.z + 5);

        distanceFromObjective = (transform.position - objective).magnitude;

        if (distanceFromObjective <= distFromPlayer) {
            moveSpeed = moveSpeedMax / distFromPlayer * distanceFromObjective;
        }
        else moveSpeed = moveSpeedMax;

        transform.position = Vector3.MoveTowards(transform.position, objective, moveSpeed * Time.deltaTime);

        transform.LookAt(new Vector3(player.position.x, player.position.y + 3, player.position.z));
    }

}