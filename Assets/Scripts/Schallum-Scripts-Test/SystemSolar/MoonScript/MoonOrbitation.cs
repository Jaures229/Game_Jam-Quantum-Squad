using UnityEngine;

public class MoonOrbitation : MonoBehaviour
{
    public Transform earth;
    public float orbitRadius = 200f;   // Distance entre la Lune et la Terre
    public float orbitSpeed = 10f;    // Vitesse d'orbite en degr�s par seconde
    public float heightVariation = 0.5f; // L�g�re variation en Y pour �viter que ce soit trop plat

    private float currentAngle = 0f;

    void Update()
    {
        if (earth == null) return;

        // Augmente l'angle en fonction de la vitesse et du temps
        currentAngle += orbitSpeed * Time.deltaTime;
        if (currentAngle >= 360f)
            currentAngle -= 360f;

        // Calcul de la position orbitale autour de la Terre sur XZ
        float rad = currentAngle * Mathf.Deg2Rad;
        float x = Mathf.Cos(rad) * orbitRadius;
        float z = Mathf.Sin(rad) * orbitRadius;

        // L�g�re variation verticale pour le r�alisme
        float y = earth.position.y /*+ Mathf.Sin(currentAngle * 2f) * heightVariation*/;

        // Position finale de la Lune
        transform.position = new Vector3(x, y, z) + earth.position;

        // Rotation sur elle-m�me si tu veux
        transform.Rotate(Vector3.up, 20f * Time.deltaTime);
    }
}
