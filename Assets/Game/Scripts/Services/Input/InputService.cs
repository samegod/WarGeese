using System;
using UnityEngine;

namespace Scripts.Services.Input
{
	public abstract class InputService : IInputService
	{
		protected const string Horizontal = "Horizontal";
		protected const string Vertical = "Vertical";
		private const string Button = "Fire";

		public abstract Vector2 Axis { get; }

		public bool IsAttackButtonUp()
		{
			throw new Exception();
			//return SimpleInput.GetButtonUp(Button);
		}

		protected static Vector2 SimpleInputAxis()
		{
			throw new Exception();
			//return new Vector2(SimpleInput.GetAxis(Horizontal), SimpleInput.GetAxis(Vertical));
		}
	}
}