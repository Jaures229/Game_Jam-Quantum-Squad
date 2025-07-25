using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class QuestObjective : ScriptableObject
{
    public string objectiveDescription = "Nouvel objectif";
    public bool isOptional = false;

    public abstract void Initialize();
    public abstract bool IsCompleted();
    public abstract void ResetObjective();
    public abstract string GetProgressText();
}
