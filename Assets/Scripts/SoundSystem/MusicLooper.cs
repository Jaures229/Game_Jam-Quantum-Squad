using UnityEngine;
using System.Collections;          // <-- pour IEnumerator
using System.Collections.Generic;

public class MusicLooper : MonoBehaviour
{
    [Header("Playlist")]
    public List<AudioClip> playlist = new List<AudioClip>();

    [Header("Transition")]
    public float fadeDuration = 1.5f; // Durée du fade in/out

    private AudioSource audioSource;
    private bool isFading = false;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = false;
        audioSource.playOnAwake = false;
    }

    void Start()
    {
        if (playlist.Count > 0)
            StartCoroutine(PlayMusicLoop());
    }

    IEnumerator PlayMusicLoop()
    {
        while (true)
        {
            // Choisir un clip aléatoire
            AudioClip clip = playlist[Random.Range(0, playlist.Count)];

            yield return StartCoroutine(FadeIn(clip));

            // Attendre la fin du clip moins le fade
            yield return new WaitForSeconds(clip.length - fadeDuration);

            yield return StartCoroutine(FadeOut());
        }
    }

    IEnumerator FadeIn(AudioClip clip)
    {
        isFading = true;
        audioSource.clip = clip;
        audioSource.volume = 0f;
        audioSource.Play();

        float t = 0;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(0f, 1f, t / fadeDuration);
            yield return null;
        }

        audioSource.volume = 1f;
        isFading = false;
    }

    IEnumerator FadeOut()
    {
        isFading = true;

        float startVolume = audioSource.volume;
        float t = 0;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, 0f, t / fadeDuration);
            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = 0f;
        isFading = false;
    }
}
