using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance { get; private set; }

    [Header("Base de Données")]
    public GameDatabase gameDatabase;

    // Listes d'états de quêtes pour le joueur
    private List<QuestState> _allPlayerQuestStates = new List<QuestState>();
    
    [Header("Quêtes Actives du Joueur")]
    public List<QuestState> activeQuests = new List<QuestState>();
    [Header("Quêtes Terminées du Joueur")]
    public List<QuestState> completedQuests = new List<QuestState>();
    [Header("Quêtes Disponibles (débloquées)")]
    public List<QuestState> availableQuests = new();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeQuestStates();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeQuestStates()
    {
        if (gameDatabase == null)
        {
            Debug.LogError("Le GameDatabase n'a pas été assigné au QuestManager.");
            return;
        }

        if (_allPlayerQuestStates.Count == 0)
        {
            foreach (Quest questData in gameDatabase.allQuests)
            {
                QuestState newState = new QuestState(questData);
                _allPlayerQuestStates.Add(newState);

                // Logique pour les quêtes de départ (non verrouillées par défaut)
                if (!newState.isLocked)
                {
                    availableQuests.Add(newState);
                }
            }
            Debug.Log($"Système de quêtes initialisé. {_allPlayerQuestStates.Count} quêtes trouvées.");
        }
    }

    // Méthode pour obtenir l'état d'une quête par son ScriptableObject
    private QuestState GetQuestState(Quest questData)
    {
        return _allPlayerQuestStates.FirstOrDefault(qs => qs.questData == questData);
    }
    
    // Méthode pour débloquer une ou plusieurs quêtes par leur ID
    private void UnlockQuests(string[] questIDs)
    {
        foreach (var id in questIDs)
        {
            QuestState questToUnlockState = _allPlayerQuestStates.FirstOrDefault(qs => qs.questData.questID == id);
            
            if (questToUnlockState != null && questToUnlockState.isLocked)
            {
                questToUnlockState.isLocked = false;
                availableQuests.Add(questToUnlockState);
                Debug.Log($"Nouvelle quête débloquée : {questToUnlockState.questData.questName}");
                QuestEvents.QuestUnlocked(questToUnlockState.questData);
            }
            else if (questToUnlockState == null)
            {
                Debug.LogError($"ID de quête '{id}' non trouvé pour le déblocage.");
            }
        }
    }

    public void AcceptQuest(Quest questDataToAccept)
    {
        QuestState questStateToAccept = GetQuestState(questDataToAccept);

        if (questStateToAccept != null && !questStateToAccept.isActive && !questStateToAccept.isCompleted && !questStateToAccept.isLocked)
        {
            availableQuests.Remove(questStateToAccept);
            activeQuests.Add(questStateToAccept);
            questStateToAccept.isActive = true;
            
            questStateToAccept.questData.ResetProgress(); 

            Debug.Log($"Quête acceptée : {questStateToAccept.questData.questName}");
            QuestEvents.QuestAccepted(questStateToAccept.questData);

            // 2. Notifier l'objectif actuel (la progression initiale)
        
            // >>> LOGIQUE MISE À JOUR POUR UTILISER List<QuestObjective> <<<
            
            // Vérifie si la liste d'objectifs existe et contient au moins un élément.
            if (questStateToAccept.questData.objectives != null && questStateToAccept.questData.objectives.Count > 0)
            {
                // On prend le premier objectif [0] de la Liste
                QuestObjective initialObjective = questStateToAccept.questData.objectives[0];
                
                // Déclencher l'événement d'objectif
                QuestEvents.OnQuestObjectives(questStateToAccept.questData, initialObjective); 
            }
            else
            {
                Debug.LogWarning($"La quête '{questDataToAccept.questName}' acceptée n'a pas d'objectif initial.");
            }
        }
        else
        {
            Debug.LogWarning($"Impossible d'accepter la quête '{questDataToAccept.questName}'. État invalide.");
        }
    }

    public void CompleteQuest(Quest questDataToComplete)
    {
        QuestState questStateToComplete = GetQuestState(questDataToComplete);

        if (questStateToComplete != null && questStateToComplete.isActive && questStateToComplete.questData.IsCompleted())
        {
            activeQuests.Remove(questStateToComplete);
            completedQuests.Add(questStateToComplete);
            questStateToComplete.isCompleted = true;
            questStateToComplete.isActive = false;

            Debug.Log($"Quête terminée : {questStateToComplete.questData.questName}");
            
            questStateToComplete.questData.ApplyAllRewards();
            QuestEvents.QuestCompleted(questStateToComplete.questData);

            // Logique de déverrouillage
            // if (questDataToComplete.unlocksAnotherQuest && questDataToComplete.questToUnlock != null)
            // {
            //     UnlockQuests(new string[] { questDataToComplete.questToUnlock.questID });
            // }
        }
        else
        {
            Debug.LogWarning($"Impossible de terminer la quête '{questDataToComplete.questName}'. Elle n'est pas active ou n'est pas terminée.");
        }
    }

    public void UpdateObjectiveProgress(Quest questData, QuestObjective objective)
    {
        QuestEvents.ObjectiveUpdated(questData, objective);
        
        QuestState activeQuestState = activeQuests.FirstOrDefault(qs => qs.questData == questData);
        if (activeQuestState != null && activeQuestState.questData.IsCompleted())
        {
            Debug.Log($"Quête '{activeQuestState.questData.questName}' est prête à être rendue !");
        }
    }

    // --- Les méthodes de notification sont corrigées pour éviter l'erreur "Collection was modified" ---

    public void NotifyItemCollected(string itemName)
    {
        // On itère à l'envers sur une boucle for pour gérer les modifications
        for (int i = activeQuests.Count - 1; i >= 0; i--)
        {
            QuestState questState = activeQuests[i];
            foreach (QuestObjective objective in questState.questData.objectives)
            {
                if (objective is CollectItemObjective collectObjective)
                {
                    collectObjective.ItemCollected(itemName);
                    UpdateObjectiveProgress(questState.questData, collectObjective);
                }
            }
        }
    }
    
    public void NotifyPlanetVisited(string  PlanetName)
    {
        // On itère à l'envers sur une boucle for pour gérer les modifications
        for (int i = activeQuests.Count - 1; i >= 0; i--)
        {
            QuestState questState = activeQuests[i];
            foreach (QuestObjective objective in questState.questData.objectives)
            {
                if (objective is VisitPlanetObjective visitObjective)
                {
                    visitObjective.PlanetEntered(PlanetName);
                    UpdateObjectiveProgress(questState.questData, visitObjective);
                }
            }
        }
    }

    // public void NotifyEnemyKilled(string enemyTag)
    // {
    //     // On itère à l'envers sur une boucle for pour gérer les modifications
    //     for (int i = activeQuests.Count - 1; i >= 0; i--)
    //     {
    //         QuestState questState = activeQuests[i];
    //         foreach (QuestObjective objective in questState.questData.objectives)
    //         {
    //             if (objective is KillEnemyObjective killObjective)
    //             {
    //                 killObjective.EnemyKilled(enemyTag);
    //                 UpdateObjectiveProgress(questState.questData, killObjective);
    //             }
    //         }
    //     }
    // }
    
    // public void NotifyStoneGathered()
    // {
    //     // On itère à l'envers sur une boucle for pour gérer les modifications
    //     for (int i = activeQuests.Count - 1; i >= 0; i--)
    //     {
    //         QuestState questState = activeQuests[i];
    //         foreach (QuestObjective objective in questState.questData.objectives)
    //         {
    //             if (objective is GatherStoneObjective gatherObjective)
    //             {
    //                 gatherObjective.StoneGathered();
    //                 UpdateObjectiveProgress(questState.questData, gatherObjective);
    //             }
    //         }
    //     }
    // }
}
