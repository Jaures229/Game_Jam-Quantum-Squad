using UnityEngine;

public class BackDoorSystem : MonoBehaviour
{
    // Référence à l'Animator unique qui gère les deux calques
    public Animator door_animator;

    private const string OPEN_TRIGGER_PARAM = "OpenDoor"; 
    private const string CLOSE_TRIGGER_PARAM = "CloseDoor"; 

    // --- Entrée dans la zone (Ouverture) ---
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (door_animator != null)
            {
                Debug.Log("Joueur détecté. Déclenchement de l'ouverture sur les deux calques.");
                // SetTrigger déclenche la transition sur tous les calques qui ont ce Trigger
                door_animator.SetTrigger(OPEN_TRIGGER_PARAM); 
            }
        }
    }

    // --- Sortie de la zone (Fermeture) ---
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (door_animator != null)
            {
                Debug.Log("Joueur sorti. Déclenchement de la fermeture sur les deux calques.");
                door_animator.SetTrigger(CLOSE_TRIGGER_PARAM);
            }
        }
    }
}

// using UnityEngine;
// using System.Collections; // Nécessaire pour les coroutines

// public class BackDoorSystem : MonoBehaviour
// {
//     // Références aux Transform des pièces de la porte à animer
//     [Header("Pièces de la porte")]
//     [Tooltip("Liste des Transforms des différentes pièces de la porte à faire pivoter.")]
//     public Transform[] doorParts;

//     [Header("Paramètres d'Animation")]
//     [Tooltip("Durée de l'animation d'ouverture/fermeture en secondes.")]
//     public float animationDuration = 1.0f;
    
//     [Tooltip("Courbe d'animation pour un mouvement plus fluide (optionnel).")]
//     public AnimationCurve animationCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

//     [Header("Angles de Rotation (Locaux)")]
//     [Tooltip("Angles de rotation locaux pour chaque pièce lorsque la porte est FERMÉE.")]
//     public Vector3[] closedLocalRotations;
    
//     [Tooltip("Angles de rotation locaux pour chaque pièce lorsque la porte est OUVERTE.")]
//     public Vector3[] openLocalRotations;

//     // État actuel de la porte
//     private bool isDoorOpen = false;
//     private Coroutine currentDoorAnimation;

//     // --- Vérifications Initiales ---
//     void Start()
//     {
//         // Assurez-vous que les tableaux sont de la même taille
//         if (doorParts.Length != closedLocalRotations.Length || doorParts.Length != openLocalRotations.Length)
//         {
//             Debug.LogError("Les tableaux 'doorParts', 'closedLocalRotations' et 'openLocalRotations' doivent avoir la même taille !");
//             this.enabled = false; // Désactiver le script pour éviter des erreurs
//             return;
//         }

//         // Assurez-vous que la porte commence dans l'état fermé
//         for (int i = 0; i < doorParts.Length; i++)
//         {
//             if (doorParts[i] != null)
//             {
//                 doorParts[i].localEulerAngles = closedLocalRotations[i];
//             }
//         }
//     }

//     // --- Entrée dans la zone (Ouverture) ---
//     void OnTriggerEnter(Collider other)
//     {
//         if (other.gameObject.CompareTag("Player"))
//         {
//             if (!isDoorOpen) // N'ouvrir que si elle est fermée
//             {
//                 Debug.Log("Joueur détecté. Lancement de l'ouverture de la porte.");
//                 if (currentDoorAnimation != null) StopCoroutine(currentDoorAnimation);
//                 currentDoorAnimation = StartCoroutine(AnimateDoor(true));
//             }
//         }
//     }

//     // --- Sortie de la zone (Fermeture) ---
//     void OnTriggerExit(Collider other)
//     {
//         if (other.gameObject.CompareTag("Player"))
//         {
//             if (isDoorOpen) // Ne fermer que si elle est ouverte
//             {
//                 Debug.Log("Joueur sorti. Lancement de la fermeture de la porte.");
//                 if (currentDoorAnimation != null) StopCoroutine(currentDoorAnimation);
//                 currentDoorAnimation = StartCoroutine(AnimateDoor(false));
//             }
//         }
//     }


//     // --------------------------------------------------------------------------
//     //                           LA COROUTINE D'ANIMATION
//     // --------------------------------------------------------------------------

//     /// <summary>
//     /// Anime la rotation des pièces de la porte sur une durée spécifiée.
//     /// </summary>
//     /// <param name="open">True pour l'ouverture, False pour la fermeture.</param>
//     public IEnumerator AnimateDoor(bool open)
//     {
//         float timer = 0f;

//         // Préparer les Quaternions de départ et les angles cibles
//         Quaternion[] startQuaternions = new Quaternion[doorParts.Length];
//         Vector3[] targetRotations = new Vector3[doorParts.Length];

//         for (int i = 0; i < doorParts.Length; i++)
//         {
//             if (doorParts[i] != null)
//             {
//                 // Utiliser la rotation de départ EXACTE en Quaternion (Solution pour angles d'Euler)
//                 startQuaternions[i] = doorParts[i].localRotation; 
                
//                 // Déterminer les angles cibles (Open ou Closed)
//                 targetRotations[i] = open ? openLocalRotations[i] : closedLocalRotations[i];
//             }
//         }
        
//         while (timer < animationDuration)
//         {
//             float progress = timer / animationDuration;
//             // Appliquer la courbe pour l'accélération/décélération (EaseInOut)
//             float curveValue = animationCurve.Evaluate(progress); 

//             for (int i = 0; i < doorParts.Length; i++)
//             {
//                 if (doorParts[i] != null)
//                 {
//                     Debug.Log($"Animé : Pièce {i}, Rotation X: {doorParts[i].localEulerAngles.x}");
//                     // Interpolation Sphérique (Slerp) pour un mouvement de rotation fluide
//                     doorParts[i].localRotation = Quaternion.Slerp(
//                         startQuaternions[i], 
//                         Quaternion.Euler(targetRotations[i]), // Convertir l'angle cible en Quaternion
//                         curveValue
//                     );
//                 }
//             }

//             timer += Time.deltaTime;
//             yield return null; // Attend la prochaine frame (boucle d'animation)
//         }

//         // 🎯 FIN DE L'ANIMATION : S'assurer que l'objet est à la position finale exacte
//         for (int i = 0; i < doorParts.Length; i++)
//         {
//             if (doorParts[i] != null)
//             {
//                 // Forcer la position finale
//                 doorParts[i].localEulerAngles = open ? openLocalRotations[i] : closedLocalRotations[i];
//             }
//         }

//         isDoorOpen = open; // Mettre à jour l'état final
//         currentDoorAnimation = null; // Libérer la coroutine
//     }
// }
