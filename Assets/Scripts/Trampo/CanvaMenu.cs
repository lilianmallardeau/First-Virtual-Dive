using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvaMenu : MonoBehaviour
{

    [SerializeField] private Camera player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(transform.position + player.transform.rotation * Vector3.forward, player.transform.rotation * Vector3.up);
    }
}
