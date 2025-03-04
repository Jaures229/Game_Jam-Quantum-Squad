using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class DragAndDropUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private Vector3 startPosition;
    private Canvas canvas;
    private GameObject currentTarget = null; 

    [SerializeField] private string[] allowedTags; 
    //private bool isPlaced = false;

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
                        rectTransform.position = targetRect.position; 
                        rectTransform.localScale = targetRect.localScale; 
                        currentTarget = hit.gameObject; 
                    }

                    foundTarget = true;
                    break;
                }
            }
        }

        if (!foundTarget)
        {
           
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
            currentTarget = null; 
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (currentTarget != null)
        {
            RectTransform targetRect = currentTarget.GetComponent<RectTransform>();
            Image targetImage = currentTarget.GetComponent<Image>();
            PlanetController TargetplanetController = currentTarget.GetComponent<PlanetController>();
 
            if (targetRect != null)
            {
                targetImage.color = new Color(255, 255, 255);
                TargetplanetController.activated = true;
                TargetplanetController.planetTextGameobject.SetActive(true);
            }
            gameObject.SetActive(false);
            DragAndDropManager.Instance.ObjectPlaced();
        }
        else
        {
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
