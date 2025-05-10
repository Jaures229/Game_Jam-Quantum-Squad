using System.Collections.Generic;
using UnityEngine;

public abstract class Reward : ScriptableObject
{
    public abstract void ApplyReward();
}

[CreateAssetMenu(fileName = "NewQuest", menuName = "Quests/Quest")]
public class Quest : ScriptableObject
{
    public string questTitle;
    [TextArea]


    public string questName; 
    [TextArea]
    public string description;
    [TextArea]
    public string destination;

    public List<QuestGoal> goals; // objectifs modulaires
    public List<Reward> rewards; // récompenses modulaires

    public bool IsCompleted()
    {
        foreach (var goal in goals)
        {
            if (!goal.IsCompleted)
                return false;
        }
        return true;
    }

}
