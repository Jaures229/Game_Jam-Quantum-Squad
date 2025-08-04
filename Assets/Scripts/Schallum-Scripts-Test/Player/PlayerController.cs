using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public FixedJoystick _joyStick;
    public float _speedMove = 5f;
    private CharacterController _characterController;
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }


    void Update()
    {
        Vector3 _move = transform.right * _joyStick.Horizontal + transform.forward * _joyStick.Vertical;
        _characterController.Move(_move*_speedMove*Time.deltaTime);
    }
}
