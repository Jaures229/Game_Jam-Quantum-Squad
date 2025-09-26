using System.Drawing;
using UnityEngine;

public class HeliceRotaion : MonoBehaviour
{
    [Header("Param�tres")]
    public Transform rotor;       // Les h�lices � faire tourner
    public float rotationSpeed = 500f; // Vitesse de rotation en degr�s/s
    public bool isActive = false;      // Active ou d�sactive la rotation

    void Update()
    {
        if (isActive && rotor != null)
        {
                rotor.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.Self);
        }
    }

    // M�thode pour activer/d�sactiver
    public void SetActive(bool state)
    {
        isActive = state;
    }
}
