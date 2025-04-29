using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testAnim : MonoBehaviour
{
    public Texture[] frames; // toutes tes textures (frames de l'animation)
    public float frameRate = 10f; // nombre d'images par seconde

    private Material material;
    private int currentFrame;
    private float timer;

    void Start()
    {
        material = GetComponent<Renderer>().material;
    }

    void Update()
    {
        if (frames.Length == 0) return;

        timer += Time.deltaTime;

        if (timer >= 1f / frameRate)
        {
            currentFrame = (currentFrame + 1) % frames.Length;
            material.mainTexture = frames[currentFrame];
            timer = 0f;
        }
    }
}
