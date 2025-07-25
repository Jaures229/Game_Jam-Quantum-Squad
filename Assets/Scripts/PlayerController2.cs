using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController2 : MonoBehaviour
{
    [SerializeField] private Joystick joystick;
    [SerializeField] private float speed = 5f;
    [SerializeField] private Animator _animator;
    bool _collide_materials = false;
    private GameObject _stoneInRange;

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
            Quaternion targetRotation = Quaternion.LookRotation(direction.normalized, Vector3.up);
            rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, 2f * Time.fixedDeltaTime);
        }

        _animator.SetBool("Walk", direction.magnitude > 0.1f);
    }
    private void UpdateAnimation()
    {
    }
    public void TakeObject()
    {
        if (_collide_materials && _stoneInRange != null)
        {
            _animator.SetTrigger("Take");

            // Envoie l'événement de collecte de la pierre
            //QuestEvents.OnPlanetStoneCollected?.Invoke("Mars", "Stone");

            // Désactive la pierre seulement ici
            _stoneInRange.SetActive(false);
            _stoneInRange = null;
            _collide_materials = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Stone"))
        {
            _collide_materials = true;
            _stoneInRange = other.gameObject;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == _stoneInRange)
        {
            _collide_materials = false;
            _stoneInRange = null;
        }
    }
}


