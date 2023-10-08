using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingMenu : MonoBehaviour
{
    [SerializeField] AudioMixerGroup _masterAudioMixer;
    [SerializeField] AudioMixerGroup _musicAudioMixer;
    [SerializeField] AudioMixerGroup _effectAudioMixer;
    
    public void MenuPlay()
    {
        gameObject.SetActive(true);
    }
    public void MenuExit()
    {
        gameObject.SetActive(false);
    }
    public void SetMasterVolume(float volume)
    {
        _masterAudioMixer.audioMixer.SetFloat("MasterVolume", Mathf.Lerp(-80, 0, volume));     
        Debug.Log(Mathf.Lerp(-80, 0, volume));
    }

    public void SetMusicVolume(float volume)
    {
        _musicAudioMixer.audioMixer.SetFloat("MusicVolume", Mathf.Lerp(-80, 0, volume));
    }

    public void SetEffectVolume(float volume)
    {
        _effectAudioMixer.audioMixer.SetFloat("EffectVolume", Mathf.Lerp(-80, 0, volume));
    }

}
