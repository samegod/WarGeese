using UnityEngine;

namespace Characters
{
	public class CharacterStatesController : MonoBehaviour
	{
		private StateBehavior _currentstate;
		private Character _character;
		private Animator _animator;

		private void Update()
		{
			_currentstate?.UpdateState();
		}

		public void Init (Character character, Animator animator)
		{
			_character = character;
			_animator = animator;
		}
		
		public async void SetState<T>() where T : StateBehavior, new()
		{
			var newState = new T();
			

			if (_currentstate != null)
			{
				await _currentstate.FinishState();
			}
			
			newState.Initialize(_character, _animator);
			newState.StartState();
			_currentstate = newState;
		}

		public void FinishCurrentState()
		{
			_currentstate.FinishState();
		}
	}
}