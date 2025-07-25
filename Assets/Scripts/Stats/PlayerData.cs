using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStatsData", menuName = "Data/Player Stats")]
public class PlayerStatsData : ScriptableObject
{
    [Header("Stats de base")]
    public int level = 1;
    public int experience = 0;
    public int gold = 0;

    [Header("XP & Level")]
    public float xpMultiplier = 1.5f;
    public List<int> xpThresholds = new() { 1000, 2000, 5000 };
    public List<string> playerGrades = new() { "Apprentis Astronaute", "Maitre des étoiles", "Voyageur interstellaire", "" };

    [Header("Badges par niveau")]
    public List<Sprite> levelBadges = new();
}
