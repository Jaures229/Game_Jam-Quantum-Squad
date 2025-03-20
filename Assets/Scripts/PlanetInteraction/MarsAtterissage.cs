using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarsAtterissage : MonoBehaviour
{
    [SerializeField] GameObject _buttonAtterissage;

    private void Awake()
    {
        if (_buttonAtterissage != null)
        {
            _buttonAtterissage.SetActive(false);
        }
    }
    void Start()
    {

    }

    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _buttonAtterissage.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _buttonAtterissage.SetActive(false);
        }
    }
}
