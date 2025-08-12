using UnityEngine;

public class QuestGiverInteraction : MonoBehaviour
{
    private QuestGiver questGiver;
    private bool playerInRange = false;

    void Start()
    {
        questGiver = GetComponent<QuestGiver>();
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            questGiver.Interact();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            Debug.Log("Appuyez sur 'E' pour interagir avec le PNJ.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            Debug.Log("Le joueur est sorti de la zone d'interaction.");
        }
    }
}
