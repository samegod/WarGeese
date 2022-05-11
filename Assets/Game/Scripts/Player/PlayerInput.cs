using System;
using Additions.Enums;
using DG.Tweening;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
	[SerializeField] private PlayerController player;

	private void Awake()
	{
		MoveButton.OnMouseDown += Move;
		MoveButton.OnMouseUp += StopMotion;
	}

	private void Move (Direction direction)
	{
		player.Move(direction);
	}

	private void StopMotion()
	{
		player.StopMotion();
	}
}