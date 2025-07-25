using UnityEngine;
using System;

public static class QuestEvents
{
    // Événement déclenché lorsqu'une quête est acceptée.
    // Argument: La quête qui a été acceptée.
    public static event Action<Quest> OnQuestAccepted;

    // Événement déclenché lorsqu'une quête est marquée comme terminée.
    // Argument: La quête qui a été complétée.
    public static event Action<Quest> OnQuestCompleted;

    // Événement déclenché lorsqu'un objectif de quête est mis à jour (progression).
    // Arguments: La quête à laquelle l'objectif appartient, et l'objectif mis à jour.
    public static event Action<Quest, QuestObjective> OnObjectiveUpdated;

    // Vous pouvez ajouter d'autres événements ici si nécessaire, par exemple :
    // public static event Action<Quest> OnQuestFailed;
    // public static event Action<QuestGiver, Quest> OnQuestOffered;
    // public static event Action<Quest, QuestReward> OnRewardApplied;

    // Méthodes pour déclencher les événements (facultatif mais recommandé pour la cohérence)
    // Ces méthodes peuvent être appelées par le QuestManager ou d'autres systèmes.

    public static void QuestAccepted(Quest quest)
    {
        OnQuestAccepted?.Invoke(quest);
    }

    public static void QuestCompleted(Quest quest)
    {
        OnQuestCompleted?.Invoke(quest);
    }

    public static void ObjectiveUpdated(Quest quest, QuestObjective objective)
    {
        OnObjectiveUpdated?.Invoke(quest, objective);
    }
}