using Additions.Enums;
using GameFiles.Scripts.Services;
using Scripts.Services.Input;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private PlayerController player;

    private IInputService _inputService;

    private void Awake()
    {
        _inputService = AllServices.Container.Single<IInputService>();
        _inputService.OnDirectionChanged += SetMovement;
    }

    private void SetMovement()
    {
        Debug.Log(_inputService.Axis);
        if(_inputService.Axis != Direction.None)
            Move(_inputService.Axis);
        else
            StopMotion();
    }


    private void Move(Direction direction)
    {
        player.Move(direction);
    }

    private void StopMotion()
    {
        player.StopMotion();
    }

}
