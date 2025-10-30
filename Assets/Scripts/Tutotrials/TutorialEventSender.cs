using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class TutorialEventSender : MonoBehaviour, IPointerDownHandler, IPointerClickHandler
{
    public string objective_id;
    public bool send_when_clicked = false;

    /// <summary>
    /// Cette fonction est appelée lorsque l'élément UI est cliqué.
    /// </summary>
    ///
    private void SendObjectiveCompleteEvent()
    {
        Debug.Log("sended");
        TutorialEvents.TutorialObjectiveCompleted(objective_id); 
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (send_when_clicked)
        {
            SendObjectiveCompleteEvent();
        }
        //
        // // Déclenche l'événement global
        // OnScreenTouched?.Invoke();
        // Debug.Log("Event UI: OnScreenTouched déclenché.");
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Event UI: OnScreenTouched déclenché.");
        SendObjectiveCompleteEvent();
    }

}