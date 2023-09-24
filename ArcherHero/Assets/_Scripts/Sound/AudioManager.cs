using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{  
    [SerializeField] private AudioData audioData;
    
    void Awake()
    {           
        foreach (AudioData.SoundData s in audioData.sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;          
            s.source.loop = s.loop;
        }
    }
  
    public void Play(int soundIndex)
    {
        if (soundIndex >= 0 && soundIndex < audioData.sounds.Length)
        {
            audioData.sounds[soundIndex].source.Play();
        }
    }
    public void Stop(int soundIndex)
    {
        if (soundIndex >= 0 && soundIndex < audioData.sounds.Length)
        {
            audioData.sounds[soundIndex].source.Stop();
        }
    }
}
