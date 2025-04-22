using UnityEngine;
using UnityEngine.EventSystems;

public class BoostButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        PlayerControler.instance.StartBoost();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        PlayerControler.instance.StopBoost();
    }
}
