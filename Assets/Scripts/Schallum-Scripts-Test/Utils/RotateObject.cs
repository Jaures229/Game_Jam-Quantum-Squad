using UnityEngine;

public class RotateObject : MonoBehaviour
{
    [Header("Vitesse de rotation en degrés par seconde")]
    public Vector3 rotationSpeed = new Vector3(0, 100, 0);

    void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }
}
