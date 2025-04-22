using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
public class PlayerControler : MonoBehaviour
{
    public static PlayerControler instance;
    [SerializeField] private Rigidbody _rigidbody;
    //[SerializeField] private FixedJoystick _joystick;
   // [SerializeField] private Animator _animator;

    [SerializeField] private float _speed;
    [SerializeField] private float _speedRotation;
    [SerializeField] private Camera _camera;

    [Header("Boost Setting")]
    [SerializeField] private float _boostMutiplicateur = 2.0f;
    [SerializeField] private float _boostDuration = 20.0f;
    [SerializeField] private float _fadeOutDuration = 5.0f;
    private float _baseSpeed;
    private bool _isBoosting = false;
    public int _healthBar = 9;


    [Header("Acelerometer Setting")]
    public float forwardSpeed = 5f;
    public float moveSpeed = 20f;
    public float rotationSpeed = 100f;
    public float maxRotationAngle = 45f;

    private bool isBoostingForward = false;
    private bool isMovingForward = false;

    private float currentYRotation = 0f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _baseSpeed = _speed;
    }

    private void FixedUpdate()
    {
        /*_rigidbody.velocity = new Vector3(_joystick.Horizontal * _speed, 0, _joystick.Vertical * _speed);

        bool isMoving = _joystick.Horizontal != 0 || _joystick.Vertical != 0;

        Vector3 targetPosition = new Vector3(transform.position.x, transform.position.y, _camera.transform.position.z + 15f);

        if (isMoving)
        {
            Vector3 direction = new Vector3(_rigidbody.velocity.x, 0f, _rigidbody.velocity.z);
            if (direction.magnitude > 0.1 )
            {
                Quaternion _targetRotation = Quaternion.LookRotation(direction);
                Quaternion _rotation = Quaternion.RotateTowards(transform.rotation, _targetRotation, _speedRotation * Time.deltaTime);
                _rigidbody.MoveRotation(_rotation);
            }
        }
        else
        {
            //_animator.SetBool("Idle", false);
        }*/

        // Code for the Actual Movement
        float tilt = Input.acceleration.x;
        float rotationAmount = tilt * rotationSpeed * Time.fixedDeltaTime;
        currentYRotation = Mathf.Clamp(currentYRotation + rotationAmount, -maxRotationAngle, maxRotationAngle);

        Quaternion targetRotation = Quaternion.Euler(0f, currentYRotation, 0f);
        _rigidbody.MoveRotation(targetRotation);

        Vector3 forward = transform.forward;

        if (isBoostingForward)
        {
            _rigidbody.velocity = forwardSpeed * Time.deltaTime * forward;
        }
        else if (isMovingForward)
        {
            _rigidbody.velocity = moveSpeed * Time.fixedDeltaTime * forward;
        }
        else
        {
            _rigidbody.velocity = Vector3.zero;
        }
    }

    public void ActiveBoost()
    {
        if (!_isBoosting)
        {
            _isBoosting = true;

            _baseSpeed = _speed;
            _speed *= _boostMutiplicateur;

           /* Debug.Log("Boost activé");*/
            StartCoroutine(DisableBoost());
        }
    }

    private IEnumerator DisableBoost()
    {
        yield return new WaitForSeconds(_boostDuration);

        float elapsedTime = 0f;

        while (elapsedTime < _fadeOutDuration)
        {
            _speed = Mathf.Lerp(_baseSpeed * _boostMutiplicateur, _baseSpeed, elapsedTime / _fadeOutDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        _speed = _baseSpeed;
        _isBoosting = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Astéroides") == true)
        {
            _healthBar -= 1;
        }
    }

    public void StartBoost() => isBoostingForward = true;
    public void StopBoost() => isBoostingForward = false;

    public void StartMoving() => isMovingForward = true;
    public void StopMoving() => isMovingForward = false;
}
