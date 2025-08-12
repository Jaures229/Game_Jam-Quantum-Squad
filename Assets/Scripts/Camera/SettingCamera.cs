using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingCamera : MonoBehaviour
{
    [SerializeField] private float referenceFOV = 60f;
    [SerializeField] private float referenceAspect = 16f /9f;
    private Camera _cam;
    void Start()
    {
        _cam = GetComponent<Camera>();
        AjusteFOV();
    }

    void AjusteFOV()
    {
        float currentAspect = (float)Screen.width / Screen.height;
        _cam.fieldOfView = referenceFOV * (referenceAspect / currentAspect);
    }
}
