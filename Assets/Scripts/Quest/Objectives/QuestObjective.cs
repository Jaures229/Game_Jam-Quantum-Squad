using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class QuestObjective : ScriptableObject
{
    [Header("Informations de Base")]
    public string objectiveDescription = "Nouvel objectif";
    public bool isOptional = false;

    [Tooltip("ID unique pour identifier cet objectif dans le code.")]
    public string objective_id;

    /// <summary>
    /// Appelé lors de l'acceptation de la quête. Utilisé pour initialiser les valeurs de progression.
    /// </summary>
    public abstract void Initialize();


    /// <summary>
    /// **Vérifie si l'objectif est terminé.** (La logique est définie par chaque classe enfant)
    /// </summary>
    /// <returns>Vrai si l'objectif est complété.</returns>
    public abstract bool IsCompleted();

    /// <summary>
    /// Réinitialise la progression de l'objectif.
    /// </summary>
    public abstract void ResetObjective();

    /// <summary>
    /// Retourne le texte de progression à afficher dans l'UI (ex: "3/5").
    /// </summary>
    public abstract string GetProgressText();
}
