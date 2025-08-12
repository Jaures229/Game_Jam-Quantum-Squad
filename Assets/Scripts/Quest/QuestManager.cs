// using System.Collections.Generic;
// using UnityEngine;
// using System;

// public class QuestManager : MonoBehaviour
// {
//     public static QuestManager Instance { get; private set; }

//     public List<Quest> activeQuests = new List<Quest>();
//     public List<Quest> completedQuests = new List<Quest>();
//     private void Awake()
//     {
//         if (Instance == null)
//         {
//             Instance = this;
//             DontDestroyOnLoad(gameObject);
//         }
//         else
//         {
//             Destroy(gameObject);
//         }
//     }

//     public void AcceptQuest(Quest questToAccept)
//     {
//         if (!activeQuests.Contains(questToAccept) && !completedQuests.Contains(questToAccept))
//         {
//             activeQuests.Add(questToAccept);
//             questToAccept.ResetProgress();
//             foreach (var objective in questToAccept.objectives)
//             {
//                 objective.Initialize();
//             }
//             Debug.Log($"Quête acceptée : {questToAccept.questName}");
//             // Utilisez la classe QuestEvents pour déclencher l'événement
//             QuestEvents.QuestAccepted(questToAccept);
//         }
//     }

//     public void CompleteQuest(Quest questToComplete)
//     {
//         if (activeQuests.Contains(questToComplete) && questToComplete.IsCompleted())
//         {
//             activeQuests.Remove(questToComplete);
//             completedQuests.Add(questToComplete);
//             Debug.Log($"Quête terminée : {questToComplete.questName}");

//             questToComplete.ApplyAllRewards();

//             // Utilisez la classe QuestEvents pour déclencher l'événement
//             QuestEvents.QuestCompleted(questToComplete);
//         }
//         else
//         {
//             Debug.LogWarning($"Impossible de terminer la quête '{questToComplete.questName}'. Elle n'est pas active ou n'est pas terminée.");
//         }
//     }

//     // Le reste des méthodes de QuestManager reste inchangé dans leur logique,
//     // mais elles appelleront maintenant QuestEvents.ObjectiveUpdated


    
//     public void UpdateObjectiveProgress(Quest quest, QuestObjective objective)
//     {
//         // Utilisez la classe QuestEvents pour déclencher l'événement
//         QuestEvents.ObjectiveUpdated(quest, objective);
//         if (quest.IsCompleted())
//         {
//             Debug.Log($"Quête '{quest.questName}' est prête à être rendue !");
//         }
//     }

//     // ... (méthodes NotifyItemCollected et NotifyEnemyKilled restent les mêmes)
//     public void NotifyItemCollected(string itemName)
//     {
//         foreach (Quest quest in activeQuests)
//         {
//             foreach (QuestObjective objective in quest.objectives)
//             {
//                 // if (objective is CollectItemObjective collectObjective)
//                 // {
//                 //     collectObjective.ItemCollected(itemName);
//                 //     UpdateObjectiveProgress(quest, collectObjective); // Appelle QuestEvents.ObjectiveUpdated
//                 // }
//             }
//         }
//     }

//     public void NotifyEnemyKilled(string enemyTag)
//     {
//         foreach (Quest quest in activeQuests)
//         {
//             foreach (QuestObjective objective in quest.objectives)
//             {
//                 // if (objective is KillEnemyObjective killObjective)
//                 // {
//                 //     killObjective.EnemyKilled(enemyTag);
//                 //     UpdateObjectiveProgress(quest, killObjective); // Appelle QuestEvents.ObjectiveUpdated
//                 // }
//             }
//         }
//     }
// }


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
    public List<QuestState> availableQuests = new List<QuestState>();

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
