using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController1 : MonoBehaviour
{
    [SerializeField] private Transform _player;

    void Update()
    {
        transform.position = new Vector3 (_player.transform.position.x, _player.transform.position.y, transform.position.z);
    }
}
