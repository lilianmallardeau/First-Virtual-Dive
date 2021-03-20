using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuSelected : MonoBehaviour
{
    [SerializeField] private Canvas rightMenu;
    [SerializeField] private Canvas leftMenu;
    // Start is called before the first frame update



    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenMenu()
    {
        if (GestureEventsManager.hand == "right")
        {
            rightMenu.gameObject.SetActive(true);
        }
        if (GestureEventsManager.hand == "left")
        {
            leftMenu.gameObject.SetActive(true);
        }
    }
}
