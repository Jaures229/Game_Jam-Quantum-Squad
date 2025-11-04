using UnityEngine;
using UnityEngine.UI; // Nécessaire pour les éléments UI (facultatif si vous utilisez juste RectTransform)
using DG.Tweening; // IMPORTANT : Ajoutez l'espace de nom DOTween


public class CrossMouvement : MonoBehaviour
{

    // L'élément UI à déplacer
    private RectTransform uiElement;

    // Définitions des points de départ et d'arrivée (en coordonnées Anchor/Ancrage)
    private Vector2 positionInitiale;
    [Header("Paramètres de Mouvement")]
    [Tooltip("Décalage Vertical (Haut/Bas)")]
    public float decalageVertical = 150f;
    [Tooltip("Décalage Horizontal (Gauche/Droite)")]
    public float decalageHorizontal = 200f;
    [Tooltip("Durée de chaque étape de l'animation")]
    public float dureeEtape = 0.5f;
    [Tooltip("Répéter l'animation en boucle (YoYo = aller-retour)")]
    public LoopType typeBoucle = LoopType.Yoyo;
    [Tooltip("Nombre de répétitions (-1 pour infini)")]
    public int nombreBoucles = -1;

    void Start()
    {
        // On récupère le RectTransform de l'élément (indispensable pour les UI)
        uiElement = GetComponent<RectTransform>();

        if (uiElement == null)
        {
            Debug.LogError("Le script MouvementEnCroixUI doit être attaché à un élément UI (RectTransform).");
            return;
        }

        // On enregistre la position de départ (Anchor Position)
        positionInitiale = uiElement.anchoredPosition;

        // On lance la séquence de mouvement
        LancerMouvementEnCroix();
    }

    private void LancerMouvementEnCroix()
    {
        // 1. Créer une nouvelle séquence DOTween
        Sequence sequence = DOTween.Sequence();

        // --- Définition des positions cibles ---
        // A. Position HAUT (Départ: positionInitiale)
        Vector2 posHaut = positionInitiale + new Vector2(0, decalageVertical);
        // B. Position BAS
        Vector2 posBas = positionInitiale - new Vector2(0, decalageVertical);
        // C. Position GAUCHE (Retour au centre vertical, Décalage Gauche)
        Vector2 posGauche = positionInitiale - new Vector2(decalageHorizontal, 0);
        // D. Position DROITE
        Vector2 posDroite = positionInitiale + new Vector2(decalageHorizontal, 0);

        // --- Construction de la séquence (Haut -> Bas -> Gauche -> Droite) ---

        // 1. Aller vers le HAUT (à partir de la position initiale)
        sequence.Append(uiElement.DOAnchorPos(posHaut, dureeEtape).SetEase(Ease.OutQuad));

        // 2. Aller vers le BAS
        sequence.Append(uiElement.DOAnchorPos(posBas, dureeEtape * 2).SetEase(Ease.InOutSine)); // Double durée pour traverser

        // 3. Aller vers la GAUCHE (à partir de la position BAS, remonte au centre vertical en se décalant horizontalement)
        // Note: Ici, on pourrait vouloir que le mouvement soit en diagonale du Bas vers la Gauche, 
        // ou qu'il revienne d'abord au centre. Pour un "mouvement en croix" pur :

        // Pour une croix pure (verticale puis horizontale au centre) :

        // 3a. Retour au CENTRE vertical (Append va s'exécuter après l'étape 2)
        sequence.Append(uiElement.DOAnchorPos(positionInitiale, dureeEtape).SetEase(Ease.OutQuad));

        // 4. Aller vers la GAUCHE (à partir du centre)
        sequence.Append(uiElement.DOAnchorPos(posGauche, dureeEtape).SetEase(Ease.OutQuad));

        // 5. Aller vers la DROITE (à partir de la position GAUCHE)
        sequence.Append(uiElement.DOAnchorPos(posDroite, dureeEtape * 2).SetEase(Ease.InOutSine)); // Double durée pour traverser

        // 6. Retour à la position INITIALE (à partir de la position DROITE)
        sequence.Append(uiElement.DOAnchorPos(positionInitiale, dureeEtape).SetEase(Ease.OutQuad));


        // --- Configuration de la séquence ---

        // Configuration de la boucle
        sequence.SetLoops(nombreBoucles, typeBoucle);

        // Démarrer la séquence
        sequence.Play();

        // Pour s'assurer que l'animation s'arrête si l'objet est détruit
        sequence.SetAutoKill(true);
        sequence.SetTarget(gameObject);
    }
}
