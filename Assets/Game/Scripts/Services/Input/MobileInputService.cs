using UnityEngine;

namespace Scripts.Services.Input
{
	public class MobileInputService : InputService
	{
		public override Vector2 Axis => SimpleInputAxis();
	}
}