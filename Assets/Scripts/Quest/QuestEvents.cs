// using UnityEngine;
// using System;

// public static class QuestEvents
// {
//     // Événement déclenché lorsqu'une quête est acceptée.
//     // Argument: La quête qui a été acceptée.
//     public static event Action<Quest> OnQuestAccepted;

//     // Événement déclenché lorsqu'une quête est marquée comme terminée.
//     // Argument: La quête qui a été complétée.
//     public static event Action<Quest> OnQuestCompleted;

//     // Événement déclenché lorsqu'un objectif de quête est mis à jour (progression).
//     // Arguments: La quête à laquelle l'objectif appartient, et l'objectif mis à jour.
//     public static event Action<Quest, QuestObjective> OnObjectiveUpdated;

//     // Vous pouvez ajouter d'autres événements ici si nécessaire, par exemple :
//     // public static event Action<Quest> OnQuestFailed;
//     // public static event Action<QuestGiver, Quest> OnQuestOffered;
//     // public static event Action<Quest, QuestReward> OnRewardApplied;

//     // Méthodes pour déclencher les événements (facultatif mais recommandé pour la cohérence)
//     // Ces méthodes peuvent être appelées par le QuestManager ou d'autres systèmes.

//     public static void QuestAccepted(Quest quest)
//     {
//         OnQuestAccepted?.Invoke(quest);
//     }

//     public static void QuestCompleted(Quest quest)
//     {
//         OnQuestCompleted?.Invoke(quest);
//     }

//     public static void ObjectiveUpdated(Quest quest, QuestObjective objective)
//     {
//         OnObjectiveUpdated?.Invoke(quest, objective);
//     }
// }

using System;
using UnityEngine;

/// <summary>
/// Classe statique qui centralise tous les événements de quêtes.
/// D'autres classes peuvent s'abonner à ces événements pour être notifiées.
/// </summary>
public static class QuestEvents
{
    // Événement déclenché lorsqu'une quête est acceptée par le joueur.
    // L'argument est la quête qui a été acceptée.
    public static event Action<Quest> OnQuestAccepted;

    // Événement déclenché lorsqu'une quête est complétée et rendue.
    // L'argument est la quête qui a été complétée.
    public static event Action<Quest> OnQuestCompleted;
    
    // Événement déclenché à chaque fois que la progression d'un objectif est mise à jour.
    // Les arguments sont la quête et l'objectif mis à jour.
    public static event Action<Quest, QuestObjective> OnObjectiveUpdated;

    // Événement déclenché lorsqu'une nouvelle quête est débloquée.
    // L'argument est la quête qui a été débloquée.
    public static event Action<Quest> OnQuestUnlocked;

    // --- Méthodes pour déclencher les événements ---
    // Ces méthodes sont publiques et statiques pour être appelées depuis n'importe où.

    public static void QuestAccepted(Quest quest)
    {
        OnQuestAccepted?.Invoke(quest);
        Debug.Log($"EVENT: Quête '{quest.questName}' acceptée.");
    }

    public static void QuestCompleted(Quest quest)
    {
        OnQuestCompleted?.Invoke(quest);
        Debug.Log($"EVENT: Quête '{quest.questName}' complétée.");
    }

    public static void ObjectiveUpdated(Quest quest, QuestObjective objective)
    {
        OnObjectiveUpdated?.Invoke(quest, objective);
        Debug.Log($"EVENT: Progression de l'objectif '{objective.objectiveDescription}' de la quête '{quest.questName}' mise à jour.");
    }

    public static void QuestUnlocked(Quest unlockedQuest)
    {
        OnQuestUnlocked?.Invoke(unlockedQuest);
        Debug.Log($"EVENT: Quête '{unlockedQuest.questName}' débloquée.");
    }
}