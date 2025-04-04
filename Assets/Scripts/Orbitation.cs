using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbitation : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Transform colliderTarget;
    [SerializeField] private float distance;
    [SerializeField] private float speed;

    private float angle = 0f;

    void Start()
    {
        
    }

    void Update()
    {
        float x = 0;
        float y = 0;
        float z = 0;
        if (target != null)
        {
            angle += speed * Time.deltaTime;
            float radians = angle * Mathf.Deg2Rad;

             x = target.position.x + distance * Mathf.Sin(radians);
             y = transform.position.y;
             z = target.position.z + distance * Mathf.Cos(radians);

            transform.position = new Vector3(x, y, z);
        }

        if (colliderTarget != null)
        {
            colliderTarget.position = new Vector2(x, y);
        }
    }
}
