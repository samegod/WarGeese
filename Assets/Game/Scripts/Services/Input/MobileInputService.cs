using Additions.Enums;

namespace Scripts.Services.Input
{
	public class MobileInputService : InputService
	{
		public override Direction Axis => InputDirection();
	}
}