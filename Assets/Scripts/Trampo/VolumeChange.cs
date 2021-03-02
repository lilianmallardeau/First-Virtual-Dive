using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeChange : MonoBehaviour
{
    public AudioMixer audioMixerFX;
    public AudioMixer audioMixerMusic;

    public void SetVolumeFX (float volume)
    {
        audioMixerFX.SetFloat("volume", volume);
    }

    public void SetVolumeMusic(float volume)
    {
        audioMixerMusic.SetFloat("volume", volume);
    }
}
