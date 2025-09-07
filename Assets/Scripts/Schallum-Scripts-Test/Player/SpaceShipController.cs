using UnityEngine;
using UnityEngine.UI;

public class SpaceShipController : MonoBehaviour
{
    private Rigidbody rb;

    [Header("Références")]
    public Slider _slider;
    public FixedTouchField lookTouchField;
    public Transform shipVisual; // le mesh enfant

    [Header("Réglages")]
    public float forwardSpeed = 15f;
    public float rotationSpeed = 60f;   // vitesse en °/s
    public float tiltAmount = 25f;      // inclinaison max du mesh (roll)
    public float pitchTilt = 15f;       // inclinaison du nez en montée/descente
    public float yawPitchBump = 5f;     // petit relevé du nez quand on tourne
    public float smoothFactor = 5f;     // lissage rotation

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

    void FixedUpdate()
    {
        // --- Avancer ---
        Vector3 forwardMove = transform.forward * forwardSpeed * Mathf.Max(0, _slider.value);
        rb.linearVelocity = forwardMove;

        // --- Rotation du parent (trajectoire réelle) ---
        float yaw = inputDir.x * rotationSpeed * Time.fixedDeltaTime;   // gauche/droite
        float pitch = -inputDir.y * rotationSpeed * Time.fixedDeltaTime; // haut/bas

        targetRotation *= Quaternion.Euler(pitch, yaw, 0);
        rb.MoveRotation(Quaternion.Lerp(rb.rotation, targetRotation, Time.fixedDeltaTime * smoothFactor));

        // --- Rotation visuelle du mesh ---
        float rollVisual = -inputDir.x * tiltAmount;   // penche à gauche/droite

        // inclinaison normale du nez + petit "bump" supplémentaire quand on tourne
        float pitchVisual = -inputDir.y * pitchTilt + Mathf.Abs(inputDir.x) * yawPitchBump;

        visualRotation = Quaternion.Euler(pitchVisual, 0, rollVisual);
        shipVisual.localRotation = Quaternion.Lerp(shipVisual.localRotation, visualRotation, Time.fixedDeltaTime * smoothFactor);
    }

}
