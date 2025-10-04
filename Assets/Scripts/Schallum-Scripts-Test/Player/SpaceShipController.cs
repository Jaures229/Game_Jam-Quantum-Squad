using UnityEngine;
using UnityEngine.UI;

public class SpaceShipController : MonoBehaviour
{
    private Rigidbody rb;

    [Header("Références")]
    public Slider _slider;
    public FixedTouchField lookTouchField;
    public Transform shipVisual; // le mesh enfant
    // 
    [Header("Effets")]
    public float maxForwardSpeed = 40f; // La vitesse maximale réelle (utilisez la même valeur que Forward Speed dans l'Inspecteur)
    [HideInInspector]
    public float currentSpeedRatio = 0f; // Ratio de vitesse actuelle (entre 0 et 1)

    [Header("Réglages")]
    public float forwardSpeed = 50f;
    public float rotationSpeed = 60f;   // vitesse en °/s
    public float tiltAmount = 25f;      // inclinaison max du mesh (roll)
    public float pitchTilt = 15f;       // inclinaison du nez en montée/descente
    public float yawPitchBump = 5f;     // petit relevé du nez quand on tourne
    public float smoothFactor = 5f;     // lissage rotation
    public float accelerationRate = 20f;


    private Vector2 inputDir;
    private Quaternion targetRotation; // pour le parent
    private Quaternion visualRotation; // pour le mesh

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        targetRotation = transform.rotation;
    }

    void Update()
    {
        if (lookTouchField.pressed)
            inputDir = lookTouchField.touchDirection.normalized;
        else
            inputDir = Vector2.zero;
    }

    [System.Obsolete]
    void FixedUpdate()
    {
        float currentThrottle = _slider.value;
    
        // Calculer la vitesse maximale désirée en fonction de la position du slider (entre 0 et forwardSpeed)
        float targetSpeed = forwardSpeed * currentThrottle;

        // --- 1. ACCÉLÉRATION (Poussée) ---
        if (currentThrottle > 0.05f) 
        {
            // Appliquer la force UNIQUEMENT si la vitesse actuelle est inférieure à la vitesse cible
            if (rb.velocity.magnitude < targetSpeed)
            {
                Vector3 thrustDirection = transform.forward;
                float thrustMagnitude = accelerationRate; 
                
                rb.AddForce(thrustDirection * thrustMagnitude, ForceMode.Acceleration);
            }
        }
        
        // --- 2. DÉCÉLÉRATION / FREINAGE ACTIF (Si Linear Damping n'est pas suffisant) ---
        else 
        {
            // --- NOUVEAU : Correction du "Shake" à l'arrêt ---
            // Si la vitesse est inférieure à un petit seuil, on force l'arrêt.
            if (rb.velocity.magnitude < 0.1f)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero; // On arrête aussi la rotation résiduelle
            }
            else
            {
                // Freinage Actif (utilisez ce bloc SEULEMENT si Linear Damping de 5 est trop lent)
                // Si vous n'avez pas de freinage actif, vous pouvez supprimer ce 'else' et laisser le Linear Damping faire le travail.
                
                // Exemple de freinage actif (à ne laisser que si nécessaire) :
                // float brakingFactor = (forwardSpeed / accelerationRate) * 0.1f;
                // rb.AddForce(-rb.velocity * brakingFactor, ForceMode.VelocityChange);
            }
        }
        currentSpeedRatio = currentThrottle;
        // // --- Rotation du parent (trajectoire réelle) ---
        // float yaw = inputDir.x * rotationSpeed * Time.fixedDeltaTime;   // gauche/droite
        // float pitch = -inputDir.y * rotationSpeed * Time.fixedDeltaTime; // haut/bas

        // targetRotation *= Quaternion.Euler(pitch, yaw, 0);
        // rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, Time.fixedDeltaTime * smoothFactor));
        // --- Rotation du parent (trajectoire réelle) ---
        float yaw = inputDir.x * rotationSpeed * Time.fixedDeltaTime;   // gauche/droite
        float pitch = -inputDir.y * rotationSpeed * Time.fixedDeltaTime; // haut/bas

        // Appliquez la rotation directement pour qu'elle soit fluide
        targetRotation *= Quaternion.Euler(pitch, yaw, 0);

        // Laissez le Rigidbody s'orienter vers la cible de manière plus fluide
        rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRotation, Time.fixedDeltaTime * smoothFactor));

        // --- Rotation visuelle du mesh ---
        float rollVisual = -inputDir.x * tiltAmount;   // penche à gauche/droite

        // inclinaison normale du nez + petit "bump" supplémentaire quand on tourne
        float pitchVisual = -inputDir.y * pitchTilt + Mathf.Abs(inputDir.x) * yawPitchBump;

        visualRotation = Quaternion.Euler(pitchVisual, 0, rollVisual);
        shipVisual.localRotation = Quaternion.Lerp(shipVisual.localRotation, visualRotation, Time.fixedDeltaTime * smoothFactor);
    }

}
