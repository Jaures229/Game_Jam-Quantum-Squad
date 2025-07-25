using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance { get; private set; }

    [Header("Référence aux données")]
    public PlayerStatsData data;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void AddExperience(int amount)
    {
        data.experience += amount;
        Debug.Log($"XP gagnée : {amount} (Total : {data.experience})");

        while (CanLevelUp())
        {
            LevelUp();
        }
    }

    private bool CanLevelUp()
    {
        if (data.level - 1 >= data.xpThresholds.Count) return false;
        return data.experience >= data.xpThresholds[data.level - 1];
    }

    private void LevelUp()
    {
        data.experience -= data.xpThresholds[data.level - 1];
        data.level++;
        Debug.Log($"Niveau augmenté ! Nouveau niveau : {data.level}");
        UpdateBadge();
    }

    public void AddGold(int amount)
    {
        data.gold += amount;
        Debug.Log($"Or gagné : {amount} (Total : {data.gold})");
    }

    private void UpdateBadge()
    {
        if (data.level - 1 < data.levelBadges.Count)
        {
            var badge = data.levelBadges[data.level - 1];
            //Debug.Log($"Badge mis à jour : {badge?.name}");
            // Tu peux ici déclencher un event ou appeler l'UI pour mettre à jour l'image
        }
        else
        {
            Debug.LogWarning("Aucun badge défini pour ce niveau.");
        }
    }
}
