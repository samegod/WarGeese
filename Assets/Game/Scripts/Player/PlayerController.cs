using Additions.Enums;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private PlayerMotionController motionController;
	[SerializeField] private PlayerEffects playerEffects;

	public void Move (Direction direction)
	{
		motionController.Rotate(direction);
		playerEffects.StartMoveEffect(direction);
	}

	public void StopMotion()
	{
		motionController.StopRotation();
		playerEffects.StartMoveEffect(Direction.None);
	}
}