using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameDatabase", menuName = "Game Data/Game Database")]
public class GameDatabase : ScriptableObject
{
    public List<Quest> allQuests = new List<Quest>();
    public List<QuestObjective> allObjectives = new List<QuestObjective>();
    public List<QuestReward> allRewards = new List<QuestReward>();
}
