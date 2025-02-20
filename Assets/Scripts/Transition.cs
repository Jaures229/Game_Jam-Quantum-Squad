using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : MonoBehaviour
{
    public void Transi ()
    {
        if (SceneLoader.Instance != null)
        {
            SceneLoader.Instance.StartLoading();
        }
    }
}
