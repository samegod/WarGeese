using System;
using System.Threading.Tasks;
using Additions.Enums;
using UnityEngine;

public class StateIdle : StateBehavior
{
	private readonly int _idleHash = Animator.StringToHash("Idle");
	
	public override void StartState (Action callBack = null)
	{
		Character.Move(Direction.Back);
		Animator.SetBool(_idleHash, true);
	}
	public override void UpdateState() { }
	public override async Task FinishState()
	{
		Animator.Play(_idleHash);
	}
}
