using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _spaceShipTransform;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3( _spaceShipTransform.position.x, _spaceShipTransform.position.y, transform.position.z);
    }
}
