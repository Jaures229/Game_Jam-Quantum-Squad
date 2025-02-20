using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarSpawners : MonoBehaviour
{
    public GameObject starPrefab;
    public int numberOfStars = 100;
    public float spawnRangeX = 10f;
    public float spawnRangeY = 10f;

    private List<GameObject> stars = new List<GameObject>();
    private Camera cam;

    void Start()
    {
        if (starPrefab == null)
        {
            Debug.LogError("Star prefab is not assigned!");
            return;
        }

        cam = Camera.main;
        SpawnStars();
    }

    void Update()
    {
        RemoveAndSpawnStars();
    }

    void SpawnStars()
    {
        for (int i = 0; i < numberOfStars; i++)
        {
            SpawnStarInView();
        }
    }

    void RemoveAndSpawnStars()
    {
        List<GameObject> starsToRemove = new List<GameObject>();

        for (int i = 0; i < stars.Count; i++)
        {
            if (!IsStarInView(stars[i]))
            {
                starsToRemove.Add(stars[i]); // Marquer les étoiles à supprimer
            }
        }

        // Supprimer et recréer les étoiles en dehors de la boucle de parcours
        foreach (GameObject star in starsToRemove)
        {
            stars.Remove(star);
            Destroy(star);
            SpawnStarInView();
        }
    }

    bool IsStarInView(GameObject star)
    {
        Vector3 starViewportPosition = cam.WorldToViewportPoint(star.transform.position);
        return starViewportPosition.x >= 0 && starViewportPosition.x <= 1 &&
               starViewportPosition.y >= 0 && starViewportPosition.y <= 1;
    }

    void SpawnStarInView()
    {
        float positionX = Random.Range(cam.transform.position.x - spawnRangeX, cam.transform.position.x + spawnRangeX);
        float positionY = Random.Range(cam.transform.position.y - spawnRangeY, cam.transform.position.y + spawnRangeY);

        Vector3 spawnPosition = new Vector3(positionX, positionY, 0f);
        GameObject star = Instantiate(starPrefab, spawnPosition, Quaternion.identity);
        stars.Add(star);

        float randomScale = Random.Range(0.2f, 0.4f);
        star.transform.localScale = new Vector3(randomScale, randomScale, 1f);
    }
}
