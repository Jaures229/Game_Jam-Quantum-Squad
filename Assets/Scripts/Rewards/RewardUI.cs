using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RewardUI : MonoBehaviour
{
    public Slider XpSlider;
    public TextMeshProUGUI Number_xp;

    void Start()
    {
        QuestEvents.ApplyRewards += ChangePlayerUIStats;
        UpdateXpUI(); // au démarrage
    }

    void OnDestroy()
    {
        QuestEvents.ApplyRewards -= ChangePlayerUIStats;
    }

    void ChangePlayerUIStats(string update)
    {
        if (update == "XP" || update == "ALL")
        {
            UpdateXpUI();
        }
    }

    void UpdateXpUI()
    {
        var stats = PlayerStats.Instance;
        int currentLevel = PlayerStats.Instance.level;

        // Vérifie que le niveau actuel est dans la liste des paliers
        if (currentLevel - 1 < stats.xpThresholds.Count)
        {
            int xpMax = stats.xpThresholds[currentLevel - 1];
            int xpCurrent = stats.experience;

            XpSlider.maxValue = xpMax;
            XpSlider.value = xpCurrent;
        }
        else
        {
            // Niveau max atteint
            XpSlider.maxValue = 1;
            XpSlider.value = 1;
        }

        Number_xp.text = $"{stats.experience}/{stats.xpThresholds[currentLevel - 1]}";
    }
}
