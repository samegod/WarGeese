using System;
using Additions.Enums;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Direction direction;

    public event Action<Direction> OnMouseDown;
    public event Action OnMouseUp;

    public void OnPointerDown (PointerEventData eventData) =>
        OnMouseDown?.Invoke(direction);

    public void OnPointerUp (PointerEventData eventData) =>
        OnMouseUp?.Invoke();
}
