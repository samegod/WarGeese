using System;
using System.Threading.Tasks;
using Additions.Enums;
using UnityEngine;

public class StateFly : StateBehavior
{
	private readonly int _flightHash = Animator.StringToHash("Flight");

	private const float fluyUpTime = 1f;

	public override void StartState (Action callBack = null)
	{
		Animator.SetBool(_flightHash, true);
	}
	public override void UpdateState()
	{
		Character.Move(Direction.Forward);
	}
	public override async Task FinishState()
	{
		Animator.Play(_flightHash);
	}
}
