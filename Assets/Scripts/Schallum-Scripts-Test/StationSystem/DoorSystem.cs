using UnityEngine;

public class DoorSystem : MonoBehaviour
{
    [SerializeField] private Transform _leftDoor;
    [SerializeField] private Transform _rightDoor;

    [SerializeField] private Vector3 _leftOpenOffset;
    [SerializeField] private Vector3 _rightOpenOffset;

    private Vector3 _leftClosedPos;
    private Vector3 _rightClosedPos;

    public float _speed = 3.0f;
    private bool _isOpen = false;

    void Start()
    {
        _leftClosedPos = _leftDoor.localPosition;
        _rightClosedPos = _rightDoor.localPosition;
    }

    void Update()
    {
        Vector3 _leftTarget = _isOpen ? _leftClosedPos + _leftOpenOffset : _leftClosedPos;
        Vector3 _rightTarget = _isOpen ? _rightClosedPos + _rightOpenOffset : _rightClosedPos;

        _leftDoor.localPosition = Vector3.Lerp(_leftDoor.localPosition, _leftTarget, Time.deltaTime * _speed);
        _rightDoor.localPosition = Vector3.Lerp(_rightDoor.localPosition, _rightTarget, Time.deltaTime * _speed);
        Debug.Log("There " + _isOpen);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _isOpen = true;
            Debug.Log("There");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player")) _isOpen = false;
    }
}
