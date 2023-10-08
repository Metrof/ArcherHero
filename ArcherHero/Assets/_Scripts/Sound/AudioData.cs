using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "AudioData", menuName = "ScriptableObjects/Audio Data")]
public class AudioData : ScriptableObject
{
    [System.Serializable]
    public class SoundData
    {
        public string name;      

        public AudioClip clip;

        [Range(0f, 1f)]
        public float volume = 1;

        [Range(0f, 3f)]
        public float pitch = 1;

        public bool loop = false;

        [HideInInspector]
        public AudioSource source;
    }
    public SoundData[] sounds;
}

