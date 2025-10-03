using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenFader : MonoBehaviour
{
    [Header("Réglages")]
    public Image fadeImage;
    public float fadeDuration = 1f;
    public bool playOnStart = true; // si vrai → fade-out au démarrage

    /*void Start()
    {
        if (playOnStart && fadeImage != null)
        {
            // commencer en noir puis fade vers transparent
            Color c = fadeImage.color;
            c.a = 1f;
            fadeImage.color = c;
            StartCoroutine(FadeOut());
        }
    }*/
    void Awake()
    {
        if (fadeImage != null)
        {
            Color c = fadeImage.color;
            c.a = 1f; // noir complet immédiatement
            fadeImage.color = c;
        }
    }

    public IEnumerator FadeOut() // noir → transparent
    {
        float t = 0f;
        Color c = fadeImage.color;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            c.a = Mathf.Lerp(1f, 0f, t / fadeDuration);
            fadeImage.color = c;
            yield return null;
        }

        c.a = 0f; // assurer transparence
        fadeImage.color = c;
    }

    public IEnumerator FadeIn() // transparent → noir
    {
        float t = 0f;
        Color c = fadeImage.color;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            c.a = Mathf.Lerp(0f, 1f, t / fadeDuration);
            fadeImage.color = c;
            yield return null;
        }

        c.a = 1f; // assurer noir total
        fadeImage.color = c;
    }
}
