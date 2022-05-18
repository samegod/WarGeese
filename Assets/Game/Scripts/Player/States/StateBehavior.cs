using System;
using Characters;
using UnityEngine;

public abstract class StateBehavior
{
	protected Action CallBack;
	protected Animator Animator;
	protected Character Character;
	
	public StateBehavior(Animator animator)
	{
		Animator = animator;
	}
	
	public abstract void StartState (Action callBack = null);
	public virtual void UpdateState()
	{
	}
	public abstract void StateEnd();
	
}
