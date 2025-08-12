using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCollectItemObjective", menuName = "Quests/Objectives/Collect Item")]
public class CollectItemObjective : QuestObjective
{
    public string itemName; // Nom de l'objet à collecter
    public int requiredAmount = 0;
    public int currentAmount = 0;

    public override void Initialize()
    {
        // Optionnel : S'abonner à un événement global pour la collecte d'objets
        // GameManager.OnItemCollected += OnItemCollected;
    }

    public override bool IsCompleted()
    {
        return currentAmount >= requiredAmount;
    }

    public override void ResetObjective()
    {
        currentAmount = 0;
    }

    public void ItemCollected(string collectedItemName)
    {
        if (collectedItemName == itemName && currentAmount < requiredAmount)
        {
            currentAmount++;
            // Optionnel : Notifier le QuestTracker ou le UI de la progression
        }
    }

    public override string GetProgressText()
    {
        return $"{objectiveDescription} ({currentAmount}/{requiredAmount})";
    }

    // N'oubliez pas de vous désabonner si vous utilisez des événements globaux
    // private void OnDisable() { if (Application.isPlaying) { GameManager.OnItemCollected -= OnItemCollected; } }
}
