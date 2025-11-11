using UnityEngine;
using UnityEngine.Events; // Pour les événements déclenchés dans l'Inspecteur

public class ConditionalInteraction : MonoBehaviour
{
    [Header("Conditions Requises")]
    [Tooltip("L'ID de la planète où l'interaction est valide (ex: MARS_COLONY).")]
    public string RequiredPlanetID;
    
    [Tooltip("Le nom de la Quête Principale requise pour activer cette interaction.")]
    public string RequiredQuestName;
    
    [Tooltip("L'ID de l'Objectif de Quête précis requis pour cette étape.")]
    public string RequiredObjectiveID; // Ex: "Deliver_Fuel_To_NPC"

    [Header("Action si Conditions Remplies")]
    [Tooltip("L'action (dialogue, puzzle, etc.) à exécuter si le contexte est correct.")]
    public UnityEvent OnContextMatch;
    
    [Header("Action si Contexte Incorrect")]
    [Tooltip("Action par défaut si les conditions ne sont pas remplies (ex: jouer un son d'erreur).")]
    public UnityEvent OnContextMiss;
    
    public void TryInteract()
    {
        // 1. Récupérer le contexte
        string currentPlanet = GameStateManager.Instance.location;
        Quest activeQuest = QuestManager.Instance.activeQuests[0].questData; // Supposons que vous ayez une référence
        string currentObjectiveID = QuestManager.Instance.activeQuests[0].questData.objectives[0].objective_id; 

        // 2. Vérification des conditions
        bool isCorrectPlanet = currentPlanet == RequiredPlanetID;
        bool isCorrectQuest = activeQuest != null && activeQuest.questName == RequiredQuestName;
        bool isCorrectObjective = string.IsNullOrEmpty(RequiredObjectiveID) || currentObjectiveID == RequiredObjectiveID;
        
        if (isCorrectPlanet && isCorrectQuest && isCorrectObjective)
        {
            // Contexte parfait : déclencher l'action désirée
            OnContextMatch.Invoke();
        }
        else
        {
            // Contexte incorrect : déclencher l'action par défaut
            Debug.Log($"Interaction bloquée sur {gameObject.name}. Contexte actuel : Planète={currentPlanet}, Quête={activeQuest?.questName}, Objectif={currentObjectiveID}");
            OnContextMiss.Invoke();
        }
    }
}