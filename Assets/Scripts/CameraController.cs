using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _spaceShip;
    [SerializeField] private Camera _camera;
    [SerializeField] private Slider _zoomSlider;

    void Start()
    {
        _zoomSlider.onValueChanged.AddListener(UpdateZoom);
        _camera.transform.position = new Vector3(_camera.transform.position.x, _camera.transform.position.y, _zoomSlider.value);
    }
    void Update()
    {
        transform.position = new Vector3(_spaceShip.position.x, _spaceShip.position.y + 3.50f, transform.position.z);
    }

    void UpdateZoom(float value)
    {
        _camera.transform.position = new Vector3(_camera.transform.position.x, _camera.transform.position.y, value);
    }

}
