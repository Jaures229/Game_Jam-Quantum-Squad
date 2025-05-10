using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*[CreateAssetMenu(menuName = "Quests/Goals/Collect Planet Stone Goal")]
public class CollectPlanetStoneGoal : QuestGoal
{
    public string planetName;
    public int requiredAmount = 1;
    private int currentAmount = 0;

    public string Progression;

    public override void Initialize()
    {
        currentAmount = 0;
        IsCompleted = false;

        // S'abonner à l’événement
        QuestEvents.OnPlanetStoneCollected += OnStoneCollected;
    }
    public override void OnEventRaised(params object[] args) { }

    private void OnStoneCollected(string collectedFromPlanet, string stoneType)
    {
        if (IsCompleted) {
            return;
        }

        *//*NotifyProgressChanged();*//*

        if (collectedFromPlanet == planetName)
        {
            currentAmount++;
            Debug.Log($"Stone from {planetName} collected: {currentAmount}/{requiredAmount}");

            // update progression
            ChangeProgression();
            NotifyProgressChanged();
            if (currentAmount >= requiredAmount)
            {
                IsCompleted = true;
                Debug.Log("Goal complete: Collected all required stones");

                // Se désabonner une fois terminé
                QuestEvents.OnPlanetStoneCollected -= OnStoneCollected;

                if (IsCompleted)
                {
                    //QuestEvents.OnQuestCompleted.Invoke();
                    // nottify that the goal is completed
                    return;
                }
            }

        }
    }
    public override string GetProgression()
    {
        return Progression;
    }
    public override void ChangeProgression()
    {
        Progression = "Matériaux trouvés : " + currentAmount.ToString() + "/" + requiredAmount.ToString();
    }
}



*/



/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
*/

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
        progression = $"Matériaux trouvés : 0/{requiredAmount}";

        // S'abonner à l’événement de collecte
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
                Debug.Log("Objectif terminé !");

                // Se désabonner
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
        progression = $"Matériaux trouvés : {currentAmount}/{requiredAmount}";
    }
}
