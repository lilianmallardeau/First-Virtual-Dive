using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoSound : MonoBehaviour
{

    [SerializeField] private AudioSource meloAudioSource;
    [SerializeField] private AudioClip introSound;
    [SerializeField] private AudioClip essaiPanneauSound;
    [SerializeField] private AudioClip naviguerSound;

    private bool naviguerSoundisRunning = false;

    // Start is called before the first frame update
    void Start()
    {
        meloAudioSource.PlayOneShot(introSound, 0.5f);

    }

    // Update is called once per frame
    void Update()
    {
        if (CheckPanneau.allCheck == 5 && !naviguerSoundisRunning)
        {
            StartCoroutine(NaviguerSound());

        }
    }

    IEnumerator NaviguerSound()
    {
        naviguerSoundisRunning = true;
        yield return new WaitForSeconds(3f);
        meloAudioSource.PlayOneShot(naviguerSound, 0.5f);
    }
}
