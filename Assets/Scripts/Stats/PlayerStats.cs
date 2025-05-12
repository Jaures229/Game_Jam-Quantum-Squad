using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance { get; private set; }

    [Header("Stats de base")]
    public int level = 1;
    public int experience = 0;
    public int gold = 0;
    // ajouter une liste de badge

    [Header("Progression d'XP")]
    public float xpMultiplier = 1.5f;

    [Header("R�compenses de niveau")]
    public Image Player_badge_image;
    public List<Sprite> levelBadges = new(); // Ou List<string> si c�est juste des noms
    public Sprite currentBadge; // Badge actuel

    [Header("Progression par niveau")]
    public List<int> xpThresholds = new List<int> { 1000, 2000, 5000 }; // XP n�cessaire pour chaque niveau
    public List<string> PlayerGrade = new List<string> { "Apprentis Astronaute", "Maitre des �toiles", "Voyageur interstellaire", ""};
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
        experience += amount;
        Debug.Log($"XP gagn�e : {amount} (Total : {experience})");

        while (CanLevelUp())
        {
            LevelUp();
        }
    }

    private bool CanLevelUp()
    {
        if (level - 1 >= xpThresholds.Count) return false;
        return experience >= xpThresholds[level - 1];
    }


    public void AddGold(int amount)
    {
        gold += amount;
        Debug.Log($"Or gagn� : {amount} (Total : {gold})");
    }

    private void LevelUp()
    {
        experience -= xpThresholds[level - 1];
        level++;
        Debug.Log($"Niveau augment� ! Nouveau niveau : {level}");
        UpdateBadge();
    }
    private void UpdateBadge()
    {
        if (level - 1 < levelBadges.Count)
        {
            currentBadge = levelBadges[level - 1];
            Player_badge_image.sprite = currentBadge; // Met � jour l'image affich�e dans l'UI
            Debug.Log($"Nouveau badge : {currentBadge.name}");
        }
        else
        {
            Debug.LogWarning("Aucun badge d�fini pour ce niveau.");
        }
    }

}
