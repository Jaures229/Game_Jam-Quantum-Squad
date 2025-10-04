using UnityEngine;
using System.Collections;

public class PuzzleManager : MonoBehaviour
{
    // ----------------------------------------------------
    // Singleton pour un accès facile
    // ----------------------------------------------------

    // ----------------------------------------------------
    // Paramètres du Puzzle
    // ----------------------------------------------------
    [Tooltip("Nombre total de socles dans le puzzle")]
    public int totalPieces = 3;

    // Suivi de la progression
    private int piecesPlaced = 0;
    public bool finish_puzzle = false;
    // ----------------------------------------------------
    // Audio (Hypothèse : Vous utilisez un AudioSource)
    // ----------------------------------------------------
    [Tooltip("AudioSource pour jouer la musique du puzzle")]
    public AudioSource puzzleMusicSource;
    [Tooltip("Clip musical à jouer pendant le puzzle")]
    public AudioClip puzzleMusic;

    [Tooltip("Le Canvas Group attaché au Panel à faire disparaître.")]
    public CanvasGroup puzzlePanelCanvasGroup;

    [Tooltip("Durée de l'animation de fondu (en secondes)")]
    public float fadeOutDuration = 1.0f;

    [Tooltip("Clip à jouer lorsque le puzzle est terminé")]
    public AudioClip successMusic;

    public void StartPlayMusic()
    {
        // Démarre la musique dès le début du jeu
        if (puzzleMusicSource != null && puzzleMusic != null)
        {
            puzzleMusicSource.clip = puzzleMusic;
            puzzleMusicSource.loop = true; // La musique tourne en boucle
            puzzleMusicSource.Play();
        }
    }

    /// <summary>
    /// Appelé par chaque socle lorsque la bonne pièce y est placée.
    /// </summary>
    public void PiecePlacedSuccessfully()
    {
        piecesPlaced++;
        Debug.Log($"Pièce placée. Progression : {piecesPlaced}/{totalPieces}");

        if (piecesPlaced >= totalPieces)
        {
            EndPuzzle();
        }
    }

    /// <summary>
    /// Logique de fin de puzzle (succès).
    /// </summary>
    private void EndPuzzle()
    {
        Debug.Log("Félicitations, le puzzle est terminé !");

        // 1. Arrête l'ancienne musique et joue le son de succès
        if (puzzleMusicSource != null)
        {
            puzzleMusicSource.Stop();
            if (successMusic != null)
            {
                // Joue le son de succès une seule fois sans boucler
                puzzleMusicSource.loop = false;
                puzzleMusicSource.PlayOneShot(successMusic);
            }
        }

        // DÉCLENCHE LE FONDU PROGRESSIF DU PANEL
        if (puzzlePanelCanvasGroup != null)
        {
            StartCoroutine(FadeOutPanel(puzzlePanelCanvasGroup, fadeOutDuration));
        }
        // 2. REND INVISIBLE LE PANEL UI
        finish_puzzle = true;
    }
    
    /// <summary>
    /// Coroutine pour animer le fondu de sortie d'un Canvas Group.
    /// </summary>
    private IEnumerator FadeOutPanel(CanvasGroup canvasGroup, float duration)
    {
        float startAlpha = canvasGroup.alpha; // Devrait être 1.0
        float targetAlpha = 0f;
        float timer = 0f;

        // Boucle d'animation
        while (timer < duration)
        {
            timer += Time.deltaTime;
            // Interpolation linéaire entre l'opacité actuelle et 0
            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, timer / duration);
            yield return null; // Attend le prochain frame
        }

        // Assurer une fin propre
        canvasGroup.alpha = targetAlpha;

        // Désactiver le GameObject à la fin du fondu pour stopper les interactions
        canvasGroup.gameObject.SetActive(false);
    }
}
