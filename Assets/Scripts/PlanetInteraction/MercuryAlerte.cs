using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MercuryAlerte : MonoBehaviour
{
    [SerializeField] private Collider _alertCollider;
    [SerializeField] private List<GameObject> _description;
    [SerializeField] private TextMeshProUGUI _message;

    private void Awake()
    {
        _alertCollider.isTrigger = true;
        if (_description != null)
        {
            foreach (GameObject go in _description)
            {
                go.SetActive(false);
            }
        }
    }

    private void UpdateDescription()
    {
        foreach (GameObject go in _description)
        {
            go.SetActive(true);
        }
        _message.text = "Vous �tes en approche de la plan�te Mercury.";

    }


    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UpdateDescription();
            //Debug.Log("Vous etes en approche de la planete Mars");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (GameObject go in _description)
            {
                go.SetActive(false);
            }
        }
    }
}
