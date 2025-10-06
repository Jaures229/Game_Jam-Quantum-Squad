using UnityEngine;
using System.Collections;

public class StationManager : MonoBehaviour
{
    public CinematicManager cinematicManager;
    public GameObject puzzlePanel;

    public PuzzleManager puzzleManager;
    public ScreenFader screenFader;
    public GameObject manager;
    public GameObject controlPanel;
    public GameObject cameraFPS;
    public GameObject cameraCinematic;
    //public GameObject player;
    //public GameObject playerVisuel;
    public Timeline3Trigger timelineCinematic3;
    public SceneLoader sceneLoader;

    void Start()
    {
        // S’abonner à l’événement quand une cinématique se termine
        cinematicManager.OnCinematicEnd += HandleCinematicEnd;
    }

    private void OnEnable()
    {
        Timeline3Trigger.Cinematic3 += Cinematic3;
    }

    private void OnDestroy()
    {
        // Toujours se désabonner pour éviter les erreurs
        cinematicManager.OnCinematicEnd -= HandleCinematicEnd;
    }

    private void Cinematic3 ()
    {
        //reenFader.FadeIn();
        controlPanel.SetActive(false);
        //meraFPS.SetActive(false);
        //cameraCinematic.SetActive(true);
        //reenFader.FadeOut();
    }

    private void HandleCinematicEnd(string cinematicName)
    {
        if (cinematicName == "intro")
        {
            puzzlePanel.SetActive(true);
            puzzleManager.StartPlayMusic();
            Debug.Log("🧩 Puzzle activé après la cinématique d’intro !");
        }
        if (cinematicName == "middle")
        {
            screenFader.gameObject.SetActive(true);
            screenFader.FadeIn();
            manager.SetActive(false);
            screenFader.FadeOut();
            controlPanel.SetActive(true);
            cameraFPS.SetActive(true);
            cameraCinematic.SetActive(false);
            //ayerEnableComponents();
        }
        if (cinematicName == "outro")
        {
            screenFader.gameObject.SetActive(true);
            screenFader.FadeIn();
            sceneLoader.gameObject.SetActive(true);
            screenFader.FadeOut();
            sceneLoader.LoadScene("Scene1-Prologue");

        }
    }

    void DisableComponents(GameObject target)
    {
        foreach (var comp in target.GetComponents<MonoBehaviour>())
        {
            comp.enabled = false; // Désactive tous les scripts
        }
    }

    void EnableComponents(GameObject target)
    {
        foreach (var comp in target.GetComponents<MonoBehaviour>())
        {
            comp.enabled = true;
        }
    }
    
    //void PlayerEnableComponents()
    //{
    //    player.GetComponent<CapsuleCollider>().enabled = true;
    //    player.GetComponent<CharacterController>().enabled = true;
    //    player.GetComponent<PlayerController>().enabled = true;
    //    player.GetComponent<TouchController>().enabled = true;
    //    playerVisuel.GetComponent<Animator>().enabled = true;
    //}

    //void PlayerDisableComponents()
    //{
    //    player.GetComponent<CapsuleCollider>().enabled = false;
    //    player.GetComponent<CharacterController>().enabled = false;
    //    player.GetComponent<PlayerController>().enabled = false;
    //    player.GetComponent<TouchController>().enabled = false;
    //    playerVisuel.GetComponent<Animator>().enabled = false;
    //}
}
