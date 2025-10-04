using UnityEngine;
using UnityEngine.EventSystems; // NÉCESSAIRE pour les interfaces de drag

public class UIDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Canvas canvas;
    private Vector2 originalPosition; // Pour revenir en cas d'échec
    

    private Vector2 originalAnchoredPosition; // La position (Vector2)
    private Transform originalParent;         // La référence de l'objet parent (Transform)
    // Identifiant unique pour le socle correspondant
    public string planetID = "Earth"; 

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GetComponentInParent<Canvas>();
        if (canvasGroup == null)
        {
            // Ajoute un CanvasGroup pour désactiver le Raycast bloquant pendant le drag
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
    }

    // 1. Début du Drag
    public void OnBeginDrag(PointerEventData eventData)
    {
        originalAnchoredPosition = rectTransform.anchoredPosition;
        originalParent = transform.parent;
        // Rend l'image semi-transparente et désactive le raycast
        // pour qu'il ne bloque pas la détection du socle en dessous.
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
        
        // Optionnel : s'assurer qu'elle est affichée au-dessus des autres éléments
        transform.SetAsLastSibling(); 
    }

    // 2. Pendant le Drag
    public void OnDrag(PointerEventData eventData)
    {
        // // Déplace l'élément UI en utilisant la delta de l'événement Pointer
        // // Le mode Screen Space Overlay (par défaut) utilise eventData.delta
        // rectTransform.anchoredPosition += eventData.delta / transform.localScale.x;

        // VÉRIFIEZ ET CORRIGEZ ICI !
        if (canvas != null)
        {
            // La correction la plus précise : Diviser le mouvement par le facteur d'échelle du Canvas.
            float scaleFactor = canvas.scaleFactor;
            rectTransform.anchoredPosition += eventData.delta / scaleFactor;
        }
        else
        {
            // Si le Canvas n'est pas trouvé, revenir à la solution précédente (moins précise)
            rectTransform.anchoredPosition += eventData.delta / transform.localScale.x;
        }
    }

    // 3. Fin du Drag (Relâchement)
    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        
        // Si l'objet n'a pas été fixé par le script de socle (voir ci-dessous),
        // il reviendra à sa position initiale.
        if (transform.parent == originalParent) 
        {
            // On retourne à la position mémorisée.
            rectTransform.anchoredPosition = originalAnchoredPosition;
        }
    }
}