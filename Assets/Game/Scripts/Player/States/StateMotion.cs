using System;
using Characters;
using UnityEngine;

public class StateMotion : StateBehavior
{

	public StateMotion(Animator animator, Character character) : base(animator, character)
	{
	}
	public override void StartState (Action callBack = null)
	{
	}

	public override void UpdateState()
	{
		base.UpdateState();
		
		
	}

	public override void StateEnd()
	{
	}
}
