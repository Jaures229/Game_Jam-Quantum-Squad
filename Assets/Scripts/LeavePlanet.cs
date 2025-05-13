using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeavePlanet : MonoBehaviour
{
    [SerializeField] GameObject _rawImage;
    [SerializeField] GameObject _cinematicManager;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LaunchCinematic()
    {
        _rawImage.SetActive(true);
        _cinematicManager.SetActive(true);
    }

}
