using Unity.VisualScripting;
using UnityEngine;

public class SceneTrigger : MonoBehaviour
{
    [Header("Paramètres de la Scène Cible")]
    [Tooltip("Le nom exact de la scène à charger. Doit être ajouté aux Build Settings !")]
    public string nomDeSceneCible;

    [Tooltip("L'ID ou le Tag que votre vaisseau de joueur doit avoir (ex: 'PlayerShip').")]
    public string tagDuVaisseauJoueur = "Player";

    public SceneLoader sceneLoader;
    public GameObject loading_panel;

    [Header("Paramètres d'Écoute")]
    [Tooltip("L'ID unique que ce loader écoute (ex: 'MarsEntrance').")]
    public string idCibleAEcouter;

    void Start()
    {
        // 🚨 S'abonner à l'événement du Manager
        if (GPSManager.Instance != null)
        {
            GPSManager.Instance.OnTargetReached += OnCibleAtteinte;
        }
    }

    void OnDestroy()
    {
        // Se désabonner
        if (GPSManager.Instance != null)
        {
            GPSManager.Instance.OnTargetReached -= OnCibleAtteinte;
        }
    }
    
    /// <summary>
    /// Méthode appelée lorsque la cible est atteinte via l'événement du GPSManager.
    /// </summary>
    /// <param name="reachedId">L'ID de la cible qui vient d'être atteinte.</param>
    private void OnCibleAtteinte(string reachedId)
    {
        // 1. Comparer l'ID reçu avec l'ID que ce script écoute
        if (reachedId == idCibleAEcouter)
        {
            // 2. L'ID correspond ! Charger la scène.
            if (!string.IsNullOrEmpty(nomDeSceneCible))
            {
                if ( QuestManager.Instance != null && QuestManager.Instance.activeQuests.Count > 0)
                {
                    QuestManager.Instance.NotifyPlanetVisited(reachedId);
                }
                Debug.Log($"[SceneLoaderListener: {idCibleAEcouter}] La cible correspond. Chargement de la scène : {nomDeSceneCible}");
                loading_panel.SetActive(true);
                sceneLoader.LoadScene(nomDeSceneCible);
            }
            else
            {
                Debug.LogError($"[SceneLoaderListener: {idCibleAEcouter}] Scène cible non définie !");
            }
        }
    }
}
