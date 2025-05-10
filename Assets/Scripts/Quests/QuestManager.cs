using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance { get; private set; }
    public List<Quest> quests;

    public Quest currentQuest;
    public List<Quest> completedQuests = new();

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public void StartQuest(Quest quest)
    {
        currentQuest = quest;
        foreach (var goal in quest.goals)
        {
            goal.Initialize();
        }
        Debug.Log("Quest started: " + quest.questName);
    }

    public void CheckProgress()
    {
        if (currentQuest == null) return;

        foreach (var goal in currentQuest.goals)
        {
            if (!goal.IsCompleted) return;
        }

        //QuestEvents.OnQuestCompleted?.Invoke(currentQuest);
        CompleteQuest();
        // appeler la notification final;

    }
    public List<Quest> GetAllQuests()
    { 
        return quests;
    }

    public void SetCurrentQuest(Quest quest)
    {
        currentQuest = quest;
    }

    public void CheckQuestCompletion()
    {
        if (currentQuest == null) return;

        foreach (var goal in currentQuest.goals)
        {
            if (!goal.IsCompleted) return;
        }

        Debug.Log("Quête terminée : " + currentQuest.questTitle);
        QuestEvents.OnQuestCompleted?.Invoke(currentQuest);

        CompleteQuest();
    }

    private void CompleteQuest()
    {
        Debug.Log("Quest complete: " + currentQuest.questName);
        completedQuests.Add(currentQuest);

        // Enlever la quête terminée de la liste
        quests.Remove(currentQuest);

        currentQuest = null;
    }


}
