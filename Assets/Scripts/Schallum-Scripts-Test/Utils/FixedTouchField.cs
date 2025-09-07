using UnityEngine;
using UnityEngine.EventSystems;
public class FixedTouchField : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [HideInInspector] public Vector2 touchDirection; // Direction "stick"
    [HideInInspector] public bool pressed;

    private int pointerId;
    private Vector2 startPos;

    [Header("Sensibilité")]
    public float sensitivityX = 1f;
    public float sensitivityY = 1f;

    public void OnPointerDown(PointerEventData eventData)
    {
        pressed = true;
        pointerId = eventData.pointerId;
        startPos = eventData.position; // point d’origine
        touchDirection = Vector2.zero;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (pressed && eventData.pointerId == pointerId)
        {
            Vector2 currentPos = eventData.position;
            Vector2 rawDir = currentPos - startPos;

            // Appliquer sensibilité séparée X/Y
            touchDirection = new Vector2(rawDir.x * sensitivityX, rawDir.y * sensitivityY);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.pointerId == pointerId)
        {
            pressed = false;
            touchDirection = Vector2.zero; // reset quand on lâche
        }
    }
}
