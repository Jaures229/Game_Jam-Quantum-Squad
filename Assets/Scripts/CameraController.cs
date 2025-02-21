using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _spaceShipTransform;
    private Vector2 minLimits;
    private Vector2 maxLimits;
    [SerializeField] private RectTransform panelMap; 

    void Start()
    {
        Vector3[] worldCorners = new Vector3[4];
        panelMap.GetWorldCorners(worldCorners);

        minLimits = new Vector2(worldCorners[0].x, worldCorners[0].y);
        maxLimits = new Vector2(worldCorners[2].x, worldCorners[2].y);
    }

    void Update()
    {
        float targetX = Mathf.Clamp(_spaceShipTransform.position.x, minLimits.x, maxLimits.x);
        float targetY = Mathf.Clamp(_spaceShipTransform.position.y, minLimits.y, maxLimits.y);

        transform.position = new Vector3(targetX, targetY, transform.position.z);
    }
}
