using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarSpawners : MonoBehaviour
{
    public GameObject starPrefab;
    public int numberOfStars = 100;
    public float spawnRangeX = 10f;
    public float spawnRangeY = 10f;
    public float starDespawnThreshold = 3f;

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
        CheckStarsInView();
    }

    void SpawnStars()
    {
        for (int i = 0; i < numberOfStars; i++)
        {
            float positionX = Random.Range(-spawnRangeX, spawnRangeX);
            float positionY = Random.Range(-spawnRangeY, spawnRangeY);

            Vector3 spawnPosition = new Vector3(positionX, positionY, 0f);
            GameObject star = Instantiate(starPrefab, spawnPosition, Quaternion.identity);
            stars.Add(star);

            float randomScale = Random.Range(0.2f, 0.4f);
            star.transform.localScale = new Vector3(randomScale, randomScale, 1f);
        }
    }

    void CheckStarsInView()
    {
        foreach (GameObject star in stars)
        {
            Vector3 starViewportPosition = cam.WorldToViewportPoint(star.transform.position);

            if (starViewportPosition.x < 0 || starViewportPosition.x > 1 || starViewportPosition.y < 0 || starViewportPosition.y > 1)
            {
                Destroy(star);
                stars.Remove(star);

                SpawnStarInView();
            }
        }
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
