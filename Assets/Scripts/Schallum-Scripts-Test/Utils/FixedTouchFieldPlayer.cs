using UnityEngine;
using UnityEngine.EventSystems;

public class FixedTouchFieldPlayer : IPointerDownHandler, IPointerUpHandler 
{
    [HideInInspector] public Vector2 _touchDist;
    [HideInInspector] public Vector2 _pointerOld; 
    [HideInInspector] protected int _pointerId;
    [HideInInspector] public bool _pressed;
    
    void Start()
    {
    }
    void Update()
    { 
        if (_pressed) 
        {
            if (_pointerId >= 0 && _pointerId < Input.touches.Length) 
            { 
                _touchDist = Input.touches[_pointerId].position - _pointerOld; 
                _pointerOld = Input.touches[_pointerId].position; } 
            else 
            { 
                _touchDist = new Vector2(Input.mousePosition.x, Input.mousePosition.y) - _pointerOld; _pointerOld = Input.mousePosition; 
            }
        } 
        else 
        { 
            _touchDist = new Vector2(); 
        } 
    }
    public void OnPointerDown(PointerEventData _eventData) 
    { 
        _pressed = true; _pointerId = _eventData.pointerId; 
        _pointerOld = _eventData.position; 
    }
    public void OnPointerUp(PointerEventData _eventData) 
    {
        _pressed = false; 
    } 
}