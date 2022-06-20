using System;
using Characters;
using UnityEngine;

public abstract class StateBehavior
{
	protected Action CallBack;
	protected Animator Animator;
	protected Character Character;
	
	public StateBehavior(Animator animator, Character character)
	{
		Animator = animator;
		Character = character;
	}
	
	public abstract void StartState (Action callBack = null);
	public virtual void UpdateState()
	{
	}
	public abstract void StateEnd();
	
}
