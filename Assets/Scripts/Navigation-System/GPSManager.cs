using UnityEngine;
using System;

public class GPSManager : MonoBehaviour
{
    // Singleton : Rendre le manager accessible de partout
    public static GPSManager Instance { get; private set; }

    // Événement pour notifier les abonnés (la flèche) que la destination a changé.
    // L'événement passe maintenant la référence au Transform de la cible.
    public event Action<Transform> OnTargetTransformSet;

    // Propriété publique pour stocker la référence au Transform de la destination actuelle.
    // Si cette valeur est null, il n'y a pas de destination.
    public Transform TargetTransform { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Définit une nouvelle cible de navigation en utilisant la référence à son Transform.
    /// </summary>
    /// <param name="newTarget">Le Transform de la planète ou de l'objet à suivre.</param>
    public void SetNewTarget(Transform newTarget)
    {
        TargetTransform = newTarget;

        if (newTarget != null)
        {
            Debug.Log($"Nouvelle cible GPS définie : {newTarget.name}");
        }
        else
        {
            // Si on passe null, c'est équivalent à ClearDestination
            Debug.Log("Cible GPS réinitialisée.");
        }

        // Notifier la flèche de la nouvelle cible (ou de null pour cacher)
        OnTargetTransformSet?.Invoke(TargetTransform);
    }

    /// <summary>
    /// Réinitialise la cible (pour cacher la flèche).
    /// </summary>
    public void ClearDestination()
    {
        Debug.Log("Cible GPS atteinte");
        // On définit la référence à null pour indiquer qu'il n'y a pas de cible.
        SetNewTarget(null);
    }
}
