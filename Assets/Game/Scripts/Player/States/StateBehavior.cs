using System;
using System.Threading.Tasks;
using Characters;
using UnityEngine;

public abstract class StateBehavior
{
	protected Action CallBack;
	protected Animator Animator;
	protected Character Character;

	public StateBehavior()
	{
	}
	
	public void Initialize (Character character, Animator animator)
	{
		Character = character;
		Animator = animator;
	}

	public abstract void StartState (Action callBack = null);
	public abstract void UpdateState();
	public abstract Task FinishState();
}