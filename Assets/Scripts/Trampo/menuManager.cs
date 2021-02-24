using UnityEngine;
using UnityEngine.UI;

public class menuManager : MonoBehaviour
{
    [Header("Canvas")]
    [SerializeField] private Canvas settings;
    [SerializeField] private Canvas help;
    [SerializeField] private Canvas menu;

    [Header("Sliders")]
    [SerializeField] private Slider generalVolume;
    [SerializeField] private Slider FXVolume;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("settingsButton"))
        {
            settings.gameObject.SetActive(true);
            menu.gameObject.SetActive(false);
        }
        if (collider.gameObject.CompareTag("helpButton"))
        {
            help.gameObject.SetActive(true);
            menu.gameObject.SetActive(false);
        }
        if (collider.gameObject.CompareTag("QuitButton"))
        {
            Application.Quit();
        }
        if (collider.gameObject.CompareTag("volumeMoinsGeneral"))
        {
            generalVolume.value -= 0.2f;
        }
        if (collider.gameObject.CompareTag("volumePlusGeneral"))
        {
            generalVolume.value += 0.2f;
        }
        if (collider.gameObject.CompareTag("volumeMoinsFX"))
        {
            FXVolume.value -= 0.2f;
        }
        if (collider.gameObject.CompareTag("volumePlusFX"))
        {
            FXVolume.value += 0.2f;
        }
    }
}
