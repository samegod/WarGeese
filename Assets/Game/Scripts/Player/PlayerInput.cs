using System;
using Additions.Enums;
using DG.Tweening;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    #region Fields

    [SerializeField] private PlayerController player;

    #endregion

    #region Unity Methods

    private void Awake()
    {
        MoveButton.OnMouseDown += Move;
        MoveButton.OnMouseUp += StopMotion;
    }
    
    #endregion
    
    #region Private Methods

    private void Move(Direction direction)
    {
        player.Move(direction);
    }

    private void StopMotion()
    {
        player.StopMotion();
    }

    #endregion

}
