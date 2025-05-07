using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioSource _audioSourceSFX;
    public event Action OnMusicFinished;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        DontDestroyOnLoad(this);
        _audioSource.volume = 0.3f;
    }

    private void Start()
    {
        if (_audioSource == null || _audioSourceSFX == null)
        {
            Debug.Log("_audioSource not asigned");
        }
    }

    private void Update()
    {
        if (!_audioSource.isPlaying && _audioSource.clip != null)
        {
            OnMusicFinished?.Invoke();
            _audioSource.clip = null;
        }
    }

    public void PlayMusic (AudioClip clip)
    {
        if (clip != _audioSource.clip)
        {
            _audioSource.clip = clip;
        }
        if (!_audioSource.isPlaying)
        {
            _audioSource.Play();
        }
    }

    public void StopMusic()
    {
        if (_audioSource.isPlaying)
        {
            _audioSource.Stop();
        }
    }

    public void PlaySFX(AudioClip effect)
    {
        _audioSource.volume = 0.25f;
        _audioSourceSFX.PlayOneShot(effect);
        _audioSource.volume = 0.30f;
    }

    public void SetVolumeMusic(float volume)
    {
        _audioSource.volume = volume;
    }

    public void SetSFXVolume(float volume)
    {
        _audioSourceSFX.volume = volume;
    }

    public void SetMasterVolume ()
    {
    }
}
