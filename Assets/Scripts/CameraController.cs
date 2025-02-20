using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //[SerializeField] private Transform _spaceShipTransform;

    //// Update is called once per frame
    //void Update()
    //{
    //    transform.position = new Vector3( _spaceShipTransform.position.x, _spaceShipTransform.position.y, transform.position.z);
    //}
    [SerializeField] private Transform _spaceShipTransform; // L'avion
    private Vector2 minLimits; // Limite en bas/gauche
    private Vector2 maxLimits; // Limite en haut/droite
    [SerializeField] private RectTransform panelMap; // Le Panel qui représente la carte

    void Start()
    {
        Vector3[] worldCorners = new Vector3[4];
        panelMap.GetWorldCorners(worldCorners); // Récupère les coins du Panel dans le monde

        minLimits = new Vector2(worldCorners[0].x, worldCorners[0].y); // Coin bas gauche
        maxLimits = new Vector2(worldCorners[2].x, worldCorners[2].y); // Coin haut droit
    }

    void Update()
    {
        float targetX = Mathf.Clamp(_spaceShipTransform.position.x, minLimits.x, maxLimits.x);
        float targetY = Mathf.Clamp(_spaceShipTransform.position.y, minLimits.y, maxLimits.y);

        transform.position = new Vector3(targetX, targetY, transform.position.z);
    }
}
