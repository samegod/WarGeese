using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public enum States
{
	Idle = 1,
	Move = 2,
	Die = 3,
	Fly = 4,
	Victory = 5,
}

public enum AnimationNames
{
	
}

public class CharacterAnimatorController : MonoBehaviour
{
	[SerializeField] private Animator animator;
	AudioSource _audioSource;

	private readonly Dictionary<States, StateBehavior> _behaviors = new Dictionary<States, StateBehavior>();
	private readonly List<States> _currentStatesList = new List<States>();

	private readonly int _speedHash = Animator.StringToHash("Speed");
	private readonly int _playSpecialHash = Animator.StringToHash("Play Special");
	private readonly int _specialIdHash = Animator.StringToHash("Special Id");
	private Action<bool> _onSpecialComplete;

	private void Awake()
	{
		_audioSource = GetComponent<AudioSource>();
	}

	public void StartState(States state, Action callBack = null)
	{
		_behaviors[state].StartState(callBack);
		_currentStatesList.Add(state);
	}

	public void UpdateStates()
	{
		foreach (States state in _currentStatesList)
		{
			_behaviors[state].UpdateState();
		}
	}

	public void FinishState (States state)
	{
		if (_currentStatesList.Contains(state))
		{
			_behaviors[state].StateEnd();
			_currentStatesList.Remove(state);
		}
	}
	
	public void UpdateMotionAnimations(float forwardSpeed)
	{
		animator.SetFloat(_speedHash, forwardSpeed);
	}
	
	public void PlaySpecialAnimation(AnimationNames animationNames, AudioClip specialAudioClip = null, Action<bool> onSpecialComplete = null)
	{
		animator.SetBool(_playSpecialHash, true);
		animator.SetInteger(_specialIdHash, (int)animationNames);

		if (specialAudioClip)
		{
			_audioSource.PlayOneShot(specialAudioClip);
		}

		_onSpecialComplete = onSpecialComplete;
	}
}
