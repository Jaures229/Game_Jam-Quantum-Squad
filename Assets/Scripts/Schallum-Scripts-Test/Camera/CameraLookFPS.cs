using UnityEngine;

public class CameraLookFPS : MonoBehaviour
{
    private float _XMove;
    private float _YMove;
    private float _XRotation;
    //private float YRotation;
    [SerializeField] private Transform _playerBody;
    public Vector2 _lockAxis;
    public float sensivity = 40f;
    void Start()
    {
        
    }

    void Update()
    {
        _XMove = _lockAxis.x;
        _YMove = _lockAxis.y;
        _XRotation -= _YMove;
        _XRotation = Mathf.Clamp(_XRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(_XRotation, 0, 0);
        _playerBody.Rotate(Vector3.up * _XMove);
    }
}
