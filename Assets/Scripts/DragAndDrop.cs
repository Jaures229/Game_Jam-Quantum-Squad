// using UnityEngine;
// using UnityEngine.EventSystems;
// using System.Collections.Generic;

// public class DragAndDropUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
// {
//     private RectTransform rectTransform;
//     private Vector3 startPosition;
//     private Canvas canvas;

//     [SerializeField] private string[] allowedTags; // Tags valides
//     private bool isPlaced = false; // Vérifie si l'objet est bien placé

//     void Awake()
//     {
//         rectTransform = GetComponent<RectTransform>();
//         canvas = GetComponentInParent<Canvas>();
//     }

//     public void OnBeginDrag(PointerEventData eventData)
//     {
//         startPosition = rectTransform.position;
//     }

//     public void OnDrag(PointerEventData eventData)
//     {
//         if (Input.touchCount > 0) // Mode mobile
//         {
//             Touch touch = Input.GetTouch(0);
//             if (touch.phase == TouchPhase.Moved)
//             {
//                 rectTransform.position = touch.position;
//             }
//         }
//         else // Mode souris
//         {
//             rectTransform.position = eventData.position;
//         }
//     }
//     public void OnEndDrag(PointerEventData eventData)
//     {
//         PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
//         pointerEventData.position = Input.touchCount > 0 ? (Vector3)Input.GetTouch(0).position : (Vector3)Input.mousePosition;

//         bool correctlyPlaced = false;

//         foreach (RaycastResult hit in GetUIRaycastResults(pointerEventData))
//         {
//             foreach (string tag in allowedTags)
//             {
//                 if (hit.gameObject.CompareTag(tag))
//                 {
//                     RectTransform targetRect = hit.gameObject.GetComponent<RectTransform>();

//                     if (targetRect != null)
//                     {
//                         rectTransform.position = targetRect.position; // Snap à la position cible
//                         rectTransform.localScale = targetRect.localScale; // Ajuster l'échelle
//                         rectTransform.sizeDelta = targetRect.sizeDelta; // Ajuster la taille
//                     }

//                     correctlyPlaced = true;
//                     break;
//                 }
//             }
//         }

//         if (correctlyPlaced)
//         {
//             if (!isPlaced) // Si ce n'était pas déjà placé
//             {
//                 isPlaced = true;
//                 DragAndDropManager.Instance.ObjectPlaced(); // Notifie le gestionnaire
//             }
//         }
//         else
//         {
//             if (isPlaced) // Si l'objet était bien placé mais ne l'est plus
//             {
//                 isPlaced = false;
//                 DragAndDropManager.Instance.ObjectRemoved(); // Notifie le gestionnaire
//             }
//             rectTransform.position = startPosition;
//         }
//     }


//     private List<RaycastResult> GetUIRaycastResults(PointerEventData eventData)
//     {
//         List<RaycastResult> results = new List<RaycastResult>();
//         EventSystem.current.RaycastAll(eventData, results);
//         return results;
//     }
// }



using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class DragAndDropUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private Vector3 startPosition;
    private Canvas canvas;
    private GameObject currentTarget = null; // Stocke la cible actuelle

    [SerializeField] private string[] allowedTags; // Tags valides
    private bool isPlaced = false; // Vérifie si l'objet est bien placé

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        startPosition = rectTransform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.touchCount > 0 ? (Vector3)Input.GetTouch(0).position : (Vector3)Input.mousePosition;

        bool foundTarget = false;

        foreach (RaycastResult hit in GetUIRaycastResults(pointerEventData))
        {
            foreach (string tag in allowedTags)
            {
                if (hit.gameObject.CompareTag(tag))
                {
                    RectTransform targetRect = hit.gameObject.GetComponent<RectTransform>();

                    if (targetRect != null)
                    {
                        rectTransform.position = targetRect.position; // Ajuste la position
                        rectTransform.localScale = targetRect.localScale; // Ajuste l'échelle
                        currentTarget = hit.gameObject; // Stocke la cible
                    }

                    foundTarget = true;
                    break;
                }
            }
        }

        if (!foundTarget)
        {
            // Déplace normalement si aucune cible valide n'est trouvée
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Moved)
                {
                    rectTransform.position = touch.position;
                }
            }
            else
            {
                rectTransform.position = eventData.position;
            }
            currentTarget = null; // Aucune cible valide détectée
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (currentTarget != null)
        {
            RectTransform targetRect = currentTarget.GetComponent<RectTransform>();

            if (targetRect != null)
            {
                rectTransform.position = targetRect.position; // Snap à la position finale
                rectTransform.localScale = targetRect.localScale; // Ajuste l'échelle
                rectTransform.sizeDelta = targetRect.sizeDelta; // Ajuste la taille
            }

            // Fait disparaître la cible
            currentTarget.SetActive(false);
            currentTarget = null;
            isPlaced = true;
            DragAndDropManager.Instance.ObjectPlaced();
        }
        else
        {
            // Revient à la position initiale si aucune cible valide
            rectTransform.position = startPosition;
        }
    }

    private List<RaycastResult> GetUIRaycastResults(PointerEventData eventData)
    {
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        return results;
    }
}
