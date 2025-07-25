using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class QuestReward : ScriptableObject
{
    public string rewardDescription = "Nouvelle récompense"; // Pour l'affichage UI

    // Méthode abstraite pour appliquer la récompense
    public abstract void ApplyReward();
}
