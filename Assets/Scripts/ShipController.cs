using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ShipController : MonoBehaviour
{
    public float forwardSpeed = 5f;
    public float moveSpeed = 20f;
    public float rotationSpeed = 100f;
    public float maxRotationAngle = 45f;

    private bool isBoostingForward = false;
    private bool isMovingForward = false;
    private Rigidbody _rigidbody;

    private float currentYRotation = 0f;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float tilt = Input.acceleration.x;
        float rotationAmount = tilt * rotationSpeed * Time.fixedDeltaTime;
        currentYRotation = Mathf.Clamp(currentYRotation + rotationAmount, -maxRotationAngle, maxRotationAngle);

        Quaternion targetRotation = Quaternion.Euler(0f, currentYRotation, 0f);
        _rigidbody.MoveRotation(targetRotation);

        Vector3 forward = transform.forward;

        if (isBoostingForward)
        {
            _rigidbody.linearVelocity = forward * forwardSpeed;
        }
        else if (isMovingForward)
        {
            _rigidbody.linearVelocity = forward * moveSpeed;
        }
        else
        {
            _rigidbody.linearVelocity = Vector3.zero;
        }
    }

    public void StartBoost() => isBoostingForward = true;
    public void StopBoost() => isBoostingForward = false;

    public void StartMoving() => isMovingForward = true;
    public void StopMoving() => isMovingForward = false;
}
