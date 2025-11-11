using UnityEngine;

[CreateAssetMenu(fileName = "NewInteractObjective", menuName = "Quests/Objectives/Interact with Element")]
public class InteractWithElementObjective : QuestObjective
{
    [Header("Spécifique à l'Interaction")]
    public string targetElementID = "MainLever_A";

    // --- Progression gérée par l'enfant ---
    [HideInInspector]
    [SerializeField] // Important: permet la sérialisation pour la persistance des données
    private bool _hasInteracted = false;
    // ---------------------------------------

    public override void Initialize()
    {
        // Initialisation de la progression
        ResetObjective();
    }

    public override bool IsCompleted()
    {
        // La méthode de la base est abstraite, donc nous définissons ici la logique de complétion.
        return _hasInteracted;
    }

    public override void ResetObjective()
    {
        _hasInteracted = false;
        // On n'appelle pas base.ResetObjective() car la méthode de base n'a pas de logique d'état à réinitialiser.
    }

    public override string GetProgressText()
    {
        return _hasInteracted
            ? $"✔️ Interagi avec {targetElementID}"
            : $"❌ Interagir avec l'élément : {targetElementID}";
    }

    // Méthode de notification appelée par l'élément interactif
    public void ElementInteracted(string elementID)
    {
        // On vérifie l'état ici, avant de mettre à jour
        if (IsCompleted()) return;

        if (elementID == targetElementID)
        {
            _hasInteracted = true;
            Debug.Log($"Objectif d'interaction atteint pour : {targetElementID}.");

            // Le QuestManager sera notifié et appellera IsCompleted(), qui retournera true.
        }
    }
}
