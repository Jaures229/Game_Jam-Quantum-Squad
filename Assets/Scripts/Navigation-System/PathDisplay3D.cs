using UnityEngine;

public class PathDisplay3D : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private Transform joueur; // La référence à votre vaisseau
    private Transform cibleMobile = null; // La cible

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false; // Désactivé par défaut
        
        // Assurez-vous d'assigner le joueur ici ou dans l'Inspecteur
        joueur = GameObject.FindWithTag("Player").transform; 

        if (GPSManager.Instance != null)
        {
            GPSManager.Instance.OnTargetSet += HandleNewTarget;
           // GPSManager.Instance.OnTargetClear += HandleTargetClear; // Assurez-vous d'ajouter cet événement
        }
    }

    private void HandleNewTarget(Transform newTarget, string newTargetID)
    {
        Debug.Log("kldklmdjkldkljd");
        cibleMobile = newTarget;
        lineRenderer.enabled = (cibleMobile != null);
    }
    
    private void HandleTargetClear()
    {
        cibleMobile = null;
        lineRenderer.enabled = false;
    }

    void Update()
    {
        if (cibleMobile != null && lineRenderer.enabled)
        {
            // Définit le point de départ sur la position du joueur
            lineRenderer.SetPosition(0, joueur.position);
            
            // Définit le point d'arrivée sur la position de la cible
            lineRenderer.SetPosition(1, cibleMobile.position);
        }
    }
}