using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Music")]
    public AudioSource _audio;
    public AudioSource _music;
    public AudioSource[] _voice;

    [Header("SFX")]
    public AudioSource[] sfxs;

    private bool isMuted = false; // Track mute state

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one AudioManager");
        }

        else
        {
            instance = this;
        }

        Debug.Log(_voice.Length);
    }

    public void PlaySFXs(int _index)
    {
        foreach (var item in sfxs)
        {
            item.Stop();
        }

        sfxs[_index].Play();
    }

    public void PlayAudio()
    {
        if (!_audio.isPlaying) { _audio.Play(); }
    }

    public void PlayVoices(int _index)
    {
        foreach (var item in _voice)
        {
            item.Stop();
        }

        if (!_voice[_index].isPlaying) { PlaySFXs(0); _voice[_index].Play(); }
    }

    public void StopVoice()
    {
        foreach (var item in _voice)
        {
            item.Stop();
        }
    }

    public void ToggleMute()
    {
        PlaySFXs(0);

        isMuted = !isMuted;

        // Mute/unmute music
        _audio.mute = isMuted;
        _music.mute = isMuted;
        foreach (var item in _voice)
        {
            item.mute = isMuted;
        }

        // Mute/unmute all SFX
        //foreach (var sfx in sfxs)
        //{
        //    sfx.mute = isMuted;
        //}

        // Optional: Save mute state
        // PlayerPrefs.SetInt("IsMuted", isMuted ? 1 : 0);
        // PlayerPrefs.Save();
    }
}
