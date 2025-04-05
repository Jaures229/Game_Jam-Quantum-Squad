// using UnityEngine;
// using UnityEngine.EventSystems;
// using System.Collections.Generic;
// using UnityEngine.UI;
// using TMPro;
// using System.Collections;

// public class DragAndDropUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
// {
//     private RectTransform rectTransform;
//     private Vector3 startPosition;
//     private GameObject currentTarget = null;

//     [Header("Configuration")]
//     [SerializeField] private string[] allowedTags;
//     [SerializeField] private string planetName = "Planète"; // Exemple : "Mars"
//     [TextArea(2, 4)]
//     [SerializeField] private string successMessage = "Bonne réponse, Mars est la quatrième planète du système solaire.";

//     [Header("Notification UI")]
//     [SerializeField] private GameObject notificationPanel;
//     [SerializeField] private TextMeshProUGUI notificationText;

//     [Header("Notification Colors")]
//     [SerializeField] private Color successColor = Color.green;
//     [SerializeField] private Color failureColor = Color.red;

//     void Awake()
//     {
//         rectTransform = GetComponent<RectTransform>();
//     }

//     public void OnBeginDrag(PointerEventData eventData)
//     {
//         startPosition = rectTransform.position;
//     }

//     public void OnDrag(PointerEventData eventData)
//     {
//         rectTransform.position = Input.touchCount > 0
//             ? (Vector3)Input.GetTouch(0).position
//             : eventData.position;
//     }

//     public void OnEndDrag(PointerEventData eventData)
//     {
//         PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
//         pointerEventData.position = Input.touchCount > 0
//             ? (Vector3)Input.GetTouch(0).position
//             : (Vector3)Input.mousePosition;

//         currentTarget = null;

//         foreach (RaycastResult hit in GetUIRaycastResults(pointerEventData))
//         {
//             foreach (string tag in allowedTags)
//             {
//                 if (hit.gameObject.CompareTag(tag))
//                 {
//                     currentTarget = hit.gameObject;
//                     break;
//                 }
//             }
//             if (currentTarget != null) break;
//         }

//         if (currentTarget != null)
//         {
//             RectTransform targetRect = currentTarget.GetComponent<RectTransform>();
//             Image targetImage = currentTarget.GetComponent<Image>();

//             if (targetRect != null)
//             {
//                 rectTransform.position = targetRect.position;
//                 rectTransform.localScale = targetRect.localScale;

//                 if (targetImage != null)
//                     targetImage.color = Color.white;

//                 ShowNotification(successMessage, successColor);
//                 StartCoroutine(HandleSuccessPlacement());
//             }
//         }
//         else
//         {
//             rectTransform.position = startPosition;
//             ShowNotification($"{planetName} n'est pas placée ici.", failureColor);
//         }
//     }

//     private List<RaycastResult> GetUIRaycastResults(PointerEventData eventData)
//     {
//         List<RaycastResult> results = new List<RaycastResult>();
//         EventSystem.current.RaycastAll(eventData, results);
//         return results;
//     }

//     private void ShowNotification(string message, Color textColor)
//     {
//         if (notificationPanel != null && notificationText != null)
//         {
//             notificationText.text = message;
//             notificationText.color = textColor;
//             notificationPanel.SetActive(true);
//             StopAllCoroutines();
//             StartCoroutine(HideNotificationAfterDelay(3f)); // délai ajustable ici
//         }
//     }

//     private IEnumerator HideNotificationAfterDelay(float delay)
//     {
//         yield return new WaitForSeconds(delay);
//         notificationPanel.SetActive(false);
//     }

//     private IEnumerator HandleSuccessPlacement()
//     {
//         DragAndDropManager.Instance.ObjectPlaced();
//         yield return new WaitForSeconds(3f); // le même délai que HideNotificationAfterDelay
//         gameObject.SetActive(false);
//     }
// }



using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class DragAndDropUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private Vector3 startPosition;
    private GameObject currentTarget = null;

    [Header("Configuration")]
    [SerializeField] private string[] allowedTags;
    [SerializeField] private string planetName = "Planète";
    [TextArea(2, 4)]
    [SerializeField] private string successMessage = "Bonne réponse, Mars est la quatrième planète du système solaire.";

    [Header("Notification UI")]
    [SerializeField] private GameObject notificationPanel;
    [SerializeField] private TextMeshProUGUI notificationText;

    [Header("Notification Colors")]
    [SerializeField] private Color successColor = Color.green;
    [SerializeField] private Color failureColor = Color.red;

    [Header("Audio")]
    [SerializeField] private AudioClip successSound;
    [SerializeField] private AudioClip failureSound;
    [SerializeField] private AudioSource audioSource;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        startPosition = rectTransform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.position = Input.touchCount > 0
            ? (Vector3)Input.GetTouch(0).position
            : eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.touchCount > 0
            ? (Vector3)Input.GetTouch(0).position
            : (Vector3)Input.mousePosition;

        currentTarget = null;

        foreach (RaycastResult hit in GetUIRaycastResults(pointerEventData))
        {
            foreach (string tag in allowedTags)
            {
                if (hit.gameObject.CompareTag(tag))
                {
                    currentTarget = hit.gameObject;
                    break;
                }
            }
            if (currentTarget != null) break;
        }

        if (currentTarget != null)
        {
            RectTransform targetRect = currentTarget.GetComponent<RectTransform>();
            Image targetImage = currentTarget.GetComponent<Image>();

            if (targetRect != null)
            {
                rectTransform.position = targetRect.position;
                rectTransform.localScale = targetRect.localScale;

                if (targetImage != null)
                    targetImage.color = Color.white;

                ShowNotification(successMessage, successColor);
                PlaySound(successSound);
                StartCoroutine(HandleSuccessPlacement());
            }
        }
        else
        {
            rectTransform.position = startPosition;
            ShowNotification($"{planetName} n'est pas placée ici.", failureColor);
            PlaySound(failureSound);
        }
    }

    private List<RaycastResult> GetUIRaycastResults(PointerEventData eventData)
    {
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        return results;
    }

    private void ShowNotification(string message, Color textColor)
    {
        if (notificationPanel != null && notificationText != null)
        {
            notificationText.text = message;
            notificationText.color = textColor;
            notificationPanel.SetActive(true);
            StopAllCoroutines();
            StartCoroutine(HideNotificationAfterDelay(2f));
        }
    }

    private IEnumerator HideNotificationAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        notificationPanel.SetActive(false);
    }

    private IEnumerator HandleSuccessPlacement()
    {
        DragAndDropManager.Instance.ObjectPlaced();
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }

    private void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.Stop();
            audioSource.clip = clip;
            audioSource.Play();
        }
    }
}
