// using System.Collections;
// using System.Collections.Generic;
// using Unity.VisualScripting;
// using UnityEngine;

// public class PlanetController : MonoBehaviour
// {
//     public Transform sun;  // Référence au Soleil
//     public float orbitSpeed = 10f; // Vitesse orbitale en degrés par seconde
//     //public bool activated = false;
//     // Start is called before the first frame update

//     public LineRenderer lineRenderer;
//     public int maxPoints = 100; // Nombre max de points visibles avant disparition
//     public float fadeTime = 2f; // Temps avant disparition
//     public GameObject planetTextGameobject;

//         void Start()
//     {
//         switch (gameObject.name)
//         {
//             case "Mercure": orbitSpeed = 50f; break;
//             case "Venus": orbitSpeed = 35f; break;
//             case "Terre": orbitSpeed = 30f; break;
//             case "Mars": orbitSpeed = 24f; break;
//             case "Jupiter": orbitSpeed = 13f; break;
//             case "Saturne": orbitSpeed = 10f; break;
//             case "Uranus": orbitSpeed = 7f; break;
//             case "Neptune": orbitSpeed = 5f; break;
//         }
//     }
//     // Update is called once per frame
//     void FixedUpdate()
//     {
//         //if (activated) {
//             if (sun != null)
//             {
//                 transform.RotateAround(sun.position, Vector3.forward, orbitSpeed * Time.fixedDeltaTime);
//             }
//         //}
//     }
// }



using UnityEngine;
using System.Collections.Generic;

public class PlanetController : MonoBehaviour
{
    public Transform sun;  
    public float orbitSpeed = 10f; // Vitesse orbitale en degrés par seconde
    private float angle; // Angle en radians
    private float radius; // Distance du Soleil

    public LineRenderer lineRenderer;
    public int maxPoints = 100; // Nombre max de points pour l’orbite
    public float fadeTime = 2f; // Temps avant disparition des traces
    //private Queue<Vector3> orbitPoints = new Queue<Vector3>();

    void Start()
    {
        // Définition de la vitesse orbitale selon la planète
        switch (gameObject.name)
        {
            case "Mercure": orbitSpeed = 50f; break;
            case "Venus": orbitSpeed = 35f; break;
            case "Terre": orbitSpeed = 30f; break;
            case "Mars": orbitSpeed = 24f; break;
            case "Jupiter": orbitSpeed = 13f; break;
            case "Saturne": orbitSpeed = 10f; break;
            case "Uranus": orbitSpeed = 7f; break;
            case "Neptune": orbitSpeed = 5f; break;
        }

        if (sun != null)
        {
            // Calcul de l'angle initial et du rayon
            Vector3 offset = transform.position - sun.position;
            radius = offset.magnitude; 
            angle = Mathf.Atan2(offset.y, offset.x);
        }

        // Initialisation du LineRenderer
        if (lineRenderer != null)
        {
            lineRenderer.positionCount = 0;
        }
    }

    void FixedUpdate()
    {
        if (sun != null)
        {
            // Mise à jour de l'angle en fonction de la vitesse
            angle += orbitSpeed * Mathf.Deg2Rad * Time.fixedDeltaTime;

            // Calcul de la nouvelle position en utilisant un cercle
            float x = sun.position.x + radius * Mathf.Cos(angle);
            float y = sun.position.y + radius * Mathf.Sin(angle);
            transform.position = new Vector3(x, y, 0);

            // Ajout de la position au LineRenderer
            //UpdateOrbitTrail();
        }
    }

    // void UpdateOrbitTrail()
    // {
    //     if (lineRenderer == null) return;

    //     // Ajoute la nouvelle position et limite le nombre de points
    //     orbitPoints.Enqueue(transform.position);
    //     if (orbitPoints.Count > maxPoints)
    //     {
    //         orbitPoints.Dequeue();
    //     }

    //     // Met à jour le LineRenderer
    //     lineRenderer.positionCount = orbitPoints.Count;
    //     lineRenderer.SetPositions(orbitPoints.ToArray());
    // }
}
