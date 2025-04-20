using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0f, 5f, -25f);

    void LateUpdate()
    {
        if (target != null)
        {
            transform.position = target.TransformPoint(offset);

            transform.rotation = target.rotation;
        }
    }
}