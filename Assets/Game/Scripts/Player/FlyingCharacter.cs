using Additions.Enums;
using Characters;
using UnityEngine;

public class FlyingCharacter : Character
{
    [SerializeField] private TurnEffect turnEffect;

    private bool _isGrounded = false;
    
    public override void Turn (Direction direction)
    {
        base.Turn(direction);

        if (_isGrounded == false)
        {
            if (turnEffect != null)
            {
                turnEffect.StartEffect(direction);
            }
        }
    }

    public override void StopTurning()
    {
        base.StopTurning();

        if (_isGrounded == false)
        {
            if (turnEffect != null)
            {
                turnEffect.StopEffect();
            }
        }
    }
}
