using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlaySound : MonoBehaviour
{

    [SerializeField] private AudioSource sound;
    [SerializeField] private float time;
    private bool playSound = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playSound) StartCoroutine(SoundCouroutine());
    }

    IEnumerator SoundCouroutine()
    {
        playSound = false;
        yield return new WaitForSeconds(time);
        sound.Play();
        playSound = true;
    }
}
