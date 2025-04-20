using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _distance = 50f;
    private Vector3 _startPosition;
    void Start()
    {
        _startPosition = transform.position;
    }

    void Update()
    {
        if (Vector3.Distance(_startPosition, transform.position) > _distance)
        {
            Destroy(gameObject);
        }
    }
}
