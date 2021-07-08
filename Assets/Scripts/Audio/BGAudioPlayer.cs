using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hellmade.Sound;
using UnityEngine.Audio;

public class BGAudioPlayer : MonoBehaviour
{

    public AudioClip backgroundAudioClip;
    public AudioClip backgroundsneakyAudioClip;

    public AudioClip[] ambienceAudioClip;
    public float bgVolume, ambienceVolume;
    public Vector2 bgFade;
    public AudioMixerGroup soundtrackMixer, ambienceMixer;


    private void Awake()
    {

        EazySoundManager.PlayMusic(backgroundAudioClip, bgVolume, true, false, bgFade.x, bgFade.y);
        EazySoundManager.GetAudio(backgroundAudioClip).AudioSource.outputAudioMixerGroup = soundtrackMixer;
        //play ambiences

        for (int x = 0; x < ambienceAudioClip.Length; x++)
        {
            EazySoundManager.PlaySound(ambienceAudioClip[x], ambienceVolume, true, null);
            EazySoundManager.GetAudio(ambienceAudioClip[x]).AudioSource.outputAudioMixerGroup = ambienceMixer;

        }
    }

    public void BackgroundMusicTransition(bool isSneaky)
    {
        if (isSneaky)
        {
            EazySoundManager.PlayMusic(backgroundsneakyAudioClip, bgVolume, true, false, bgFade.x, bgFade.y);
            EazySoundManager.GetAudio(backgroundsneakyAudioClip).AudioSource.outputAudioMixerGroup = soundtrackMixer;
        }
        else
        {
            EazySoundManager.PlayMusic(backgroundAudioClip, bgVolume, true, false, bgFade.x, bgFade.y);
            EazySoundManager.GetAudio(backgroundAudioClip).AudioSource.outputAudioMixerGroup = soundtrackMixer;
        }
    }
}
