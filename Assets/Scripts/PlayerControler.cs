using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(BoxCollider))] // Rigidbody 3D
public class PlayerControler : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private FixedJoystick _joystick;
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

    private void Start()
    {
        _baseSpeed = _speed;
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = new Vector3(_joystick.Horizontal * _speed, 0, _joystick.Vertical * _speed);

        bool isMoving = _joystick.Horizontal != 0 || _joystick.Vertical != 0;

        Vector3 targetPosition = new Vector3(transform.position.x, transform.position.y, _camera.transform.position.z + 15f);
        //transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * _speedZoom);

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
        }
    }

    public void ActiveBoost()
    {
        if (!_isBoosting)
        {
            _isBoosting = true;

            _baseSpeed = _speed;
            _speed *= _boostMutiplicateur;

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
}
