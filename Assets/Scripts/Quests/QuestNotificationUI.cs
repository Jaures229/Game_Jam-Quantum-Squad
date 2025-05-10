using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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
