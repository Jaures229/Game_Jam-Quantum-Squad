using UnityEngine;

public class StationManager : MonoBehaviour
{
    public CinematicManager cinematicManager;
    public GameObject puzzlePanel;

    public PuzzleManager puzzleManager;

    void Start()
    {
        // S’abonner à l’événement quand une cinématique se termine
        cinematicManager.OnCinematicEnd += HandleCinematicEnd;
    }

    private void OnDestroy()
    {
        // Toujours se désabonner pour éviter les erreurs
        cinematicManager.OnCinematicEnd -= HandleCinematicEnd;
    }

    private void HandleCinematicEnd(string cinematicName)
    {
        if (cinematicName == "intro")
        {
            puzzlePanel.SetActive(true);
            puzzleManager.StartPlayMusic();
            Debug.Log("🧩 Puzzle activé après la cinématique d’intro !");
        }
    }
}
