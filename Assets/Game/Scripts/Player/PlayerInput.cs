using Additions.Enums;
using Characters;
using GameFiles.Scripts.Services;
using Scripts.Services.Input;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private Character player;

    private IInputService _inputService;

    private void Awake()
    {
        _inputService = AllServices.Container.Single<IInputService>();
        _inputService.OnDirectionChanged += SetMovement;
    }

    private void SetMovement()
    {
        if(_inputService.Axis != Direction.None)
            Turn(_inputService.Axis);
        else
            StopTurning();
    }
    private void Turn (Direction direction)
    {
        player.Turn(direction);
    }

    private void StopTurning()
    {
        player.StopTurning();
    }
}
