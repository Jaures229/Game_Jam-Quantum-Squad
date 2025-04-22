using UnityEngine;
using UnityEngine.EventSystems;

public class MoveButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        PlayerControler.instance.StartMoving();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        PlayerControler.instance.StopMoving();
    }
}
