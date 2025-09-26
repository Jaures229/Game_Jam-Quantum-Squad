using UnityEngine;
using UnityEngine.UI;

public class ElevetorFloor : MonoBehaviour
{
    [Header("Ascenseur")]
    [SerializeField] private Transform elevator;        // L’objet qui bouge
    [SerializeField] private Transform topPoint;        // Position en haut
    [SerializeField] private Transform bottomPoint;     // Position en bas
    [SerializeField] private float speed = 2f;

    [Header("UI")]
    [SerializeField] private GameObject interactUI;     // Bouton UI (Canvas → Button)

    private bool isMoving = false;
    private bool goingUp = true;
    private bool playerNearby = false;

    void Start()
    {
        if (interactUI != null) interactUI.SetActive(false);
    }

    void Update()
    {

        // Déplacement de l’ascenseur
        if (isMoving)
        {
            Transform target = goingUp ? topPoint : bottomPoint;
            elevator.position = Vector3.MoveTowards(elevator.position, target.position, speed * Time.deltaTime);

            if (Vector3.Distance(elevator.position, target.position) < 0.01f)
            {
                isMoving = false; // Arrêt une fois arrivé
            }
        }
    }

    public void ToggleElevator()
    {
        if (!isMoving)
        {
            goingUp = !goingUp;
            isMoving = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
            if (interactUI != null) interactUI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
            if (interactUI != null) interactUI.SetActive(false);
        }
    }
}
