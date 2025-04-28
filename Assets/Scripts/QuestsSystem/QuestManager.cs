using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance { get; private set; }

    public List<Quest> availableQuests;
    public Quest currentQuest;
    public List<Quest> completedQuests = new List<Quest>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Détruire les doublons
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // Reste entre les scènes
    }

    public void StartQuest(Quest quest)
    {
        currentQuest = quest;
        Debug.Log("Started quest: " + quest.questName);
    }

    public void CompleteCurrentQuest()
    {
        if (currentQuest != null)
        {
            completedQuests.Add(currentQuest);
            Debug.Log("Completed quest: " + currentQuest.questName);
            currentQuest = null;
        }
    }

    public bool HasActiveQuest()
    {
        return currentQuest != null;
    }
}
