using UnityEngine;

public class SpaceShipController : MonoBehaviour
{
<<<<<<< Updated upstream
    private Rigidbody _spaceShip;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
=======
    //private Rigidbody _spaceShipRB;

    //float verticalMove;
    //float horizontalMove;
    //float mouseInputX;
    //float mouseInputY;
    //float rollInput;
    //[SerializeField] float speedMult = 1;
    //[SerializeField] float speedMultAngle = 0.5f;
    //[SerializeField] float speedRollMultAngle = 0.05f;


    //void Start()
    //{
    //    Cursor.lockState = CursorLockMode.Locked;
    //    _spaceShipRB = GetComponent<Rigidbody>();
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    verticalMove = Input.GetAxis("Vertical");
    //    horizontalMove = Input.GetAxis("Horizontal");
    //    rollInput = Input.GetAxis("Roll");
    //    mouseInputX = Input.GetAxis("Mouse X");
    //    mouseInputY = Input.GetAxis("Mouse Y");
    //}

    //private void FixedUpdate()
    //{
    //    Debug.Log("Vertical input: " + verticalMove);
    //    _spaceShipRB.AddForce(_spaceShipRB.transform.TransformDirection(Vector3.forward) * verticalMove * speedMult, ForceMode.VelocityChange);
    //    _spaceShipRB.AddForce(_spaceShipRB.transform.TransformDirection(Vector3.right) * horizontalMove * speedMult, ForceMode.VelocityChange);
    //    _spaceShipRB.AddTorque(_spaceShipRB.transform.right * speedMultAngle * mouseInputY * -1,
    //   ForceMode.VelocityChange);
    //    _spaceShipRB.AddTorque(_spaceShipRB.transform.up * speedMultAngle * mouseInputX,
    //        ForceMode.VelocityChange);
    //    _spaceShipRB.AddTorque(_spaceShipRB.transform.forward * speedRollMultAngle * rollInput, ForceMode.VelocityChange);
    //}


    // version 2-------------------------------------------------------------------------------------------------------
    //[Header("Références")]
    //public FixedJoystick moveJoystick;
    //public FixedTouchField lookTouchField;

    //private Rigidbody _spaceShipRB;

    //[Header("Réglages")]
    //public float speedMult = 10f;
    //public float speedMultAngle = 0.3f;
    //public float speedRollMultAngle = 0.05f;
    //public float lookSensitivity = 0.1f;

    //private float rollInput;

    //void Start()
    //{
    //    _spaceShipRB = GetComponent<Rigidbody>();

    //}


    //void FixedUpdate()
    //{
    //    // --- Mouvement ---
    //    float verticalMove = moveJoystick.Vertical;
    //    float horizontalMove = moveJoystick.Horizontal;

    //    _spaceShipRB.AddForce(_spaceShipRB.transform.TransformDirection(Vector3.forward) * verticalMove * speedMult, ForceMode.VelocityChange);
    //    _spaceShipRB.AddForce(_spaceShipRB.transform.TransformDirection(Vector3.right) * horizontalMove * speedMult, ForceMode.VelocityChange);

    //    // --- Rotation via Touch ---
    //    float mouseInputX = lookTouchField._touchDist.x * lookSensitivity;
    //    float mouseInputY = lookTouchField._touchDist.y * lookSensitivity;

    //    _spaceShipRB.AddTorque(_spaceShipRB.transform.right * speedMultAngle * -mouseInputY, ForceMode.VelocityChange);
    //    _spaceShipRB.AddTorque(_spaceShipRB.transform.up * speedMultAngle * mouseInputX, ForceMode.VelocityChange);

    //    // --- Roll ---
    //    _spaceShipRB.AddTorque(_spaceShipRB.transform.forward * speedRollMultAngle * rollInput, ForceMode.VelocityChange);
    //}

    //public void StartRollLeft() { rollInput = -1; }
    //public void StartRollRight() { rollInput = 1; }
    //public void StopRoll() { rollInput = 0; }

    [Header("Références")]
    public FixedJoystick moveJoystick;
    public FixedTouchField lookTouchField;

    [Header("Réglages")]
    public float forwardSpeed = 15f;         // Vitesse avant
    public float turnSpeed = 60f;            // Vitesse de rotation horizontale
    public float pitchSpeed = 40f;           // Vitesse de rotation verticale
    public float tiltAmount = 15f;           // Inclinaison visuelle max
    public float tiltSpeed = 5f;             // Vitesse de lissage du tilt
    public float minPitch = -45f;            // Limite de montée
    public float maxPitch = 45f;            // Limite de descente
    [SerializeField] private float smoothFactor = 0.1f;

    private Rigidbody rb;
    private float currentTilt = 0f;
    private float pitchAngle = 0f;
    private float targetPitchAngle;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // --- Avancer ---
        Vector3 forwardMove = transform.forward * forwardSpeed * Mathf.Max(0, moveJoystick.Vertical);
        rb.linearVelocity = forwardMove;

        // --- Rotation gauche/droite ---
        float turnInput = moveJoystick.Horizontal;
        transform.Rotate(Vector3.up, turnInput * turnSpeed * Time.fixedDeltaTime, Space.Self);

        // --- Contrôle du pitch avec le doigt ---
        float pitchInput = -lookTouchField._touchDist.y * Time.fixedDeltaTime * pitchSpeed;
        targetPitchAngle = Mathf.Clamp(pitchAngle + pitchInput, minPitch, maxPitch);
        pitchAngle = Mathf.Lerp(pitchAngle, targetPitchAngle, smoothFactor);
        //pitchAngle = Mathf.Clamp(pitchAngle + pitchInput, minPitch, maxPitch);

        transform.localRotation = Quaternion.Euler(pitchAngle, transform.localEulerAngles.y, transform.localEulerAngles.z);

        // --- Tilt visuel pour le style ---
        float targetTilt = -turnInput * tiltAmount;
        currentTilt = Mathf.Lerp(currentTilt, targetTilt, Time.fixedDeltaTime * tiltSpeed);

        // Appliquer tilt sans casser le pitch
        transform.rotation = Quaternion.Euler(pitchAngle, transform.eulerAngles.y, currentTilt);
>>>>>>> Stashed changes
    }
}
