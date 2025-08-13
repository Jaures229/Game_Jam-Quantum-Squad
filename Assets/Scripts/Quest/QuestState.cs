using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] // Permet d'afficher dans l'inspecteur si besoin et de sérialiser pour la sauvegarde
public class QuestState
{
    public Quest questData; // Référence au ScriptableObject Quest immuable
    public bool isLocked; // L'état de verrouillage spécifique au joueur
    public bool isActive; // Est-ce une quête active du joueur ?
    public bool isCompleted; // Est-ce une quête complétée du joueur ?
    // Ajoutez d'autres états spécifiques au joueur ici, ex:
    // public List<ObjectiveProgress> objectiveStates; // Si les objectifs ont aussi des états complexes

    public QuestState(Quest data)
    {
        questData = data;
        isLocked = data.isLocked; // Initialise avec l'état par défaut défini dans le ScriptableObject
        isActive = false;
        isCompleted = false;
        // Initialiser les objectifs ici si vous avez des états complexes pour eux
    }

    // Méthodes d'aide
    public bool AreObjectivesCompleted()
    {
        return questData.IsCompleted(); // Utilise la logique IsCompleted du ScriptableObject
    }

    public void SetLockedState(bool locked)
    {
        isLocked = locked;
    }

    public void SetActiveState(bool active)
    {
        isActive = active;
    }

    public void SetCompletedState(bool completed)
    {
        isCompleted = completed;
    }
}