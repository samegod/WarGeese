using System;
using System.Collections.Generic;
using UnityEngine;

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
	Idle,
}

public class CharacterAnimatorController : MonoBehaviour
{
	[SerializeField] private Animator animator;
	
	private AudioSource _audioSource;

	private readonly int _speedHash = Animator.StringToHash("Speed");
	private readonly int _playSpecialHash = Animator.StringToHash("Play Special");
	private readonly int _specialIdHash = Animator.StringToHash("Special Id");
	private Action<bool> _onSpecialComplete;

	public Animator ModelAnimator => animator;
	
	private void Awake()
	{
		_audioSource = GetComponent<AudioSource>();
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
