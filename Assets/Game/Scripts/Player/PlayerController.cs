using Additions.Enums;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerMotionController motionController;
    [SerializeField] private PlayerEffects playerEffects;

    public void Move(Direction direction)
    {
        motionController.Move(direction);
        playerEffects.StartMoveEffect(direction);
    }

    public void StopMotion()
    {
        motionController.StopMotion();
        playerEffects.StartMoveEffect(Direction.None);
    }
}
