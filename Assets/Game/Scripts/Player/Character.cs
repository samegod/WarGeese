using System;
using Additions.Enums;
using UnityEngine;

namespace Characters
{
	public abstract class Character : MonoBehaviour
	{
		[SerializeField] protected MotionController motionController;

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
			throw new System.NotImplementedException();
		}
		public virtual void SetAnimation ()
		{
			throw new System.NotImplementedException();
		}
		public virtual void Turn (Direction direction)
		{
			motionController.Rotate(direction);
		}
		public virtual void StopMotion()
		{
			throw new System.NotImplementedException();
		}
		public virtual void StopTurning()
		{
			motionController.StopRotation();
		}
	}
}