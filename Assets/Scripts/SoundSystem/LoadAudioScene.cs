using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadAudioScene : MonoBehaviour
{
    [SerializeField] private AudioClip[] _musicList;
    private int _currentClipIndex = 0;

    private void OnEnable()
    {
        if (AudioManager.Instance != null)
        AudioManager.Instance.OnMusicFinished += PlayNextClip;
    }

    private void OnDisable()
    {
        if (AudioManager.Instance != null)
        AudioManager.Instance.OnMusicFinished -= PlayNextClip;
    }

    void Start()
    {
        PlayNextClip();
    }

    private void PlayNextClip()
    {
        if (_musicList.Length > 0)
        {
            AudioManager.Instance.PlayMusic(_musicList[_currentClipIndex]);
            _currentClipIndex = 0;
            if (_currentClipIndex >= _musicList.Length)
            {
                _currentClipIndex = 0;
            }
        }
    }

    public void OnMusicFinished()
    {
        PlayNextClip();
    }
    void Update()
    {
        
    }
}
