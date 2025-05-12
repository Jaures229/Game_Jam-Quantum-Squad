using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UnlockQuestReward", menuName = "Rewards/Unlock Quest")]
public class UnlockQuestReward : Reward
{
    public List<Quest> questsToUnlock;
    public bool switch_directly = false;
    public override void ApplyReward()
    {
        if (switch_directly)
        {
            Debug.Log("S is true");
        }
        else {
            Debug.Log("S is not");
        }
        foreach (var quest in questsToUnlock)
        {
            if (switch_directly)
            {
                Debug.Log("Switched Directly");
                // unlock the quest 
                QuestManager.Instance.UnlockQuest(quest);

                // and switch directly to the unlocked quest

                QuestManager.Instance.StartQuest(quest);
            }
            else
            {
                QuestManager.Instance.UnlockQuest(quest);
            }
            Debug.Log($"Quête débloquée : {quest.questTitle}");
        }
    }
}
