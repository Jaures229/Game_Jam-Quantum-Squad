using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2 : MonoBehaviour
{
    [SerializeField] private Joystick joystick;
    [SerializeField] private float speed = 5f;
    [SerializeField] private Animator _animator;
    //[SerializeField] private Animator animator;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        
        Vector3 direction = new Vector3(joystick.Horizontal, 0, joystick.Vertical);
        rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);

        if (direction.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, 5f * Time.fixedDeltaTime);
        }
        _animator.SetBool("Walk", direction.magnitude > 0.1f);
    }

    private void UpdateAnimation()
    {
    }
}
