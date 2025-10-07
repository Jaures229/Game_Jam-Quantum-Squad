using UnityEngine;
using UnityEngine.UI;

public class DirectionnalArrow : MonoBehaviour
{
    public Transform joueur;
    public RectTransform flecheUI;

    // La distance en mètres à laquelle la flèche doit disparaître.
    public float distanceDeDetection = 200.0f; // Ajustez cette valeur pour l'espace !

    // La référence à la cible mobile. Elle est null si aucune cible n'est définie.
    private Transform cibleMobile = null;

    void Start()
    {
        if (joueur == null || flecheUI == null)
        {
            Debug.LogError("Le Joueur et la Flèche UI doivent être assignés dans l'Inspector.");
            return;
        }

        // S'abonner à l'événement du GPSManager pour recevoir la nouvelle cible (Transform)
        if (GPSManager.Instance != null)
        {
            GPSManager.Instance.OnTargetSet += HandleNewTarget;
            // Récupérer la cible actuelle au démarrage (si elle existe)
            cibleMobile = GPSManager.Instance.TargetTransform;
            flecheUI.gameObject.SetActive(cibleMobile != null);
        }
    }

    // Méthode appelée lorsque le manager définit une nouvelle cible
    private void HandleNewTarget(Transform newTarget, string newTargetID)
    {
        cibleMobile = newTarget;
        // L'ID n'est pas utilisé ici, mais il est reçu
        // ...
        flecheUI.gameObject.SetActive(cibleMobile != null);
    }

    void OnDestroy()
    {
        // Se désabonner de l'événement
        if (GPSManager.Instance != null)
        {
            GPSManager.Instance.OnTargetSet -= HandleNewTarget;
        }
    }

    void Update()
    {
        // Si cibleMobile est null, cela signifie que la destination est effacée ou non définie.
        if (cibleMobile == null)
        {
            return;
        }

        // *** CALCUL DE LA DESTINATION MOBILE EN TEMPS RÉEL ***

        // La position de la cible est lue à chaque frame, même si elle bouge.
        Vector3 destinationActuelle = cibleMobile.position;

        // 1. Vérification de la distance (Condition d'achèvement)
        float distanceRestante = Vector3.Distance(joueur.position, destinationActuelle);

        if (distanceRestante <= distanceDeDetection)
        {
            // Cible atteinte ! On demande au manager d'effacer la destination.
            GPSManager.Instance.ClearDestination();
            return;
        }

        // 2. Calcul de la direction horizontale (ignorer la hauteur, si nécessaire dans l'espace)
        Vector3 directionVersCible = (destinationActuelle - joueur.position);
        directionVersCible.y = 0;

        // 3. Calcul et application de l'angle de rotation
        float angleRotation = Vector3.SignedAngle(joueur.forward, directionVersCible, Vector3.up);

        flecheUI.localEulerAngles = new Vector3(0, 0, -angleRotation);
    }
}
