using UnityEngine;

[CreateAssetMenu(fileName = "NewSolvePuzzleObjective", menuName = "Quests/Objectives/Solve Puzzle")]
public class SolvePuzzleObjective : QuestObjective
{
    [Header("Spécifique au Puzzle")]
    [Tooltip("L'identifiant unique (ID) du puzzle que le joueur doit résoudre (ex: 'AncientRunes', 'LaserGrid_01').")]
    public string targetPuzzleID = "AncientRunes";

    // --- Progression gérée par l'enfant ---
    [HideInInspector]
    [SerializeField]
    private bool _hasBeenSolved = false;
    // ---------------------------------------

    // --- Implémentation des Méthodes Abstraites ---

    public override void Initialize()
    {
        // Appelé quand la quête est acceptée.
        ResetObjective();
    }

    public override bool IsCompleted()
    {
        // La complétion est basée sur l'état interne.
        return _hasBeenSolved;
    }

    public override void ResetObjective()
    {
        // Réinitialisation si le joueur abandonne ou si le puzzle est réinitialisé.
        _hasBeenSolved = false;
    }

    public override string GetProgressText()
    {
        return _hasBeenSolved
            ? $"✔️ Puzzle '{targetPuzzleID}' résolu"
            : $"❌ Résoudre le puzzle : {targetPuzzleID}";
    }

    // --- Logique Spécifique à la Résolution ---

    /// <summary>
    /// Appelé par le script du puzzle dans la scène lorsque sa condition de victoire est remplie.
    /// </summary>
    /// <param name="puzzleID">L'identifiant du puzzle qui a été résolu.</param>
    public void PuzzleSolved(string puzzleID)
    {
        // Si l'objectif est déjà terminé, on ne fait rien.
        if (IsCompleted()) return;

        // Vérifie si l'identifiant du puzzle correspond à l'objectif
        if (puzzleID == targetPuzzleID)
        {
            _hasBeenSolved = true;
            Debug.Log($"Objectif de résolution de puzzle atteint pour : {targetPuzzleID}.");

            // Le script de la scène doit appeler QuestManager.Instance.UpdateObjectiveProgress(...)
        }
    }
}
