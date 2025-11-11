using UnityEngine;

// Ce script est attaché à CHAQUE objet porte qui doit pivoter.
public class DoorController : MonoBehaviour
{
    // Configuration spécifique à CETTE porte
    public float openAngle = 90f;
    public float rotationSpeed = 100f;
    public Vector3 rotationAxis = Vector3.up;

    private Quaternion startRotation;
    private Quaternion targetRotation;
    private bool isDoorOpen = false;

    void Start()
    {
        // La rotation initiale est celle du Game Object auquel le script est attaché
        startRotation = transform.localRotation;
        targetRotation = startRotation;
    }

    void Update()
    {
        // Rotation progressive vers la cible
        transform.localRotation = Quaternion.RotateTowards(
            transform.localRotation,
            targetRotation,
            rotationSpeed * Time.deltaTime
        );
    }

    // Fonction publique pour ouvrir la porte
    public void Open()
    {
        if (!isDoorOpen)
        {
            // Calcule la rotation d'ouverture
            targetRotation = startRotation * Quaternion.AngleAxis(openAngle, rotationAxis);
            isDoorOpen = true;
        }
    }

    // Fonction publique pour fermer la porte
    public void Close()
    {
        if (isDoorOpen)
        {
            // La cible redevient la rotation initiale (fermée)
            targetRotation = startRotation;
            isDoorOpen = false;
        }
    }
}
