using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
public class CinematicManager : MonoBehaviour
{
    [Header("Références des timelines")]
    public PlayableDirector introTimeline;
    public PlayableDirector middleTimeline;
    public PlayableDirector outroTimeline;

    [Header("Fade optionnel")]
    public ScreenFader screenFader;

    private void Start()
    {
        PlayIntro();
    }

    // --- Fonction pour jouer l'intro ---
    public void PlayIntro()
    {
        StartCoroutine(PlayTimeline(introTimeline));
    }

    // --- Fonction pour jouer la cinématique du milieu ---
    public void PlayMiddle()
    {
        StartCoroutine(PlayTimeline(middleTimeline));
    }

    // --- Fonction pour jouer l'outro ---
    public void PlayOutro()
    {
        StartCoroutine(PlayTimeline(outroTimeline));
    }

    // Coroutine générique pour enchaîner fade + timeline
    private IEnumerator PlayTimeline(PlayableDirector director)
    {
        if (director == null) yield break;

        // Fade noir avant la cinématique
        if (screenFader != null)
            yield return StartCoroutine(screenFader.FadeOut());

        // Lancer la timeline
        director.gameObject.SetActive(true);
        director.Play();

        // Attendre la fin
        yield return new WaitForSeconds((float)director.duration);

        // Fade out après la cinématique
        if (screenFader != null)
            yield return StartCoroutine(screenFader.FadeIn());
    }
}
