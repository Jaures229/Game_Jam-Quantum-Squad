using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlanetController : MonoBehaviour
{
    public Transform sun;  // Référence au Soleil
    public float orbitSpeed = 10f; // Vitesse orbitale en degrés par seconde
    public bool activated = false;
    // Start is called before the first frame update

    public LineRenderer lineRenderer;
    public int maxPoints = 100; // Nombre max de points visibles avant disparition
    public float fadeTime = 2f; // Temps avant disparition
    public GameObject planetTextGameobject;
    void Start()
    {
        planetTextGameobject.SetActive(false);
        if (lineRenderer == null)
            lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (activated) {
            if (sun != null)
            {
                transform.RotateAround(sun.position, Vector3.forward, orbitSpeed * Time.fixedDeltaTime);
            }
        }
    }
}
