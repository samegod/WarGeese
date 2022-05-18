using System;
using Game.Scripts.Animator;
using UnityEngine;

namespace CodeBase.Enemy
{
	public class CharacterAnimator : MonoBehaviour, IAnimationStateReader
	{
		private static readonly int Attack = Animator.StringToHash("Attack");
		private static readonly int Speed = Animator.StringToHash("Speed");
		private static readonly int IsMoving = Animator.StringToHash("IsMoving");
		private static readonly int Victory = Animator.StringToHash("Victory");
		private static readonly int Die = Animator.StringToHash("Die");

		private readonly int _idleStateHash = Animator.StringToHash("Idle");
		private readonly int _attackStateHash = Animator.StringToHash("Attack");
		private readonly int _walkingStateHash = Animator.StringToHash("Move");
		private readonly int _victoryStateHash = Animator.StringToHash("Victory");
		private readonly int _deathStateHash = Animator.StringToHash("Dead");

		private Animator _animator;                   

		public event Action<AnimatorState> StateEntered;  
		public event Action<AnimatorState> StateExited;

		public AnimatorState State { get; private set; }

		private void Awake() =>
			_animator = GetComponent<Animator>();

		public void Move(float speed)
		{
			_animator.SetBool(IsMoving, true);
			_animator.SetFloat(Speed, speed);
		}

		public void PlayDeath() => _animator.SetTrigger(Die);

		public void StopMoving() => _animator.SetBool(IsMoving, false);

		public void PlayAttack() => _animator.SetTrigger(Attack);

		public void PlayVictory() => _animator.SetTrigger(Victory);

		public void EnteredState(int stateHash)
		{
			State = StateFor(stateHash);
			StateEntered?.Invoke(State);
		}

		public void ExitedState(int stateHash) =>
			StateExited?.Invoke(StateFor(stateHash));

		private AnimatorState StateFor(int stateHash)
		{
			AnimatorState state;
			if (stateHash == _idleStateHash)
				state = AnimatorState.Idle;
			else if (stateHash == _attackStateHash)
				state = AnimatorState.Attack;
			else if (stateHash == _walkingStateHash)
				state = AnimatorState.Walking;
			else if (stateHash == _deathStateHash)
				state = AnimatorState.Died;
			else if (stateHash == _victoryStateHash)
				state = AnimatorState.Victory;
			else
				state = AnimatorState.Unknown;

			return state;
		}
	}
}