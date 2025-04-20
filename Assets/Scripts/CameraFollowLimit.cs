using UnityEngine;

public class CameraFollowLimit : MonoBehaviour
{
    public Transform target; // Le joueur
    public SpriteRenderer terrainSprite; // Le sprite du terrain

    private float halfHeight;
    private float halfWidth;

    private float minX, maxX, minY, maxY;

    void Start()
    {
        Camera cam = Camera.main;
        halfHeight = cam.orthographicSize;
        halfWidth = halfHeight * cam.aspect;

        Bounds bounds = terrainSprite.bounds;

        minX = bounds.min.x + halfWidth;
        maxX = bounds.max.x - halfWidth;
        minY = bounds.min.y + halfHeight;
        maxY = bounds.max.y - halfHeight;
    }

    void LateUpdate()
    {
        float clampedX = Mathf.Clamp(target.position.x, minX, maxX);
        float clampedY = Mathf.Clamp(target.position.y, minY, maxY);

        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }
}
