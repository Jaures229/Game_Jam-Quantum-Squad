using UnityEngine;

[System.Serializable]
public class QuestGoal
{
    public string goalDescription;
    public GoalType goalType;
    public int requiredAmount;
    public int currentAmount;

    public bool IsCompleted => currentAmount >= requiredAmount;
}

public enum GoalType
{
    ExplorePlanet,
    CollectResource,
    DefeatEnemies,
    TalkToNPC
}
