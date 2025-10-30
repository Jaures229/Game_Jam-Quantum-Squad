using UnityEngine;
using System;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class DialogueUIManager : MonoBehaviour, IPointerDownHandler, IPointerClickHandler
{
    // Événement statique pour signaler une interaction UI générale (clic/touche)
    // Utile pour faire avancer les dialogues qui attendent une entrée utilisateur.
    public static event Action OnScreenTouched;

    // Cette interface IPointerClickHandler permet de détecter les clics/touches sur le GameObject.
    // Vous devez attacher ce script à un GameObject UI transparent (ex: un Panel) qui couvre tout l'écran,
    // et il doit avoir un composant Canvas Group avec 'Blocks Raycasts' activé.

    public void OnPointerClick(PointerEventData eventData)
    {
        // // Déclenche l'événement global
        // OnScreenTouched?.Invoke();
        // Debug.Log("Event UI: OnScreenTouched déclenché.");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnScreenTouched?.Invoke();
        Debug.Log("Event UI: OnScreenTouched déclenché.");
    }
}
