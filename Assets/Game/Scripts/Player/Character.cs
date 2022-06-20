using System;
using Additions.Enums;
using UnityEngine;

namespace Characters
{
	[RequireComponent(typeof(CharacterAnimatorController), typeof(MotionController))]
	[RequireComponent(typeof(AudioSource), typeof(CharacterController))]
	public abstract class Character : MonoBehaviour
	{
		protected MotionController MotionController;
		protected CharacterAnimatorController AnimatorController;

		private CharacterController _controller;
		
		private void Awake()
		{
			MotionController = GetComponent<MotionController>();
			AnimatorController = GetComponent<CharacterAnimatorController>();
			_controller = GetComponent<CharacterController>();
		}

		private void Start()
		{
			MotionController.Move(Direction.Forward);
		}

		private void Update()
		{
			AnimatorController.UpdateMotionAnimations(_controller.velocity.z);
		}

		public virtual void Move (Direction direction)
		{
			MotionController.Move(direction);
		}
		public virtual void Move (Transform point)
		{
			MotionController.MoveToPoint(point.position);
		}

		public virtual void Attack()
		{
			throw new NotImplementedException();
		}
		public virtual void SetAnimation ()
		{
			throw new NotImplementedException();
		}
		public virtual void Turn (Direction direction)
		{
			MotionController.Rotate(direction);
		}
		public virtual void StopMotion()
		{
			throw new NotImplementedException();
		}
		public virtual void StopTurning()
		{
			MotionController.StopRotation();
		}

		public void EnableGravity() => MotionController.EnableGravity();
		public void DisableGravity() => MotionController.DisableGravity();
	}
}