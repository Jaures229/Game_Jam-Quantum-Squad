using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewQuest", menuName = "Quests/Quest")]
public class Quest : ScriptableObject
{
    public string questName = "Nouvelle Quête";
    [TextArea(3, 10)]
    public string description = "Description de la quête.";
    public List<QuestObjective> objectives = new List<QuestObjective>();
    public List<QuestReward> rewards = new List<QuestReward>(); 
    public bool IsCompleted()
    {
        foreach (QuestObjective objective in objectives)
        {
            if (!objective.IsCompleted())
            {
                return false;
            }
        }
        return true;
    }

    public void ResetProgress()
    {
        foreach (QuestObjective objective in objectives)
        {
            objective.ResetObjective();
        }
    }

    public void ApplyAllRewards()
    {
        foreach (QuestReward reward in rewards)
        {
            reward.ApplyReward();
        }
    }
}
