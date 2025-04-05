using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using UnityEngine;

public class MiniMapManager : MonoBehaviour
{
    [SerializeField] private GameObject _circleMarqueur;
    [SerializeField] private GameObject _vaiseauMarqueur;
    [SerializeField] private List<Transform> _planets;
    [SerializeField] private Transform _vaiseau;
    [SerializeField] private float _hauteur;
    [SerializeField] private float scale;
    void Start()
    {
        CreateTriangle(_vaiseau);
        foreach(Transform _planet in _planets)
        {
            CreateCircle(_planet);
        }
    }

    void Update()
    {
        transform.position = new Vector3(_vaiseau.transform.position.x, transform.position.y, _vaiseau.transform.position.z
            );  
    }

    private void CreateCircle(Transform _planet)
    {
        GameObject _circle = Instantiate(_circleMarqueur, _planet.position + Vector3.up * _hauteur, Quaternion.Euler(90, 0,0));

        _circle.layer = LayerMask.NameToLayer("MiniMapObjects");
        _circle.transform.localScale = new Vector3(scale, scale, 1f);

        _circle.transform.SetParent(_planet);
    }

    private void  CreateTriangle (Transform _vaiseau)
    {
        GameObject _triangle = Instantiate(_vaiseauMarqueur, _vaiseau.position + Vector3.up * _hauteur, Quaternion.Euler(90, 0, 0));

       _triangle.layer = LayerMask.NameToLayer("MiniMapObjects");
        _triangle.transform.localScale = new Vector3(3000, 4000, 1f);

        _triangle.transform.SetParent(_vaiseau);
    }
}
