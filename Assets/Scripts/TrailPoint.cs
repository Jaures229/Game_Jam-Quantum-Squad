using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailPoint : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 2f); // Détruit le point après 2 secondes
    }
}
