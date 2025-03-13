using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlanetController : MonoBehaviour
{
    public Transform sun;  // Référence au Soleil
    public float orbitSpeed = 10f; // Vitesse orbitale en degrés par seconde
    //public bool activated = false;
    // Start is called before the first frame update

    public LineRenderer lineRenderer;
    public int maxPoints = 100; // Nombre max de points visibles avant disparition
    public float fadeTime = 2f; // Temps avant disparition
    public GameObject planetTextGameobject;


    void Start()
{
    switch (gameObject.name)
    {
        case "Mercure": orbitSpeed = 6.147f; break;
        case "Venus": orbitSpeed = 4.497f; break;
        case "Terre": orbitSpeed = 3.823f; break;
        case "Mars": orbitSpeed = 3.091f; break;
        case "Jupiter": orbitSpeed = 1.678f; break;
        case "Saturne": orbitSpeed = 1.244f; break;
        case "Uranus": orbitSpeed = 0.873f; break;
        case "Neptune": orbitSpeed = 0.697f; break;
    }
}
    // Update is called once per frame
    void Update()
    {
        //if (activated) {
            if (sun != null)
            {
                transform.RotateAround(sun.position, Vector3.forward, orbitSpeed * Time.fixedDeltaTime);
            }
        //}
    }
}
