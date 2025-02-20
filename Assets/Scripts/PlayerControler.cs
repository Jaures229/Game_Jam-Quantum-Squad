using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
public class PlayerControler : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private FixedJoystick _joystick;

    [SerializeField] private float _speed;
    [SerializeField] private float _speedRotation;

    private void FixedUpdate()
    {
        _rigidbody.velocity = new Vector2(_joystick.Horizontal * _speed, _joystick.Vertical * _speed);

        if (_joystick.Horizontal != 0 || _joystick.Vertical != 0)
        {
            Debug.Log("moveeeee");
            Quaternion _targetRotation = Quaternion.LookRotation(transform.forward, _rigidbody.velocity);
            Quaternion _rotation = Quaternion.RotateTowards(transform.rotation, _targetRotation, _speedRotation * Time.deltaTime);
            _rigidbody.MoveRotation(_rotation);
        }
    }
}
