using UnityEngine;
using UnityEngine.UI;

public class DragAndDropManager : MonoBehaviour
{
    public static DragAndDropManager Instance { get; private set; }

    [SerializeField] private GameObject validatePanel; // Le bouton à activer
    private int totalObjects = 8; // Nombre total d'objets à placer
    private int placedObjects = 0; // Nombre d'objets placés

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        //validateButton.interactable = false; // Désactive le bouton au début
    }

    public void ObjectPlaced()
    {
        placedObjects++;
        Debug.Log(placedObjects);
        CheckCompletion();
    }

    public void ObjectRemoved()
    {
        placedObjects--;
        CheckCompletion();
    }

    private void CheckCompletion()
    {
        if (placedObjects == totalObjects) {
            validatePanel.SetActive(true);
            //validateButton.gameObject.SetActive(true);
        }
        //validateButton.interactable = (placedObjects == totalObjects);
    }
}
