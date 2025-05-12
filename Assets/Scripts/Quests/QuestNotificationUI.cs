using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[System.Serializable]
public class QuestNotificationPair
{
    public Quest quest;
    public GameObject panel;
}

/*
public class QuestNotificationUI : MonoBehaviour
{
    public GameObject panel;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;

    void Start()
    {
        panel.SetActive(false);
        QuestEvents.OnQuestCompleted += ShowNotification;
    }

    void OnDestroy()
    {
        QuestEvents.OnQuestCompleted -= ShowNotification;
    }

    void ShowNotification(Quest quest)
    {
        panel.SetActive(true);
        titleText.text = "Quête terminée : " + quest.questTitle;
        descriptionText.text = quest.description;
    }

    public void HideNotification()
    {
        panel.SetActive(false);
    }
}
*/

public class QuestNotificationUI : MonoBehaviour
{
    [SerializeField]
    private List<QuestNotificationPair> questNotifications = new();

    void Start()
    {
        QuestEvents.OnQuestCompleted += ShowNotification;
    }

    void OnDestroy()
    {
        QuestEvents.OnQuestCompleted -= ShowNotification;
    }
    void ShowNotification(Quest completedQuest)
    {
        foreach (var pair in questNotifications)
        {
            if (pair.quest == completedQuest)
            {
                pair.panel.SetActive(true);
                return;
            }
        }

        Debug.LogWarning($"Aucune notification trouvée pour la quête : {completedQuest.questTitle}");
    }
}
