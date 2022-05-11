using System;
using System.Collections.Generic;
using Additions.Enums;
using UnityEngine;

namespace Game.Scripts.Player
{
	public class MoveButtonMediator : MonoBehaviour
	{
		[SerializeField] private List<MoveButton> moveButtons;

		public static event Action OnDirectionChanged;

		public static Direction CurrentDirection { get; private set; } = Direction.None;

		private void Awake()
		{
			foreach (var x in moveButtons)
			{
				x.OnMouseUp += SetDefaultDirection;
				x.OnMouseDown += SetDirection;
			}
		}

		private void SetDirection(Direction direction)
		{
			CurrentDirection = direction;
			OnDirectionChanged?.Invoke();
		}

		private void SetDefaultDirection()
		{
			CurrentDirection = Direction.None;
			OnDirectionChanged?.Invoke();
		}
	}
}