using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrailPointUI : MonoBehaviour
{
    private Image image;
    [SerializeField] private float fadeDuration = 10f;
    private float timer = 0f;

    void Start()
    {
        image = GetComponent<Image>();
        Destroy(gameObject, fadeDuration); // Détruit après 2 secondes
    }

    void Update()
    {
        timer += Time.deltaTime;
        float alpha = Mathf.Lerp(1f, 0f, timer / fadeDuration);
        if (image != null)
        {
            Color color = image.color;
            color.a = alpha;
            image.color = color;
        }
    }
}
