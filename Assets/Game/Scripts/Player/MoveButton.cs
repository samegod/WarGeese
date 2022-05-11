using System;
using UnityEngine;
using Additions.Enums;
using UnityEngine.EventSystems;

public class MoveButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
	public static event Action<Direction> OnMouseDown;
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