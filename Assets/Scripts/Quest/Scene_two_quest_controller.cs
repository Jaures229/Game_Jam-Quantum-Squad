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
        //SetCurrentQuestInfo();
    }

    // void SetCurrentQuestInfo()
    // {
    //     if (QuestManager.Instance != null && QuestManager.Instance.currentQuest != null)
    //     {
    //         Quest_name.text = QuestManager.Instance.currentQuest.questTitle;
    //         Quest_description.text = QuestManager.Instance.currentQuest.destination + "\n" + QuestManager.Instance.currentQuest.description;
    //     }
    //     else
    //     {
    //         Quest_name.text = "MISSION :";
    //         Quest_description.text = "Premi�re misson trouver \r\nla planete rouge.\r\nUn indice c'est la 4eme planete du syst�me.";
    //     }
    // }
}
