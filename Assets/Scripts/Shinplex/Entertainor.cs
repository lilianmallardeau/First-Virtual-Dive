using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Entertainor : MonoBehaviour
{
    [SerializeField] private Transform player;
    
    [SerializeField] private float alertMinTimer = 15f;
    [SerializeField] private float alertMaxTimer = 25f;
    [SerializeField] private float moveSpeedMax = 8f;
    [SerializeField] private float distFromPlayer = 5f;
    [SerializeField] private GameObject canvasPivot;

    [SerializeField] private Image currentPic;
    [SerializeField] private Sprite okPic;
    [SerializeField] private Sprite coldPic;
    [SerializeField] private Sprite oxygenPic;
    [SerializeField] private Sprite pressurePic;
    [SerializeField] private AudioSource entertainorAudioSource;
    [SerializeField] private AudioClip heySound;
    [SerializeField] private AudioClip alrightSound;
    
    public static bool lookAtMe = false;
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
            int choice = Random.Range(0,3);
            switch (choice)
            {
                case 0:
                    currentPic.sprite = okPic;
                    break;
                case 1:
                    currentPic.sprite = coldPic;
                    break;
                case 2:
                    currentPic.sprite = oxygenPic;
                    break;
                case 3:
                    currentPic.sprite = pressurePic;
                    break;
                default:
                    break;
            }
            canvasPivot.SetActive(true);
            entertainorAudioSource.PlayOneShot(heySound,0.5f);
        }

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
        canvasPivot.transform.LookAt(player.position);
    }

    public void AskOK()
    {
        alerting = true;
        currentPic.sprite = okPic;
        canvasPivot.SetActive(true);
    }

    public void AnswerOK()
    {
        if (alerting && lookAtMe) 
        {
            canvasPivot.SetActive(false);
            entertainorAudioSource.PlayOneShot(alrightSound,0.8f);
            alerting = false;
        }
    }
}