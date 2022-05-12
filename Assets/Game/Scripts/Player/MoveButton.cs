using System;
using UnityEngine;
using Additions.Enums;
using UnityEngine.EventSystems;

public class MoveButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public delegate void DirectionDelegate(Direction value);
    public static event DirectionDelegate OnMouseDown;
    public static event Action OnMouseUp;
    
    [SerializeField] private Direction direction;

    public void OnPointerDown (PointerEventData eventData)
    {
        OnMouseDown?.Invoke(direction);
    }
    public void OnPointerUp (PointerEventData eventData)
    {
        OnMouseUp?.Invoke();
    }
}
