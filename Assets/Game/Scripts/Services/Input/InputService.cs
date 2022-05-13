using System;
using Additions.Enums;
using Game.Scripts.Player;

namespace Scripts.Services.Input
{
	public abstract class InputService : IInputService
	{
		protected const string Horizontal = "Horizontal";
		protected const string Vertical = "Vertical";
		private const string Button = "Fire";

		public InputService() =>
			MoveButtonMediator.OnDirectionChanged += OnOnDirectionChanged;

		public event Action OnDirectionChanged;

		public abstract Direction Axis { get; }

		public bool IsAttackButtonUp()
		{
			throw new Exception();
		}

		protected static Direction InputDirection() =>
			MoveButtonMediator.CurrentDirection;

		private void OnOnDirectionChanged() =>
			OnDirectionChanged?.Invoke();
	}
}