using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _distance = 50f;
    [SerializeField] private float _speed = 10f;
    [SerializeField] private Vector3 _target = new Vector3 (0f, 16f, 1000f);
    private Vector3 _startPosition;
    private Vector3 _moveDirection;


    void Start()
    {
        _startPosition = transform.position;
       // transform.rotation = Quaternion.Euler(90, 0, 0);
        Vector3 _targetPoint = transform.position + transform.TransformDirection(_target);
        _moveDirection = (_targetPoint - _startPosition).normalized;

        transform.rotation = Quaternion.LookRotation(_moveDirection);
    }

    void Update()
    {
        //transform.position += _moveDirection * _speed * Time.deltaTime;
        GetComponent<Rigidbody>().velocity = _moveDirection * _speed;
        if (Vector3.Distance(_startPosition, transform.position) > _distance)
        {
            Destroy(gameObject);
        }
    }
}
