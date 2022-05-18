using System;
using Characters;
using UnityEngine;

public class StateFly : StateBehavior
{
	private readonly int _flightHash = Animator.StringToHash("flight");
	
	public StateFly(Animator animator) : base(animator)
	{
	}
	
	public override void StartState (Action callBack = null)
	{
			Animator.SetBool(_flightHash, true);
	}
	public override void StateEnd()
	{
		Animator.SetBool(_flightHash, false);
	}
}
