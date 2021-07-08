using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hellmade.Sound;
using UnityEngine.Audio;

public class PlayerAudio : MonoBehaviour
{

    [Header("Audio related")]
    public AudioController[] audiosSO;
    public AudioMixerGroup sfxMixer, voiceMixer;
    public AudioMixerSnapshot normalSnapshot, sneakySnapshot;

    public float timeChangeSnapshot;

    private int waitCount = 12, followCount = 9, gameoverCount = 7, runningCount = 6, jumpCount = 3;
    private int footstepID, jumpID;

    public void SnapshotTransition(bool isSneaky)
    {
        if (isSneaky)
        {
            sneakySnapshot.TransitionTo(timeChangeSnapshot);
        }
        else
        {
            normalSnapshot.TransitionTo(timeChangeSnapshot);
        }
    }

    public void FootstepsSFX()
    {
        
        if (EazySoundManager.GetSoundAudio(footstepID) == null)
        {
            int audioChoosed = Random.Range(1, runningCount);

            int audioPosition = System.Array.FindIndex(audiosSO[0].audios, aSO => aSO.audioClipName == "Running" + audioChoosed);
            footstepID = EazySoundManager.PlaySound(audiosSO[0].audios[audioPosition].clip, audiosSO[0].volume, false, null);

            EazySoundManager.GetSoundAudio(footstepID).AudioSource.outputAudioMixerGroup = voiceMixer;

            Debug.Log("Run Audio Choosed = " + audioChoosed);

        }
        else if(!EazySoundManager.GetSoundAudio(footstepID).IsPlaying)
        {
            int audioChoosed = Random.Range(1, runningCount);

            int audioPosition = System.Array.FindIndex(audiosSO[0].audios, aSO => aSO.audioClipName == "Running" + audioChoosed);
            footstepID = EazySoundManager.PlaySound(audiosSO[0].audios[audioPosition].clip, audiosSO[0].volume, false, null);

            EazySoundManager.GetSoundAudio(footstepID).AudioSource.outputAudioMixerGroup = voiceMixer;

            Debug.Log("Run Audio Choosed = " + audioChoosed);

        }



    }

    public void JumpSFX()
    {
        EazySoundManager.GetSoundAudio(footstepID).Stop();

        if (EazySoundManager.GetSoundAudio(jumpID) == null)
        {
            int audioChoosed = Random.Range(1, jumpCount);

            int audioPosition = System.Array.FindIndex(audiosSO[0].audios, aSO => aSO.audioClipName == "Jump" + audioChoosed);
            jumpID = EazySoundManager.PlaySound(audiosSO[0].audios[audioPosition].clip, audiosSO[0].volume / 3f, false, null);

            Debug.Log("Jump Audio Choosed = " + audioChoosed);
        }
        else 
        {
            if (!EazySoundManager.GetSoundAudio(jumpID).IsPlaying)
            {
                int audioChoosed = Random.Range(1, jumpCount);

                int audioPosition = System.Array.FindIndex(audiosSO[0].audios, aSO => aSO.audioClipName == "Jump" + audioChoosed);
                jumpID = EazySoundManager.PlaySound(audiosSO[0].audios[audioPosition].clip, audiosSO[0].volume / 3f, false, null);

                Debug.Log("Jump Audio Choosed = " + audioChoosed);
            } else if (EazySoundManager.GetSoundAudio(jumpID).IsPlaying)
            {
                EazySoundManager.GetSoundAudio(jumpID).Stop();


                int audioChoosed = Random.Range(1, jumpCount);

                int audioPosition = System.Array.FindIndex(audiosSO[0].audios, aSO => aSO.audioClipName == "Jump" + audioChoosed);
                jumpID = EazySoundManager.PlaySound(audiosSO[0].audios[audioPosition].clip, audiosSO[0].volume/3f, false, null);

                Debug.Log("Jump Audio Choosed = " + audioChoosed);
            }
        }


    }

    public void ZarabatanaSFX()
    {

    }

}
