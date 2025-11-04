using UnityEngine;
using TMPro;

public class WarningSystem : MonoBehaviour
{
    // Référénces UI
    [Header("Références UI")]
    [Tooltip("Le GameObject racine du panneau d'alerte (à activer/désactiver).")]
    public GameObject warningPanel;
    [Tooltip("Le composant TextMeshPro pour afficher le message d'alerte.")]
    public TextMeshProUGUI warning_text;
    
    // Référénces Monde
    [Header("Références Monde")]
    [Tooltip("La Transform du joueur/vaisseau.")]
    public Transform joueur;

    [Header("Paramètres d'Alerte")]
    [Tooltip("Distance maximale à laquelle l'alerte doit s'activer (ex: 50 unités).")]
    public float distanceAlerte = 50.0f; 
    
    // État interne
    private Transform cibleActuelle = null;
    private string cibleNom = "la planète"; // Nom générique par défaut

    // Constante pour le nom de l'événement de détection de cible
    private const string TARGET_EVENT_NAME = "OnTargetSet";

    void Start()
    {
        // Vérifications de sécurité de base
        if (warningPanel == null || warning_text == null || joueur == null)
        {
            Debug.LogError("Les références UI (Panneau, Texte) ou Joueur ne sont pas assignées dans le WarningSystem.");
            return;
        }
        warningPanel.SetActive(false);
        
        // S'abonner aux événements du GPSManager
        if (GPSManager.Instance != null)
        {
            // Abonnements pour recevoir la nouvelle cible
            GPSManager.Instance.OnTargetSet += HandleNewTarget;
            GPSManager.Instance.OnTargetReached += HandleTargetClear; // Assurez-vous d'avoir cet événement si la cible est retirée
        }
        else
        {
            Debug.LogError("GPSManager.Instance n'est pas disponible. Le WarningSystem ne fonctionnera pas.");
        }
    }
    
    void OnDestroy()
    {
        // Se désabonner pour éviter les fuites de mémoire (très important !)
        if (GPSManager.Instance != null)
        {
            GPSManager.Instance.OnTargetSet -= HandleNewTarget;
            GPSManager.Instance.OnTargetReached -= HandleTargetClear;
        }
    }

    // Gère la réception d'une nouvelle cible du GPSManager
    private void HandleNewTarget(Transform newTarget, string newTargetID)
    {
        cibleActuelle = newTarget;
        cibleNom = newTargetID; // Utiliser l'ID/Nom de la cible pour le message

        // Si une nouvelle cible est définie, réinitialiser l'alerte
        if (cibleActuelle != null)
        {
            warningPanel.SetActive(false);
        }
    }
    
    // Gère la suppression de la cible du GPSManager
    private void HandleTargetClear(string targetID)
    {
        if (targetID == cibleNom)
        {
            cibleActuelle = null;
            cibleNom = "la destination";
            warningPanel.SetActive(false); // S'assurer que l'alerte est désactivée si la cible est atteinte/retirée
        }
    }

    void Update()
    {
        // 1. Vérification de l'existence de la cible et du joueur
        if (cibleActuelle == null || joueur == null)
        {
            // S'assurer que le panneau est désactivé s'il n'y a pas de cible
            if (warningPanel.activeSelf)
            {
                warningPanel.SetActive(false);
            }
            return;
        }

        // 2. Calcul de la distance
        float distanceRestante = Vector3.Distance(joueur.position, cibleActuelle.position);

        // 3. Logique d'alerte
        if (distanceRestante <= distanceAlerte)
        {
            
            // ALERTE PROXIMITÉ
            if (!warningPanel.activeSelf)
            {
                warningPanel.SetActive(true);
            }
            
            // Mettre à jour le message
            warning_text.text = $"Proximité critique avec {cibleNom} !";
        }
        else
        {
            // Hors de la zone d'alerte
            if (warningPanel.activeSelf)
            {
                warningPanel.SetActive(false);
            }
        }
    }
}
