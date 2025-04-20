using UnityEngine;

public class PlayerLimiter : MonoBehaviour
{
    public SpriteRenderer terrainSprite;

    private float minX, maxX, minY, maxY;
    private float halfWidth, halfHeight;

    void Start()
    {
        Bounds bounds = terrainSprite.bounds;
        Vector3 size = GetComponent<SpriteRenderer>().bounds.size;

        halfWidth = size.x / 2f;
        halfHeight = size.y / 2f;

        minX = bounds.min.x + halfWidth;
        maxX = bounds.max.x - halfWidth;
        minY = bounds.min.y + halfHeight;
        maxY = bounds.max.y - halfHeight;
    }

    void LateUpdate()
    {
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, minX, maxX);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, minY, maxY);

        transform.position = clampedPosition;
    }
}
