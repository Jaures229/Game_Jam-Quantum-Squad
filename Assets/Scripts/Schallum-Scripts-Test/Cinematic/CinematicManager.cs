using System;
using System.Collections;
using Unity.Cinemachine;
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
    

    [Header("Caméras")]
    public CinemachineCamera defaultCamera;
    public CinemachineCamera introCamera;
    public CinemachineCamera middleCamera;
    public CinemachineCamera outroCamera;
    public PuzzleManager puzzleManager;
    private CinemachineCamera currentCamera;
    public Timeline3Trigger timelineCinematic3;
    public GameObject PlayerCinematic;
    public GameObject PlayerGameplay;
    public GameObject CameraCinemactic;

    // --- Événement global ---
    public event Action<string> OnCinematicEnd;
    // string = nom de la cinématique terminée

    private void OnEnable()
    {
        PuzzleManager.OnPuzzleCompleted += HandlePuzzleCompleted; // 🔗 écoute l'événement
        Timeline3Trigger.Cinematic3 += Cinematic3;
    }

    private void OnDisable()
    {
        PuzzleManager.OnPuzzleCompleted -= HandlePuzzleCompleted;
        Timeline3Trigger.Cinematic3 -= Cinematic3;
    }

    private void HandlePuzzleCompleted()
    {
        Debug.Log("🎬 Le puzzle est terminé → Lancement de la cinématique suivante !");
        PlayMiddle(middleCamera);
    }

    private  void Cinematic3()
    {
        PlayOutro(outroCamera);
    }
    private void Start()
    {
        PlayIntro(introCamera);
    }

    // --- Fonction pour jouer l'intro ---
    public void PlayIntro(CinemachineCamera endCamera = null)
    {
        StartCoroutine(PlayTimeline(introTimeline, endCamera, "intro"));
    }

    // --- Fonction pour jouer la cinématique du milieu ---
    public void PlayMiddle(CinemachineCamera endCamera = null)
    {
        StartCoroutine(PlayTimeline(middleTimeline, endCamera, "middle"));

    }

    // --- Fonction pour jouer l'outro ---
    public void PlayOutro(CinemachineCamera endCamera = null)
    {
        StartCoroutine(PlayTimeline(outroTimeline, endCamera, "outro"));
    }

    // --- Coroutine générique ---
    private IEnumerator PlayTimeline(PlayableDirector director, CinemachineCamera endCamera, string cinematicName)
    {
        if (director == null) yield break;

        // --- Fade avant la cinématique ---
        if (screenFader != null)
        {
            yield return StartCoroutine(screenFader.FadeOut());
        }
        PlayerGameplay.SetActive(false);
        PlayerCinematic.SetActive(true);
        CameraCinemactic.SetActive(true);
        // --- Lancer la timeline ---
        director.gameObject.SetActive(true);
        director.Play();

        // --- Attendre la fin ---
        yield return new WaitForSeconds((float)director.duration - 25f);

        if (screenFader != null)
            yield return StartCoroutine(screenFader.FadeIn());

        // --- Choisir la caméra à garder ---
        if (endCamera != null)
            SetActiveCamera(endCamera);
        else if (defaultCamera != null)
            SetActiveCamera(defaultCamera);

        yield return new WaitForSeconds(5f);

        // --- Fade après la cinématique ---


        // --- Prévenir les autres systèmes ---
        OnCinematicEnd?.Invoke(cinematicName);
        Debug.Log($"🎬 Cinématique terminée : {cinematicName}");
        PlayerGameplay.SetActive(true);
        PlayerCinematic.SetActive(false );
        PlayerGameplay.transform.position = PlayerCinematic.transform.position;
        PlayerGameplay.transform.rotation = PlayerCinematic.transform.rotation;
        yield return StartCoroutine(screenFader.FadeOut());
        screenFader.gameObject.SetActive(false);
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

   /*rivate void Update()
    {
        if (puzzleManager.finish_puzzle == true)
        {
            PlayMiddle(middleCamera);
            puzzleManager.finish_puzzle = false;
        }
    }*/
}
