using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetTrailUI : MonoBehaviour
{
    public GameObject trailUIPrefab; // Assigner le prefab UI ici
    public Transform parentCanvas; // Assigner le Canvas contenant les planètes
    public float spawnInterval = 0.3f; // Temps entre chaque point
    private PlanetController planetController;
    private float timer = 0f;


    void Start()
    {
        planetController = GetComponent<PlanetController>();
    }
    void FixedUpdate()
    {
        if (planetController.activated) {
            timer += Time.fixedDeltaTime;
            if (timer >= spawnInterval)
            {
                SpawnTrailPoint();
                timer = 0f;
            }
        }
    }

    void SpawnTrailPoint()
    {
        GameObject trail = Instantiate(trailUIPrefab, parentCanvas);
        trail.transform.position = transform.position; // Position de la planète
    }
}
