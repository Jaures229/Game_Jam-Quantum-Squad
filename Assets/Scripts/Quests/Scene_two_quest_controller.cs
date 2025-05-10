using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Scene_two_quest_controller : MonoBehaviour
{
    public TextMeshProUGUI Quest_name;
    public TextMeshProUGUI Quest_description;

    void Start()
    {
        SetCurrentQuestInfo();
    }

    void SetCurrentQuestInfo()
    {
        if (QuestManager.Instance != null && QuestManager.Instance.currentQuest != null)
        {
            Quest_name.text = QuestManager.Instance.currentQuest.questTitle;
            Quest_description.text = QuestManager.Instance.currentQuest.destination + "\n" + QuestManager.Instance.currentQuest.description;
        }
        else
        {
            Quest_name.text = "Mission :";
            Quest_description.text = "Rien pour le moment";
        }
    }
}
