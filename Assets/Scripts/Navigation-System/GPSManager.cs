using UnityEngine;
using System;

public class GPSManager : MonoBehaviour
{
    // Événement 1 : Pour notifier la flèche (la cible a changé)
    public event Action<Transform, string> OnTargetSet;

    // NOUVEL ÉVÉNEMENT 2 : Pour notifier les systèmes (cible atteinte)
    public event Action<string> OnTargetReached;

    // Singleton : Rendre le manager accessible de partout
    public static GPSManager Instance { get; private set; }

    // Événement pour notifier les abonnés (la flèche) que la destination a changé.
    // L'événement passe maintenant la référence au Transform de la cible.
    public event Action<Transform> OnTargetTransformSet;

    // Propriété publique pour stocker la référence au Transform de la destination actuelle.
    // Si cette valeur est null, il n'y a pas de destination.
    public Transform TargetTransform { get; private set; }


    // NOUVELLE PROPRIÉTÉ : L'ID de la cible actuelle
    public string TargetID { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

           // DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Définit une nouvelle cible et son ID unique.
    /// </summary>
    public void SetNewTarget(Transform newTarget, string targetId)
    {
        TargetTransform = newTarget;
        TargetID = targetId;

        if (newTarget != null)
        {
            Debug.Log($"Nouvelle cible GPS définie : {newTarget.name} (ID: {targetId})");
        }

        // Notifier la flèche : on passe la cible et l'ID (l'ID peut être null pour effacer)
        OnTargetSet?.Invoke(TargetTransform, TargetID);
    }
    
    /// <summary>
    /// Appelé par la flèche lorsque la distance est atteinte.
    /// Déclenche l'événement d'achèvement de mission.
    /// </summary>
    public void ClearDestination()
    {
        if (TargetID != null)
        {
            Debug.Log($"Cible GPS atteinte ! ID: {TargetID}");
            // 🚨 ÉTAPE CRUCIALE : Déclenche l'événement OnTargetReached avec l'ID
            OnTargetReached?.Invoke(TargetID);
        }
        
        // On efface la référence de la cible
        SetNewTarget(null, null); 
    }
}
