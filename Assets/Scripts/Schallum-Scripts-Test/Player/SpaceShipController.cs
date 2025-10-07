using UnityEngine;
using UnityEngine.UI;

public class SpaceShipController : MonoBehaviour
{
    private Rigidbody rb;

    [Header("Références")]
    public Slider _slider;                // Contrôle de la vitesse
    public FixedTouchField lookTouchField; // Zone de glissement pour orienter
    public Transform shipVisual;          // Mesh enfant du vaisseau

    [Header("Réglages de déplacement")]
    public float forwardSpeed = 15f;      // Vitesse avant
    public float rotationSpeed = 60f;     // Vitesse de rotation (yaw/pitch)
    public float tiltAmount = 25f;        // Inclinaison latérale (roll)
    public float pitchTilt = 15f;         // Inclinaison du nez (visuel)
    public float smoothFactor = 5f;       // Lissage des rotations

    private Vector2 inputDir;
    private Quaternion targetRotation;    // Rotation du vaisseau réel
    private Quaternion visualRotation;    // Rotation du mesh visuel

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        targetRotation = transform.rotation;
    }

    void Update()
    {
        // Récupère la direction du slide
        if (lookTouchField.pressed)
            inputDir = lookTouchField.touchDirection.normalized;
        else
            inputDir = Vector2.zero;
    }

    void FixedUpdate()
    {
        // --- Mouvement vers l'avant ---
        Vector3 forwardMove = transform.forward * forwardSpeed * Mathf.Max(0, _slider.value);
        rb.linearVelocity = forwardMove;

        // --- Rotation du vaisseau (trajectoire réelle) ---
        float yaw = inputDir.x * rotationSpeed * Time.fixedDeltaTime;   // Gauche / droite
        float pitch = -inputDir.y * rotationSpeed * Time.fixedDeltaTime; // Haut / bas

        targetRotation *= Quaternion.Euler(pitch, yaw, 0);
        rb.MoveRotation(Quaternion.Lerp(rb.rotation, targetRotation, Time.fixedDeltaTime * smoothFactor));

        // --- Rotation visuelle du mesh (effet immersif) ---
        float rollVisual = -inputDir.x * tiltAmount;   // penche sur le côté
        float pitchVisual = -inputDir.y * pitchTilt;   // nez monte / descend

        visualRotation = Quaternion.Euler(pitchVisual, 0, rollVisual);
        shipVisual.localRotation = Quaternion.Lerp(shipVisual.localRotation, visualRotation, Time.fixedDeltaTime * smoothFactor);
    }
}
