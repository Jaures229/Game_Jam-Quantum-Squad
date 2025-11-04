using UnityEngine;
using TMPro; // Pour TextMeshProUGUI (recommandé pour le texte UI moderne)
using DG.Tweening; // IMPORTANT : Ajoutez l'espace de nom DOTween

public class ZoomTexteUI : MonoBehaviour
{
    // Référence au composant de texte
    // Utilisez TextMeshProUGUI si vous utilisez TextMeshPro (recommandé)
    // Sinon, utilisez UnityEngine.UI.Text pour le texte UI hérité
    // Ou si vous utilisez le texte Legacy:
    // public UnityEngine.UI.Text textComponent;

    [Header("Paramètres de Zoom")]
    [Tooltip("L'échelle maximale atteinte pendant le zoom in (ex: 1.2 pour 120%)")]
    public float scaleMax = 1.2f;
    [Tooltip("L'échelle minimale atteinte pendant le zoom out (ex: 0.8 pour 80%)")]
    public float scaleMin = 0.8f;
    [Tooltip("Durée de l'animation de zoom in")]
    public float dureeZoomIn = 0.5f;
    [Tooltip("Durée de l'animation de zoom out")]
    public float dureeZoomOut = 0.5f;
    [Tooltip("Délai avant le début de l'animation")]
    public float delaiInitial = 0f;
    [Tooltip("Type de courbe d'animation pour le zoom in")]
    public Ease easeZoomIn = Ease.OutQuad;
    [Tooltip("Type de courbe d'animation pour le zoom out")]
    public Ease easeZoomOut = Ease.InQuad;
    [Tooltip("Répéter l'animation en boucle (-1 pour infini)")]
    public int nombreBoucles = -1;

    // L'échelle initiale du texte
    private Vector3 echelleInitiale;

    void Start()
    {

        // On enregistre l'échelle initiale du GameObject qui contient le texte
        echelleInitiale = transform.localScale;

        // On lance la séquence de zoom après un délai optionnel
        Invoke("LancerZoom", delaiInitial);
    }

    private void LancerZoom()
    {
        // 1. Créer une nouvelle séquence DOTween
        Sequence sequence = DOTween.Sequence();

        // 2. Ajouter l'étape de zoom IN (de l'échelle initiale à l'échelle max)
        sequence.Append(transform.DOScale(echelleInitiale * scaleMax, dureeZoomIn)
            .SetEase(easeZoomIn));

        // 3. Ajouter l'étape de zoom OUT (de l'échelle max à l'échelle min)
        sequence.Append(transform.DOScale(echelleInitiale * scaleMin, dureeZoomOut)
            .SetEase(easeZoomOut));

        // 4. Ajouter l'étape de retour à l'échelle initiale (de l'échelle min à l'échelle initiale)
        sequence.Append(transform.DOScale(echelleInitiale, dureeZoomIn) // Utilise la même durée que le zoom in pour le retour
            .SetEase(easeZoomIn));


        // --- Configuration de la séquence ---

        // Configuration de la boucle
        // Utilisation de LoopType.Restart pour redémarrer l'animation depuis le début
        sequence.SetLoops(nombreBoucles, LoopType.Restart);

        // Démarrer la séquence
        sequence.Play();

        // Pour s'assurer que l'animation s'arrête si l'objet est détruit
        sequence.SetAutoKill(true);
        sequence.SetTarget(gameObject);
    }

    // Optionnel : Réinitialiser l'échelle et arrêter l'animation si l'objet est désactivé
    void OnDisable()
    {
        // Arrête toutes les animations DOTween sur ce GameObject
        transform.DOKill(true);
        // Réinitialise l'échelle à sa valeur initiale
        transform.localScale = echelleInitiale;
    }
}
