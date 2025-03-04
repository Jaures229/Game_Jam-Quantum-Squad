// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;

// public class ResponsiveSolarSystem : MonoBehaviour
// {
//     // public RectTransform sun; // Le Soleil en UI
//     // public RectTransform[] planets; // Tableau des planètes (UI Images)

//     // private float[] relativeDistances = { 0.1f, 0.2f, 0.3f, 0.4f, 0.6f, 0.8f, 1.0f, 1.2f }; // Distances relatives (modifiables)

//     // void Start()
//     // {
//     //     float screenWidth = Screen.width;
        
//     //     for (int i = 0; i < planets.Length; i++)
//     //     {
//     //         if (planets[i] != null)
//     //         {
//     //             float distance = screenWidth * relativeDistances[i]; // Ajuster la distance selon l'écran
//     //             planets[i].anchoredPosition = new Vector2(sun.anchoredPosition.x + distance, sun.anchoredPosition.y);
//     //         }
//     //     }
//     // }

//     public RectTransform sun; // Soleil (UI)
//     public RectTransform[] planets; // Liste des planètes (UI)
    
//     private float[] distances = { 0.1f, 0.2f, 0.3f, 0.4f, 0.6f, 0.8f, 1.0f, 1.2f }; // Rayon de l'orbite
//     private float[] angles; // Angles initiaux des planètes

//     void Start()
//     {
//         angles = new float[planets.Length];
//         for (int i = 0; i < planets.Length; i++)
//         {
//             if (planets[i] != null)
//             {
//                 float radius = Screen.width * distances[i] * 0.4f; // Ajuster selon écran
//                 angles[i] = Random.Range(0, 360); // Angle initial aléatoire
//                 planets[i].anchoredPosition = sun.anchoredPosition + new Vector2(Mathf.Cos(angles[i]) * radius, Mathf.Sin(angles[i]) * radius);
//             }
//         }
//     }
// }


using UnityEngine;

public class SolarSystemRealistic : MonoBehaviour
{
    // public RectTransform sun; // Le Soleil (UI)
    // public RectTransform[] planets; // Liste des planètes (UI)
    
    // private float[] distances = { 0.39f, 0.72f, 1.00f, 1.52f, 5.20f, 9.58f, 19.22f, 30.05f }; // Distances réelles
    // private float[] angles = { 0f, 45f, 90f, 135f, 180f, 225f, 270f, 315f }; // Angles réels

    // void Start()
    // {
    //     float scaleFactor = Screen.width * 0.02f; // Ajuste selon l'écran

    //     for (int i = 0; i < planets.Length; i++)
    //     {
    //         if (planets[i] != null)
    //         {
    //             float radius = distances[i] * scaleFactor;
    //             float angleRad = angles[i] * Mathf.Deg2Rad; // Convertit en radians
                
    //             planets[i].anchoredPosition = sun.anchoredPosition + new Vector2(Mathf.Cos(angleRad) * radius, Mathf.Sin(angleRad) * radius);
    //         }
    //     }
    // }
}
