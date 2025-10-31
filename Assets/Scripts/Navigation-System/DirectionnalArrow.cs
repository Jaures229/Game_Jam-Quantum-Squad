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

    // void Update()
    // {
    //     // Si cibleMobile est null, cela signifie que la destination est effacée ou non définie.
    //     if (cibleMobile == null)
    //     {
    //         return;
    //     }

    //     // *** CALCUL DE LA DESTINATION MOBILE EN TEMPS RÉEL ***

    //     // La position de la cible est lue à chaque frame, même si elle bouge.
    //     Vector3 destinationActuelle = cibleMobile.position;

    //     // 1. Vérification de la distance (Condition d'achèvement)
    //     float distanceRestante = Vector3.Distance(joueur.position, destinationActuelle);

    //     if (distanceRestante <= distanceDeDetection)
    //     {
    //         // Cible atteinte ! On demande au manager d'effacer la destination.
    //         GPSManager.Instance.ClearDestination();
    //         return;
    //     }

    //     // 2. Calcul de la direction horizontale (ignorer la hauteur, si nécessaire dans l'espace)
    //     Vector3 directionVersCible = (destinationActuelle - joueur.position);
    //     directionVersCible.y = 0;

    //     // 3. Calcul et application de l'angle de rotation
    //     float angleRotation = Vector3.SignedAngle(joueur.forward, directionVersCible, Vector3.up);

    //     flecheUI.localEulerAngles = new Vector3(0, 0, -angleRotation);
    // }

    void Update()
    {
        // Si cibleMobile est null, la destination n'est pas définie.
        if (cibleMobile == null)
        {
            return;
        }

        // *** CALCUL DE LA DESTINATION MOBILE EN TEMPS RÉEL ***

        Vector3 destinationActuelle = cibleMobile.position;

        // 1. Vérification de la distance (Condition d'achèvement)
        float distanceRestante = Vector3.Distance(joueur.position, destinationActuelle);

        if (distanceRestante <= distanceDeDetection)
        {
            // Cible atteinte !
            GPSManager.Instance.ClearDestination();
            return;
        }

        // --- MODIFICATIONS POUR LA 3D (Espace) ---
        
        // 2. Calcul de la direction complète (incluant l'axe Y)
        Vector3 directionVersCible = destinationActuelle - joueur.position;
        
        // 3. Calcul du vecteur de la cible vu depuis la caméra de l'espace/vaisseau.
        // Il faut utiliser la direction de la caméra (ou du joueur) pour projeter la direction.
        
        // Obtenons la caméra utilisée pour rendre le joueur (souvent la caméra attachée au vaisseau)
        // Nous supposons que le joueur (vaisseau) a une caméra principale qui suit sa rotation.
        Camera cameraDeReference = Camera.main; // Assurez-vous que c'est la bonne caméra!

        if (cameraDeReference == null)
        {
            Debug.LogError("Caméra principale introuvable.");
            return;
        }

        // A. Calculer le vecteur "devant" (forward) de la caméra.
        Vector3 cameraForward = cameraDeReference.transform.forward;
        
        // B. Projeter la direction de la cible sur le plan de l'écran pour obtenir l'angle 2D.
        
        // Calcul de l'angle horizontal (autour de l'axe Y de la caméra)
        Vector3 directionHorizontal = Vector3.ProjectOnPlane(directionVersCible, cameraDeReference.transform.up).normalized;
        float angleHorizontal = Vector3.SignedAngle(cameraForward, directionHorizontal, cameraDeReference.transform.up);

        // Calcul de l'angle vertical (l'angle d'inclinaison vers le haut/bas)
        // Nous comparons la direction de la cible à l'axe horizontal de la caméra.
        Vector3 directionVertical = Vector3.ProjectOnPlane(directionVersCible, cameraDeReference.transform.right).normalized;
        float angleVertical = Vector3.SignedAngle(cameraForward, directionVertical, cameraDeReference.transform.right);

        // La rotation 2D finale sur l'axe Z (l'angle sur l'écran) est la combinaison des deux angles précédents.
        // Pour une flèche de type boussole 2D sur l'écran, vous avez besoin de l'angle entre le vecteur
        // "vers l'avant de la caméra" et la cible, projeté sur le plan de l'écran.

        // Calcul de l'angle de rotation 2D sur l'écran (le plus simple pour une UI de boussole) :
        
        // 1. Convertir la position 3D de la cible en position 2D (écran)
        Vector3 positionEcranCible = cameraDeReference.WorldToScreenPoint(destinationActuelle);
        
        // 2. Déterminer la position centrale de l'écran
        Vector3 centreEcran = new Vector3(Screen.width / 2, Screen.height / 2, 0);

        // 3. Calculer la direction 2D de la flèche
        Vector3 directionUI = positionEcranCible - centreEcran;

        // 4. Calculer l'angle de rotation (par rapport au haut de l'écran, soit l'axe Y)
        // L'angle est la rotation entre le vecteur UP (0, 1) et le vecteur directionUI (x, y)
        float angleUI = Mathf.Atan2(directionUI.y, directionUI.x) * Mathf.Rad2Deg;

        // 5. Appliquer la rotation. Nous soustrayons 90 pour aligner l'axe Y de l'UI avec l'angle calculé.
        flecheUI.localEulerAngles = new Vector3(0, 0, angleUI - 90);

        // 6. Gérer l'affichage lorsque la cible est derrière la caméra
        if (positionEcranCible.z < 0)
        {
            // La cible est derrière le joueur. La flèche doit pointer dans la direction opposée.
            // On inverse l'angle pour qu'il soit à l'opposé de 180 degrés.
            angleUI += 180f;
            
            // C'est souvent complexe pour une simple flèche. Il est plus simple de cacher la flèche 
            // quand la cible est derrière, ou d'utiliser un indicateur de bord d'écran.
            // Pour l'instant, la rotation devrait fonctionner, mais la flèche pointera au bord de l'écran.
        }
    }
}
