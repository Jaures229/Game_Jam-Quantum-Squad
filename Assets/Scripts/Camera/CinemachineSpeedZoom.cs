using UnityEngine;
using Unity.Cinemachine;


public class CinemachineSpeedZoom : MonoBehaviour
{
    // Référence au composant Cinemachine Virtual Camera (V-Cam)
    private CinemachineCamera vCam;
    
    // Référence au script du vaisseau pour récupérer la vitesse
    public SpaceShipController shipController; 
    
    // La distance de caméra normale (par défaut)
    [Header("Réglages Zoom")]
    public float baseDistance = 1.5f; 
    
    // La distance maximale que la caméra atteindra à pleine vitesse
    public float maxDistance = 3.0f; 
    
    // Facteur de lissage pour l'effet de zoom
    public float zoomSmoothFactor = 2.0f; 

    private CinemachineThirdPersonFollow thirdPersonFollow;

    void Start()
    {
        vCam = GetComponent<CinemachineCamera>();
        
        // Trouver le composant Third Person Follow
        thirdPersonFollow = vCam.GetComponent<CinemachineThirdPersonFollow>();

        if (shipController == null)
        {
            Debug.LogError("Le script SpaceShipController n'est pas assigné !");
            enabled = false;
        }

        // Assurez-vous que la distance de base correspond à votre réglage initial
        if (thirdPersonFollow != null)
        {
             baseDistance = thirdPersonFollow.CameraDistance;
        }
    }

    void LateUpdate()
    {
        if (thirdPersonFollow == null) return;

        // Récupérer le ratio de vitesse (entre 0 et 1)
        float speedRatio = shipController.currentSpeedRatio;

        // Calculer la distance de caméra cible : 
        // Interpolation entre la baseDistance (vitesse 0) et maxDistance (vitesse max)
        float targetDistance = Mathf.Lerp(baseDistance, maxDistance, speedRatio);

        // Lisser la transition de la distance de la caméra
        thirdPersonFollow.CameraDistance = Mathf.Lerp(
            thirdPersonFollow.CameraDistance,
            targetDistance,
            Time.deltaTime * zoomSmoothFactor
        );
    }
}