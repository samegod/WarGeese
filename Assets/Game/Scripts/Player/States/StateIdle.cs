using System;
using Characters;
using UnityEngine;

public class StateIdle : StateBehavior
{
	public StateIdle(Animator animator, Character character) : base(animator, character)
	{
	}
	public override void StartState (Action callBack = null)
	{
		
	}
	public override void StateEnd()
	{
	}
}
