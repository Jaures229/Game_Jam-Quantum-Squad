using UnityEngine;

public class TouchController : MonoBehaviour
{
    public FixedTouchField _FixedTouchField;
    public CameraLookFPS _CameraLookFPS;

    void Start()
    {
        
    }

    void Update()
    {
        _CameraLookFPS._lockAxis = _FixedTouchField._touchDist;
    }
}
