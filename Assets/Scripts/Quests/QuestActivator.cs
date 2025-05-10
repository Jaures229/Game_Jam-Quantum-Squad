using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestActivator : MonoBehaviour
{
    public Quest linkedQuest;
    public bool deactivateOnStart = true;

    private void Start()
    {

        if (deactivateOnStart)
            gameObject.SetActive(false);

        OnQuestActivated(QuestManager.Instance.currentQuest);
        //QuestEvents.OnQuestActivated += OnQuestActivated;
        QuestEvents.OnQuestCompleted += OnQuestCompleted;
    }

    private void OnDestroy()
    {
        //QuestEvents.OnQuestActivated -= OnQuestActivated;
        //QuestEvents.OnQuestCompleted -= OnQuestCompleted;
    }

    private void OnQuestActivated(Quest quest)
    {
        if (linkedQuest != null)
        {
            if (quest.Equals(linkedQuest))
            {
                gameObject.SetActive(true);
            }
            else
            {
                Debug.Log("This is not the adequate quest for the objet");
            }
        } else
        {
            Debug.Log("Linked Quest is null");
        }
    }

    private void OnQuestCompleted(Quest quest)
    {
        if (quest == linkedQuest)
        {
            Debug.Log($"Deactivating {gameObject.name} after quest completion.");
            gameObject.SetActive(false);
        }
    }
}
