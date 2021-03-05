using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoFinish : MonoBehaviour
{

    public static bool tutoFinish = true;
    [SerializeField] private AudioSource melo;


    // Start is called before the first frame update
    void Start()
    {
        if (tutoFinish)
        {
            melo.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
