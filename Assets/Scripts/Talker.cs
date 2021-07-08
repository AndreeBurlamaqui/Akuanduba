using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Hellmade.Sound;

public class Talker : MonoBehaviour
{
    public int contoCount = 1, contoLimit, contoPlaying;
    public AudioClip[] contosAudio;
    public string text;
    public float narrativaVolume;

    public RedBlueGames.Tools.TextTyper.TextTyper textTyper;
    void Start()
    {

        text = Lean.Localization.LeanLocalization.GetTranslationText("Conto" + contoCount);
        contoPlaying = EazySoundManager.PlaySound(contosAudio[contoCount], narrativaVolume, false, null);

        textTyper.TypeText(text, 0.05f);

    }
    public void ChangeNextConto()
    {
        if (EazySoundManager.GetAudio(contosAudio[contoPlaying]) == null)
        {
            if (contoCount <= contoLimit)
            {
                contoCount++;

                text = Lean.Localization.LeanLocalization.GetTranslationText("Conto" + contoCount);
                contoPlaying = EazySoundManager.PlaySound(contosAudio[contoCount], narrativaVolume, false, null);


                textTyper.TypeText(text, 0.05f);

            }
            else
            {
                //loadar cena nova
                SceneManager.LoadScene(2);
            }
        }
    }
}
