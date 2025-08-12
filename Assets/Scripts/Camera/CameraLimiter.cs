using UnityEngine;

public class CameraLimiter : MonoBehaviour
{
    public Transform target; // Le joueur
    public float minX, maxX, minY, maxY;

    private float camHalfHeight;
    private float camHalfWidth;

    void Start()
    {
        Camera cam = Camera.main;
        camHalfHeight = cam.orthographicSize;
        camHalfWidth = camHalfHeight * cam.aspect;
    }

    void LateUpdate()
    {
        float clampedX = Mathf.Clamp(target.position.x, minX + camHalfWidth, maxX - camHalfWidth);
        float clampedY = Mathf.Clamp(target.position.y, minY + camHalfHeight, maxY - camHalfHeight);

        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }
}
