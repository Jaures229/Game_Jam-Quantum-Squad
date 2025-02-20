using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    [SerializeField] private Transform spaceShipTransform;
    [SerializeField] private RectTransform panelMap;
    private Vector2 minLimits, maxLimits;

    void Start()
    {
        Vector3[] worldCorners = new Vector3[4];
        panelMap.GetWorldCorners(worldCorners);

        minLimits = new Vector2(worldCorners[0].x, worldCorners[0].y);
        maxLimits = new Vector2(worldCorners[2].x, worldCorners[2].y);
    }

    void Update()
    {
        float targetX = Mathf.Clamp(-spaceShipTransform.position.x, -maxLimits.x, -minLimits.x);
        float targetY = Mathf.Clamp(-spaceShipTransform.position.y, -maxLimits.y, -minLimits.y);

        panelMap.transform.position = new Vector3(targetX, targetY, panelMap.transform.position.z);
    }
}
