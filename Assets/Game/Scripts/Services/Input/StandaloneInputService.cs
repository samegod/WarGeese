using Additions.Enums;
using UnityEngine;

namespace Scripts.Services.Input
{
	public class StandaloneInputService : InputService
	{
		public override Direction Axis
		{
			get
			{
				var axis = InputDirection();

				if (axis == Direction.None)
					axis = UnityAxis();

				return axis;
			}
		}

		private static Direction UnityAxis()
		{
			if (UnityEngine.Input.GetAxis(Horizontal) == 0f)
				return Direction.None;

			return UnityEngine.Input.GetAxis(Horizontal) < 0f ? Direction.Left : Direction.Right;
		}
	}
}