using UnityEngine;

[CreateAssetMenu(fileName = "NewVisitPlanetObjective", menuName = "Quests/Objectives/Visit Planet")]
public class VisitPlanetObjective : QuestObjective
{
    [Header("Spécifique à la Visite")]
    [Tooltip("L'identifiant unique (nom ou Tag) de la planète/zone à visiter. Doit correspondre au Notifier.")]
    public string targetPlanetIdentifier = "Mars";

    // Propriété de progression interne (on n'a pas besoin de la sérialiser car isCompleted fait le suivi)
    private bool _hasBeenVisited = false;

    // --- Implémentation des Méthodes Abstraites ---
    
    public override void Initialize()
    {
        // Appelé quand la quête est acceptée.
        ResetObjective();
    }

    // La méthode IsCompleted() est gérée par la classe de base, 
    // qui retourne la valeur de 'isCompleted'.

    public override void ResetObjective()
    {
       // base.ResetObjective(); // Réinitialise isCompleted à false
        _hasBeenVisited = false;
    }

    public override string GetProgressText()
    {
        string progress = $"Rend toi sur la planéte : {targetPlanetIdentifier} en suivant la flèche.";
        return progress;
        //return _hasBeenVisited ? $"✔️ {targetPlanetIdentifier} (Visité)" : $"❌ Se rendre sur {targetPlanetIdentifier}";
    }
    
    public override bool IsCompleted()
    {
        return _hasBeenVisited;
    }
    // --- Logique Spécifique à la Visite ---

    /// <summary>
    /// Appelé par un trigger dans la scène lorsque le joueur entre dans la zone cible.
    /// </summary>
    public void PlanetEntered(string planetIdentifier)
    {
        // Si l'objectif est déjà terminé, on ne fait rien.
        if (_hasBeenVisited) return;

        // Vérifie si l'identifiant de la planète correspond à l'objectif
        if (planetIdentifier == targetPlanetIdentifier)
        {
            _hasBeenVisited = true;
            
            Debug.Log($"Objectif de visite atteint pour : {targetPlanetIdentifier}.");
            
            // Note: Le script 'PlanetTriggerNotifier' devra appeler 
            // QuestManager.Instance.UpdateObjectiveProgress(...) après cet appel.
        }
    }
}