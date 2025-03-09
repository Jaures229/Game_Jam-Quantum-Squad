using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _spaceShip;

    void Update()
    {
        transform.position = new Vector3(_spaceShip.position.x, _spaceShip.position.y + 3.50f, transform.position.z);
    }
}
