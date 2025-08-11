using UnityEngine;

public class SystemSolarManager : MonoBehaviour
{
    [System.Serializable]
    public class PlanetData
    {
        public string _name;
        public GameObject _prefab;
        public float _diameter;
        public float _distanceFromSun;
        public float _orbitSpeed;
        public float _rotationSpeed;
        public Transform _orbitCenter;
    }

    [SerializeField] public PlanetData[] _planets;
    public float a;
    void Start()
    {
        foreach(PlanetData planet in _planets)
        {
            if(planet._prefab != null)
            {
                planet._prefab.transform.localScale = Vector3.one * planet._diameter;

                float _YOffset = Random.Range(-planet._diameter * 0.2f, planet._diameter * 0.2f);
                planet._prefab.transform.position = new Vector3(planet._distanceFromSun, _YOffset, 0);
            }
        }
    }
    void Update()
    {
        foreach(PlanetData planet in _planets)
        {
            if (planet._prefab != null && planet._orbitCenter != null)
            {
                planet._prefab.transform.RotateAround(planet._orbitCenter.position, Vector3.up, planet._orbitSpeed * Time.deltaTime);
                planet._prefab.transform.Rotate(Vector3.up, planet._rotationSpeed * Time.deltaTime);
            }
        }
    }
}
