using UnityEngine;

public class menuManager : MonoBehaviour
{
    [SerializeField] private Collider settingsButton;
    [SerializeField] private Collider helpButton;
    [SerializeField] private Collider backSettings;
    [SerializeField] private Collider backHelp;

    [SerializeField] private Canvas settings;
    [SerializeField] private Canvas help;
    [SerializeField] private Canvas menu;

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
        if (collider.gameObject.CompareTag("backHelp"))
        {
            help.gameObject.SetActive(false);
            menu.gameObject.SetActive(true);
        }
        if (collider.gameObject.CompareTag("backSettings"))
        {
            settings.gameObject.SetActive(false);
            menu.gameObject.SetActive(true);
        }
    }
}
