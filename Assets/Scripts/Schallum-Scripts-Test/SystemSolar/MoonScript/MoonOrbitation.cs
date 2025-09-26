using UnityEngine;

public class MoonOrbitation : MonoBehaviour
{
    public Transform earth;
    public float orbitRadius = 200f;   // Distance entre la Lune et la Terre
    public float orbitSpeed = 10f;    // Vitesse d'orbite en degrés par seconde
    public float heightVariation = 0.5f; // Légère variation en Y pour éviter que ce soit trop plat

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

        // Légère variation verticale pour le réalisme
        float y = earth.position.y /*+ Mathf.Sin(currentAngle * 2f) * heightVariation*/;

        // Position finale de la Lune
        transform.position = new Vector3(x, y, z) + earth.position;

        // Rotation sur elle-même si tu veux
        transform.Rotate(Vector3.up, 20f * Time.deltaTime);
    }
}
