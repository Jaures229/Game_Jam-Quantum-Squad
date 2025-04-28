using UnityEngine;

[CreateAssetMenu(fileName = "NewQuest", menuName = "Quests/Quest")]
public class Quest : ScriptableObject
{
    public string questName;
    [TextArea]
    public string description;

    public QuestGoal[] goals; // Objectifs de la qu�te
    public int rewardXP;
    public int rewardCredits;
}
