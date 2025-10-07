using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    // Vitesse de déplacement du joueur, ajustable dans l'Inspector
    public float vitesseDeplacement = 5.0f;
    
    // Vitesse de rotation, ajustable dans l'Inspector
    public float vitesseRotation = 720.0f; // 720 degrés par seconde (rotation rapide)

    private CharacterController characterController;
    private Vector3 directionDeplacement = Vector3.zero;

    void Start()
    {
        // Tente d'obtenir le CharacterController. 
        // Si vous n'en avez pas, vous pouvez commenter la ligne ci-dessous 
        // et décommenter la ligne 'characterController = null;' pour utiliser la transformation directe.
        characterController = GetComponent<CharacterController>();
        if (characterController == null)
        {
            Debug.LogWarning("CharacterController non trouvé. Utilisation directe de Transform.");
        }
    }

    void Update()
    {
        // 1. Lire les entrées du joueur (ZQSD ou WASD)
        float inputX = Input.GetAxis("Horizontal"); // A/Q et D
        float inputZ = Input.GetAxis("Vertical");   // W/Z et S

        // Créer le vecteur de direction, basé sur la caméra si on est en 3D
        // Pour un jeu 3D standard, on utilise le 'forward' et le 'right' de la caméra.
        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;

        // Assurez-vous que le mouvement reste horizontal (ignorer la hauteur de la caméra)
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        // Calculer la direction totale du déplacement
        directionDeplacement = (forward * inputZ) + (right * inputX);
        
        // 2. Appliquer le mouvement
        if (characterController != null)
        {
            // Utiliser CharacterController pour une bonne gestion des collisions
            characterController.Move(directionDeplacement * vitesseDeplacement * Time.deltaTime);
        }
        else
        {
            // Utiliser Transform.Translate si pas de CharacterController (plus simple mais sans collision)
            transform.Translate(directionDeplacement * vitesseDeplacement * Time.deltaTime, Space.World);
        }

        // 3. Orienter le joueur dans la direction du mouvement
        if (directionDeplacement != Vector3.zero)
        {
            // Calculer la rotation souhaitée pour regarder dans la direction
            Quaternion rotationCible = Quaternion.LookRotation(directionDeplacement);

            // Appliquer une rotation progressive (smooth)
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation, 
                rotationCible, 
                vitesseRotation * Time.deltaTime
            );
        }
    }
}