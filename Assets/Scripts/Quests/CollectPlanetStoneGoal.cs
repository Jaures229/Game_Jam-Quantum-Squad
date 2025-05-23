using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Quests/Goals/Collect Planet Stone Goal")]
public class CollectPlanetStoneGoal : QuestGoal
{
    public string planetName;
    public int requiredAmount = 1;
    private int currentAmount = 0;

    private string progression;
    public override void Initialize()
    {
        currentAmount = 0;
        IsCompleted = false;
        progression = $"Mat�riaux trouv�s : 0/{requiredAmount}";

        // S'abonner � l��v�nement de collecte
        QuestEvents.OnPlanetStoneCollected += OnStoneCollected;

        Debug.Log("Goal has been initialize");
    }

    public override void OnEventRaised(params object[] args) { }

    private void OnStoneCollected(string collectedFromPlanet, string stoneType)
    {

        Debug.Log("Called");
        if (IsCompleted) return;

        if (collectedFromPlanet == planetName)
        {
            currentAmount++;
            Debug.Log($"Stone from {planetName} collected: {currentAmount}/{requiredAmount}");

            ChangeProgression();
            NotifyProgressChanged(); // Notifie l'UI

            if (currentAmount >= requiredAmount)
            {
                IsCompleted = true;
                Debug.Log("Objectif termin� !");

                // Se d�sabonner
                QuestEvents.OnPlanetStoneCollected -= OnStoneCollected;

                //Avertir le QuestManager
                QuestManager.Instance.CheckQuestCompletion();
            }
        }
        Debug.Log(planetName);
        Debug.Log(collectedFromPlanet);
    }

    public override string GetProgression()
    {
        return progression;
    }

    public override void ChangeProgression()
    {
        progression = $"Mat�riaux trouv�s : {currentAmount}/{requiredAmount}";
    }
}
