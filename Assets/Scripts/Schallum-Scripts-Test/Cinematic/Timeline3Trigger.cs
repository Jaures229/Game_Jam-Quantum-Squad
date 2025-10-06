using UnityEngine;
using System;

public class Timeline3Trigger : MonoBehaviour
{
    public static event Action Cinematic3;

    private void OnTriggerEnter(Collider other)
    {
        Cinematic3?.Invoke();
    }
}
