using System.Collections.Generic;
using UnityEngine;
using System;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance { get; private set; }

    public List<Quest> activeQuests = new List<Quest>();
    public List<Quest> completedQuests = new List<Quest>();
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AcceptQuest(Quest questToAccept)
    {
        if (!activeQuests.Contains(questToAccept) && !completedQuests.Contains(questToAccept))
        {
            activeQuests.Add(questToAccept);
            questToAccept.ResetProgress();
            foreach (var objective in questToAccept.objectives)
            {
                objective.Initialize();
            }
            Debug.Log($"Quête acceptée : {questToAccept.questName}");
            // Utilisez la classe QuestEvents pour déclencher l'événement
            QuestEvents.QuestAccepted(questToAccept);
        }
    }

    public void CompleteQuest(Quest questToComplete)
    {
        if (activeQuests.Contains(questToComplete) && questToComplete.IsCompleted())
        {
            activeQuests.Remove(questToComplete);
            completedQuests.Add(questToComplete);
            Debug.Log($"Quête terminée : {questToComplete.questName}");

            questToComplete.ApplyAllRewards();

            // Utilisez la classe QuestEvents pour déclencher l'événement
            QuestEvents.QuestCompleted(questToComplete);
        }
        else
        {
            Debug.LogWarning($"Impossible de terminer la quête '{questToComplete.questName}'. Elle n'est pas active ou n'est pas terminée.");
        }
    }

    // Le reste des méthodes de QuestManager reste inchangé dans leur logique,
    // mais elles appelleront maintenant QuestEvents.ObjectiveUpdated


    
    public void UpdateObjectiveProgress(Quest quest, QuestObjective objective)
    {
        // Utilisez la classe QuestEvents pour déclencher l'événement
        QuestEvents.ObjectiveUpdated(quest, objective);
        if (quest.IsCompleted())
        {
            Debug.Log($"Quête '{quest.questName}' est prête à être rendue !");
        }
    }

    // ... (méthodes NotifyItemCollected et NotifyEnemyKilled restent les mêmes)
    public void NotifyItemCollected(string itemName)
    {
        foreach (Quest quest in activeQuests)
        {
            foreach (QuestObjective objective in quest.objectives)
            {
                // if (objective is CollectItemObjective collectObjective)
                // {
                //     collectObjective.ItemCollected(itemName);
                //     UpdateObjectiveProgress(quest, collectObjective); // Appelle QuestEvents.ObjectiveUpdated
                // }
            }
        }
    }

    public void NotifyEnemyKilled(string enemyTag)
    {
        foreach (Quest quest in activeQuests)
        {
            foreach (QuestObjective objective in quest.objectives)
            {
                // if (objective is KillEnemyObjective killObjective)
                // {
                //     killObjective.EnemyKilled(enemyTag);
                //     UpdateObjectiveProgress(quest, killObjective); // Appelle QuestEvents.ObjectiveUpdated
                // }
            }
        }
    }
}
