using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Venus : MonoBehaviour
{
    [SerializeField] private Collider _alertCollider;

    private void Awake()
    {
        _alertCollider.isTrigger = true;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Vous etes en approche de la planete  Venus");
        }
    }
}
