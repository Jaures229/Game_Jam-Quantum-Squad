using DG.Tweening;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Move Player")]
    public CharacterController _characterController;

    public FixedJoystick _joyStick;
    public float _acceleration = 5f;
    public float _maxSpeed = 5f;
    public float _deceleration = 3f;
    private Vector3 velocity = Vector3.zero;

    [Header("Jump Player")]
    public float jumpForce = 1f;
    private float yVelocity = 0f;
    public float gravity = -9.81f;
    private bool isJumping = false;
    public Animator animator;

    void Start()
    {
        //_characterController = GetComponent<CharacterController>();
    }


    void Update()
    {
        //Gestion du Jump
        if (_characterController.isGrounded && yVelocity < 0)
            yVelocity = -2f;

        if (isJumping)
            yVelocity = jumpForce;

        yVelocity += gravity * Time.deltaTime;

        // Gestion du Déplacement avec Inertie

        Vector3 inputDirection = transform.right * _joyStick.Horizontal + transform.forward * _joyStick.Vertical;

        if (inputDirection.magnitude > 0.1f)
        {
            velocity += inputDirection.normalized * _acceleration * Time.deltaTime;
            animator.SetTrigger("Walk");
        }
        else
        {
            velocity = Vector3.Lerp(velocity, Vector3.zero, _deceleration * Time.deltaTime);
        }

        velocity = Vector3.ClampMagnitude(velocity, _maxSpeed);

        velocity.y = yVelocity;

        _characterController.Move(velocity * Time.deltaTime);

    }

    public void Jump(bool isHeld)
    {
        if (_characterController.isGrounded && !isHeld)
            return;

        isJumping = isHeld;
    }

    void OnTriggerEnter(Collider other)
    {
        //_characterController.DOPlayForward
    }
}
