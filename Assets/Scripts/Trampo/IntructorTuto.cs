using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntructorTuto : MonoBehaviour
{
    [SerializeField] private GameObject canvasPivot;
    [SerializeField] private Transform player;
    [SerializeField] private Image instructions;
    [SerializeField] private Sprite okPic;
    [SerializeField] private Sprite coldPic;
    [SerializeField] private Sprite oxygenPic;
    [SerializeField] private Sprite pressurePic;
    [SerializeField] private GestureEventsManager _gestureManager;
    [SerializeField] private GameObject bodyToMove;
    [SerializeField] private Image _fadingScreen;



    [SerializeField] private AudioSource entertainorAudioSource;
    [SerializeField] private AudioClip heySound;
    [SerializeField] private AudioClip alrightSound;


    private float distanceFromObjective = 0f;
    private bool hey = true;
    private bool startInstru = true;

    public void FixedUpdate()
    {
        Vector3 objective = new Vector3(player.position.x, player.position.y, player.position.z);
        distanceFromObjective = (transform.position - objective).magnitude;

        transform.LookAt(new Vector3(player.position.x, player.position.y, player.position.z));
        canvasPivot.transform.LookAt(player.position);

        if (distanceFromObjective < 10f && startInstru)
        {
            Debug.Log("startInstru");
            StartCoroutine(StartInstructions());
        }

        if (CheckPanneau.allCheck == 5 && hey)
        {
            StartCoroutine(PlayAudio());
        }
    }

    IEnumerator PlayAudio()
    {
        hey = false;
        yield return new WaitForSeconds(2.5f);
        entertainorAudioSource.PlayOneShot(heySound, 0.5f);
    }
    
    IEnumerator StartInstructions()
    {
        //Manque juste à adapter les wait en fonction de la voix qui explique
        startInstru = false;
        yield return new WaitForSeconds(3f);
        canvasPivot.SetActive(true);
        yield return new WaitForSeconds(3f);
        instructions.sprite = okPic;
        yield return new WaitForSeconds(3f);
        instructions.sprite = coldPic;
        yield return new WaitForSeconds(3f);
        instructions.sprite = oxygenPic;
        yield return new WaitForSeconds(3f);
        canvasPivot.SetActive(false);
        entertainorAudioSource.PlayOneShot(alrightSound, 0.5f);
        yield return new WaitForSeconds(3f);
        PlayerControl._canMove = false;
        do
        {
            yield return null;
        } while (_gestureManager.currentGesture != GestureEventsManager.Gesture.Down);

        // Instructor asks if everything is ok
        instructions.sprite = okPic;
        canvasPivot.SetActive(true);
        yield return new WaitForSeconds(.01f);
        do
        {
            yield return null;
        } while (_gestureManager.currentGesture != GestureEventsManager.Gesture.Ok);
        canvasPivot.SetActive(false);
        float timer = 4f;
        while (timer > 0)
        {
            bodyToMove.transform.Translate(Vector3.down * (0.5f * Time.deltaTime));
            timer -= Time.deltaTime;
            // TODO fade screen to black
            _fadingScreen.color = new Color(0, 0, 0, 1 - timer / 4f);
            yield return null;
        }

        _fadingScreen.color = new Color(0, 0, 0, 0);
        PlayerControl._canMove = true;
        SceneManager.LoadScene("Dive");

    }
}
