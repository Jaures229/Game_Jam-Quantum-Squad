using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class UIDropChecker : MonoBehaviour, IDropHandler
{

    [Tooltip("Référence au Puzzle Manager de la scène")]
    public PuzzleManager puzzleManager; 

    // Doit correspondre à l'ID de la planète, ex: "Earth"
    public string requiredID = "Earth";
    public float fadeDuration = 1.0f; // Durée de l'animation de disparition en secondes
    // Appelé quand un élément (planète) qui implémente IDragHandler est relâché sur ce socle
    public void OnDrop(PointerEventData eventData)
    {
        // 1. Tente d'obtenir le script de Drag de l'objet relâché
        UIDragHandler droppedPlanet = eventData.pointerDrag.GetComponent<UIDragHandler>();

        if (droppedPlanet != null)
        {
            // 2. Vérifie si l'ID de la planète correspond à l'ID requis
            if (droppedPlanet.planetID == requiredID)
            {
                // **SUCCESS!**

                // 3. Fixe la planète au centre du socle
                RectTransform planetRect = droppedPlanet.GetComponent<RectTransform>();
                planetRect.SetParent(transform, false); // Optionnel: rend le socle parent de la planète
                planetRect.anchorMin = new Vector2(0.5f, 0.5f);
                planetRect.anchorMax = new Vector2(0.5f, 0.5f);

                planetRect.pivot = new Vector2(0.5f, 0.5f);
                // 4. Désactive la possibilité de la glisser à nouveau
                planetRect.anchoredPosition = Vector2.zero;// Centre l'enfant dans le parent
                droppedPlanet.enabled = false;


                TextMeshProUGUI planetText = droppedPlanet.GetComponentInChildren<TextMeshProUGUI>();
                if (planetText != null)
                {
                    // DÉCLENCHE LA COROUTINE POUR FAIRE DISPARAÎTRE LE TEXTE
                    StartCoroutine(FadeOutTMP(planetText, fadeDuration));
                }

                Image socketImage = this.GetComponent<Image>();
                if (socketImage != null)
                {
                    socketImage.enabled = false;
                }
                Debug.Log("La planète " + droppedPlanet.planetID + " a été placée.");
                // >>> LA LOGIQUE DE COMPTAGE DU PUZZLE <<<
                // NOTIFIE LE MANAGER via la référence assignée
                if (puzzleManager != null)
                {
                    puzzleManager.PiecePlacedSuccessfully();
                } else {
                    // Message de sécurité si vous avez oublié d'assigner le manager
                    Debug.LogError("Le Puzzle Manager n'a pas été assigné au socle " + gameObject.name + "!");
                }
            }
            else
            {
                // Échec : L'objet OnEndDrag du script UIDragHandler va le faire revenir
                Debug.Log("Mauvaise planète. Remise à la position initiale.");
            }
        }
    }
    // NOUVELLE COROUTINE pour faire disparaître le TextMeshPro
    private System.Collections.IEnumerator FadeOutTMP(TextMeshProUGUI textMesh, float duration)
    {
        Color startColor = textMesh.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0f); // Transparence totale
        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            // Interpolation linéaire entre la couleur de départ et la couleur finale (transparente)
            textMesh.color = Color.Lerp(startColor, endColor, timer / duration);
            yield return null; // Attend le prochain frame
        }

        textMesh.color = endColor; // S'assure que la couleur est bien finale
        textMesh.gameObject.SetActive(false); // Optionnel : désactive le texte une fois invisible
    }
}
