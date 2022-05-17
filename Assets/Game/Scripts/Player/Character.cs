using System;
using Additions.Enums;
using CodeBase.Enemy;
using UnityEngine;

namespace Characters
{
	public abstract class Character : MonoBehaviour
	{
		[SerializeField] protected MotionController motionController;
		[SerializeField] protected virtual CharacterAnimator animator
		{
			get;
		}

		private void Start()
		{
			motionController.Move(Direction.Forward);
		}

		public virtual void Move (Direction direction)
		{
			motionController.Move(direction);
		}
		public virtual void Move (Transform point)
		{
			motionController.MoveToPoint(point.position);
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
			motionController.Rotate(direction);
		}
		public virtual void StopMotion()
		{
			throw new NotImplementedException();
		}
		public virtual void StopTurning()
		{
			motionController.StopRotation();
		}
	}
}