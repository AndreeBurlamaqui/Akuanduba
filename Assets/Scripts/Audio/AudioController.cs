using UnityEngine;

[CreateAssetMenu(fileName = "Audio List", menuName = "ScriptableObjects/Audio List", order = 1)]
public class AudioController : ScriptableObject
{
    public AudioList[] audios;

    [Range(0,1)]
    public float volume;
}

[System.Serializable]
public struct AudioList
{
    public string audioClipName;
    public AudioClip clip;
}

