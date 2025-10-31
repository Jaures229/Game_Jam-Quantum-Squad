using UnityEngine;
using TMPro; // N'oubliez pas ceci pour TextMeshPro

public class QuestUIProgression : MonoBehaviour
{
    [Header("Références UI")]
    [Tooltip("Le composant TextMeshPro pour afficher le nom de la quête.")]
    [SerializeField] private TextMeshProUGUI questNameText;

    [Tooltip("Le composant TextMeshPro pour afficher la description ou la progression de l'objectif.")]
    [SerializeField] private TextMeshProUGUI questProgressText;
    [SerializeField] private GameObject questUIPanel;

    // Référence au GameObject racine de ce panneau UI (souvent le GameObject sur lequel ce script est attaché)

    void Awake()
    {
        // Pas besoin de récupérer this.gameObject.
        // Initialisation du panneau :
        if (questUIPanel != null)
        {
            questUIPanel.SetActive(false);
        }
    }

    private void OnEnable()
    {
        // --- Abonnement aux Événements de Quête ---

        // Quand une quête est acceptée : (string = Nom de la quête)
        QuestEvents.OnQuestAccepted += HandleQuestAccepted;

        // Quand une quête est terminée : (string = Nom de la quête)
        QuestEvents.OnQuestCompleted += HandleQuestCompleted;

        // Quand un objectif est mis à jour : (string = Nom de l'objectif/Progression)
        QuestEvents.OnObjectiveUpdated += HandleObjectiveUpdated;

        QuestEvents.QuestObjectives += InitDefaultProgress;



        // (Optionnel) Si vous avez des événements de retrait/échec de quête
        // QuestEvents.OnQuestFailed += HandleQuestCompleted; 
    }

    private void OnDisable()
    {
        // --- Désabonnement des Événements ---
        QuestEvents.OnQuestAccepted -= HandleQuestAccepted;
        QuestEvents.OnQuestCompleted -= HandleQuestCompleted;
        QuestEvents.OnObjectiveUpdated -= HandleObjectiveUpdated;

        QuestEvents.QuestObjectives -= InitDefaultProgress;
        // QuestEvents.OnQuestFailed -= HandleQuestCompleted;
    }

    // --------------------------------------------------------------------------
    //                         GESTION DES ÉVÉNEMENTS
    // --------------------------------------------------------------------------

    /// <summary>
    /// Active le panneau UI et affiche le nom de la quête acceptée.
    /// </summary>
    private void HandleQuestAccepted(Quest quest)
    {
        if (questUIPanel != null)
        {
            questUIPanel.SetActive(true); // RÉACTIVE LE PANNEAU UI
        }

        if (questNameText != null)
        {
            questNameText.text = quest.questName;
        }

        // Optionnel : Réinitialiser le texte de progression au début
        if (questProgressText != null)
        {
            questProgressText.text = "";
        }
    }

    /// <summary>
    /// Désactive le panneau UI lorsque la quête est terminée (succès ou échec).
    /// </summary>
    private void HandleQuestCompleted(Quest quest)
    {
        if (questUIPanel != null)
        {
            questUIPanel.SetActive(false); // DÉSACTIVE LE PANNEAU UI
        }

        Debug.Log($"Quête {quest.questName} terminée. HUD de quête désactivé.");


    }

    /// <summary>
    /// Met à jour le texte affichant la progression de l'objectif.
    /// </summary>
    /// <param name="objectiveDescription">La chaîne de texte décrivant l'objectif actuel et sa progression (ex: "Tuer 5/10 Gobelins").</param>
    private void HandleObjectiveUpdated(Quest quest, QuestObjective objective)
    {
        if (questProgressText != null)
        {
            questProgressText.text = objective.GetProgressText();
        }
    }

    private void InitDefaultProgress(Quest quest, QuestObjective objective)
    {
        if (questProgressText != null)
        {
            questProgressText.text = objective.GetProgressText();
        }
    }
}
