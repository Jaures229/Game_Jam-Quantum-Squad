using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class DragAndDropUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private Vector3 startPosition;
    private Canvas canvas;

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
        if (Input.touchCount > 0) // Mode mobile
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {
                rectTransform.position = touch.position;
            }
        }
        else // Mode souris
        {
            rectTransform.position = eventData.position;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.touchCount > 0 ? (Vector3)Input.GetTouch(0).position : (Vector3)Input.mousePosition;

        bool correctlyPlaced = false;

        foreach (RaycastResult hit in GetUIRaycastResults(pointerEventData))
        {
            foreach (string tag in allowedTags)
            {
                if (hit.gameObject.CompareTag(tag))
                {
                    rectTransform.position = hit.gameObject.transform.position; // Snap à la position cible
                    correctlyPlaced = true;
                    break;
                }
            }
        }

        if (correctlyPlaced)
        {
            if (!isPlaced) // Si ce n'était pas déjà placé
            {
                isPlaced = true;
                DragAndDropManager.Instance.ObjectPlaced(); // Notifie le gestionnaire
            }
        }
        else
        {
            if (isPlaced) // Si l'objet était bien placé mais ne l'est plus
            {
                isPlaced = false;
                DragAndDropManager.Instance.ObjectRemoved(); // Notifie le gestionnaire
            }
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
