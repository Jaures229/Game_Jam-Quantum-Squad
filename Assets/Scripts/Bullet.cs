using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _distance = 50f;
    [SerializeField] private float _speed = 10f;
    private Vector3 _target;
    private Vector3 _startPosition;
    private Vector3 _moveDirection;
    private Vector3 _shipVelocity;


    void Start()
    {
        _startPosition = transform.position;
        _target = BulletController.instance.GetTarget();
        _moveDirection = (_target - _startPosition).normalized;
        transform.rotation = Quaternion.LookRotation(_moveDirection) * Quaternion.Euler(90, 0, 0);
    }

    void Update()
    {
        GetComponent<Rigidbody>().velocity = _shipVelocity + _moveDirection * _speed;
        if (Vector3.Distance(_startPosition, transform.position) > _distance)
        {
            Destroy(gameObject);
        }
    }

    public void SetShipVelocity(Vector3 shipVelocity)
    {
        _shipVelocity = shipVelocity;
    }
}
