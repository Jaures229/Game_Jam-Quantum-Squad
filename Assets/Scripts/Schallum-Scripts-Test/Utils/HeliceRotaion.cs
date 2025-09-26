using System.Drawing;
using UnityEngine;

public class HeliceRotaion : MonoBehaviour
{
    [Header("Paramètres")]
    public Transform rotor;       // Les hélices à faire tourner
    public float rotationSpeed = 500f; // Vitesse de rotation en degrés/s
    public bool isActive = false;      // Active ou désactive la rotation

    void Update()
    {
        if (isActive && rotor != null)
        {
                rotor.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.Self);
        }
    }

    // Méthode pour activer/désactiver
    public void SetActive(bool state)
    {
        isActive = state;
    }
}
