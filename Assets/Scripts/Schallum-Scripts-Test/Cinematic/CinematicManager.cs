using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using Unity.Cinemachine;
public class CinematicManager : MonoBehaviour
{
    [Header("Références des timelines")]
    public PlayableDirector introTimeline;
    public PlayableDirector middleTimeline;
    public PlayableDirector outroTimeline;

    [Header("Fade optionnel")]
    public ScreenFader screenFader;

    [Header("Caméras")]
    public CinemachineCamera defaultCamera;
    public CinemachineCamera introCamera;
    public CinemachineCamera middleCamera;
    public CinemachineCamera outroCamera;

    private CinemachineCamera currentCamera;

    private void Start()
    {
        PlayIntro(introCamera); // tu peux préciser ici la caméra sur laquelle rester
    }

    // --- Fonction pour jouer l'intro ---
    public void PlayIntro(CinemachineCamera endCamera = null)
    {
        StartCoroutine(PlayTimeline(introTimeline, endCamera));
    }

    // --- Fonction pour jouer la cinématique du milieu ---
    public void PlayMiddle(CinemachineCamera endCamera = null)
    {
        StartCoroutine(PlayTimeline(middleTimeline, endCamera));
    }

    // --- Fonction pour jouer l'outro ---
    public void PlayOutro(CinemachineCamera endCamera = null)
    {
        StartCoroutine(PlayTimeline(outroTimeline, endCamera));
    }

    // --- Coroutine générique ---
    private IEnumerator PlayTimeline(PlayableDirector director, CinemachineCamera endCamera)
    {
        if (director == null) yield break;

        // --- Fade avant la cinématique ---
        if (screenFader != null)
            yield return StartCoroutine(screenFader.FadeOut());

        // --- Lancer la timeline ---
        director.gameObject.SetActive(true);
        director.Play();

        // --- Attendre la fin ---
        yield return new WaitForSeconds((float)director.duration);

        // --- Fade après la cinématique ---
        if (screenFader != null)
            yield return StartCoroutine(screenFader.FadeIn());

        // --- Choisir la caméra à garder ---
        if (endCamera != null)
        {
            SetActiveCamera(endCamera);
        }
        else if (defaultCamera != null)
        {
            SetActiveCamera(defaultCamera);
        }
    }

    // --- Active une caméra et désactive toutes les autres ---
    private void SetActiveCamera(CinemachineCamera targetCam)
    {
        var allCams = FindObjectsOfType<CinemachineCamera>();
        foreach (var cam in allCams)
            cam.Priority = 0;

        targetCam.Priority = 20;
        currentCamera = targetCam;

        Debug.Log($"➡ Caméra active : {targetCam.name}");
    }
}
