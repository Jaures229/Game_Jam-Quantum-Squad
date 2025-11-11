using UnityEngine;

[CreateAssetMenu(fileName = "NewPlaceItemObjective", menuName = "Quests/Objectives/Place Item")]
public class PlaceItemObjective : QuestObjective
{
    [Header("Spécifique au Placement d'Objet")]
    [Tooltip("L'identifiant (ID ou nom) de l'objet que le joueur doit placer.")]
    public string targetItemID = "Artifact_01";

    [Tooltip("L'identifiant unique (Tag ou nom) du lieu de dépôt (ex: 'Altar_A', 'DropOffPoint').")]
    public string dropOffLocationID = "Altar_A";

    [Header("Progression")]
    [Tooltip("Nombre d'objets requis à placer.")]
    public int requiredAmount = 1;
    private bool isCompleted = false;

    [HideInInspector]
    public int currentAmount = 0; // Le nombre actuel d'objets placés

    // --- Implémentation des Méthodes Abstraites ---

    public override void Initialize()
    {
        ResetObjective();
    }

    public override bool IsCompleted()
    {
        // La complétion est basée sur la comparaison entre les montants
        return currentAmount >= requiredAmount;
    }

    public override void ResetObjective()
    {
        // Réinitialise l'état et le compteur
        currentAmount = 0;
        isCompleted = false; // Rappel: isCompleted dans la base est maintenu par l'appel à IsCompleted()
    }

    public override string GetProgressText()
    {
        // Affichage du progrès dans l'UI
        return $"Placer l'objet '{targetItemID}' à {dropOffLocationID} : ({currentAmount}/{requiredAmount})";
    }

    // --- Logique Spécifique au Placement ---

    /// <summary>
    /// Appelé par le trigger de la zone de dépôt lorsque le joueur interagit 
    /// ou dépose l'objet requis.
    /// </summary>
    /// <param name="locationID">L'identifiant de la zone de dépôt.</param>
    /// <param name="placedItemID">L'identifiant de l'objet qui a été placé.</param>
    public void ItemPlaced(string locationID, string placedItemID)
    {
        // Si l'objectif est déjà terminé, ou si les IDs ne correspondent pas, on arrête
        if (IsCompleted() || isCompleted) return;

        bool locationMatches = (locationID == dropOffLocationID);
        bool itemMatches = (placedItemID == targetItemID);

        if (locationMatches && itemMatches)
        {
            currentAmount++; // Incrémente le compteur

            // Mise à jour de l'état de complétion si le seuil est atteint
            if (currentAmount >= requiredAmount)
            {
                isCompleted = true; // Marque comme terminé
                Debug.Log($"Objectif de placement atteint : Tous les {targetItemID} ont été déposés à {dropOffLocationID}.");
            }
            else
            {
                Debug.Log($"Progression de l'objectif : {currentAmount}/{requiredAmount} {targetItemID} déposés.");
            }

            // Note: Il est crucial que le DropOffZone appelle QuestManager.Instance.UpdateObjectiveProgress(...)
        }
    }
}
