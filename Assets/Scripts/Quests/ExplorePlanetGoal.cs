using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ExplorePlanetGoal", menuName = "Quests/Goals/ExplorePlanet")]

public class ExplorePlanetGoal : QuestGoal
{
    public string planetName;

    public override void Initialize()
    {
        IsCompleted = false;
        QuestEvents.OnPlanetVisited += CheckPlanetVisited;
    }
    private void CheckPlanetVisited(string visitedPlanet)
    {
        if (IsCompleted) return;

        // change progression


        //  Notifie l’UI
        NotifyProgressChanged();

        if (visitedPlanet == planetName)
        {
            IsCompleted = true;
            Debug.Log("Goal complete: Explored " + planetName);

            // On se désabonne quand c’est terminé
            QuestEvents.OnPlanetVisited -= CheckPlanetVisited;
        }
    }

    public override string GetProgression()
    {
        return "";
        //return IsCompleted ? "Exploration terminée" : "Explorer : " + planetName;
    }

    public override void OnEventRaised(params object[] args) { }


    public override void ChangeProgression()
    {

    }
}

