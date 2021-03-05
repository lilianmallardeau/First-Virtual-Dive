using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class menuManager : MonoBehaviour
{
    
    [Header("Canvas")]
    [SerializeField] private Canvas settings;
    [SerializeField] private Canvas help;
    [SerializeField] private Canvas menu;
    [SerializeField] private Canvas rightMenu;
    [SerializeField] private Canvas leftMenu;
    [SerializeField] private Canvas popDebut;
    [SerializeField] private Canvas popFin;
    [SerializeField] private Canvas popAlerte;
    [SerializeField] private Canvas popDebutMain;

    [Header("Sliders")]
    [SerializeField] private Slider thisGeneralVolume;
    [SerializeField] private Slider generalVolume;
    [SerializeField] private Slider thisFXVolume;
    [SerializeField] private Slider FXVolume;

    [Header("Sounds")]
    [SerializeField] private AudioSource menuSound;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GestureEventsManager.hand == "right")
        {
            menu = rightMenu;
        }
        if (GestureEventsManager.hand == "left")
        {
            menu = leftMenu;
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("settingsButton"))
        {
            settings.gameObject.SetActive(true);
            menu.gameObject.SetActive(false);
            menuSound.Play();
        }
        if (collider.gameObject.CompareTag("helpButton"))
        {
            help.gameObject.SetActive(true);
            menuSound.Play();
            menu.gameObject.SetActive(false);
        }
        if (collider.gameObject.CompareTag("QuitButton"))
        {
            popFin.gameObject.SetActive(true);
            menu.gameObject.SetActive(false);
            menuSound.Play();
        }
        if (collider.gameObject.CompareTag("Quitter"))
        {
            Application.Quit();
        }
        if (collider.gameObject.CompareTag("Rester"))
        {
            popFin.gameObject.SetActive(false);
            menu.gameObject.SetActive(true);
            menuSound.Play();
        }
        if (collider.gameObject.CompareTag("volumeMoinsGeneral"))
        {
            menuSound.Play();
            thisGeneralVolume.value -= 10f;
            generalVolume.value -= 10f;
        }
        if (collider.gameObject.CompareTag("volumePlusGeneral"))
        {
            menuSound.Play();
            thisGeneralVolume.value += 10f;
            generalVolume.value += 10f;
        }
        if (collider.gameObject.CompareTag("volumeMoinsFX"))
        {
            menuSound.Play();
            thisFXVolume.value -= 10f;
            FXVolume.value -= 10f;
        }
        if (collider.gameObject.CompareTag("volumePlusFX"))
        {
            menuSound.Play();
            thisFXVolume.value += 10f;
            FXVolume.value += 10f;
        }
        if (collider.gameObject.CompareTag("OKPop"))
        {
            menuSound.Play();
            popDebut.gameObject.SetActive(false);
        }
        if (collider.gameObject.CompareTag("AlertUnderstood"))
        {
            menuSound.Play();
            popAlerte.gameObject.SetActive(false);
        }
        if (collider.gameObject.CompareTag("OkMainScene"))
        {
            menuSound.Play();
            popDebutMain.gameObject.SetActive(false);
        }
        if (collider.gameObject.CompareTag("PassTuto"))
        {
            menuSound.Play();
            SceneManager.LoadScene("Dive");
            TutoFinish.tutoFinish = false;
        }
    }
}
