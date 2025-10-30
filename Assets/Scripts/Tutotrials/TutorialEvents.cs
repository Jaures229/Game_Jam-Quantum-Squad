using System;
using UnityEngine;

public static class TutorialEvents
{
    // Déclenché à chaque changement d'étape
    public static event Action<int> OnTutorialStepAdvanced;

    // Déclenché quand le tutoriel est complètement terminé
    public static event Action OnTutorialEnded;

    public static event Action<string> OnTutorialStarted;
    public static event Action<string> OnTutorialObjectiveCompleted;
    public static event Action<string>  OnObjectiveAwaiting;

    public static void TutorialStepAdvanced(int newStepIndex)
    {
        OnTutorialStepAdvanced?.Invoke(newStepIndex);
    }

    public static void TutorialEnded()
    {
        OnTutorialEnded?.Invoke();
    }

    public static void TutorialStarted(string TutorialNameId)
    {
        OnTutorialStarted?.Invoke(TutorialNameId);
    }

    public static void TutorialObjectiveCompleted(string objective_id)
    {
        OnTutorialObjectiveCompleted?.Invoke(objective_id);
    }
    public static void ObjectiveAwaiting(string objective_id)
    {
        OnObjectiveAwaiting?.Invoke(objective_id);
    }
}
