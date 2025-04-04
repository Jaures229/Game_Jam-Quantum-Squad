using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapManager : MonoBehaviour
{
    [SerializeField] private GameObject _circleMarqueur;
    [SerializeField] private List<Transform> _planets;
    [SerializeField] private float _hauteur;
    [SerializeField] private float scale;
    void Start()
    {
        foreach(Transform _planet in _planets)
        {
            CreateCircle(_planet);
        }
    }

    void Update()
    {
        
    }

    private void CreateCircle(Transform _planet)
    {
        GameObject _circle = Instantiate(_circleMarqueur, _planet.position + Vector3.up * _hauteur, Quaternion.Euler(90, 0,0));

        _circle.layer = LayerMask.NameToLayer("MiniMapObjects");
        _circle.transform.localScale = new Vector3(scale, scale, 1f);

        _circle.transform.SetParent(_planet);
    }
}
