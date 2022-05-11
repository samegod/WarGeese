using Additions.Enums;
using Additions.Utils;
using UnityEngine;

public class PlayerMotionController : MonoBehaviour
{
	[SerializeField] private float speed;
	[SerializeField] private float rotateSpeed;

	private Vector3 _currentMoveDirection;
	private Vector3 _currentRotateDirection;

	public Vector3 CurrentMoveDirection => _currentMoveDirection;

	private void Update()
	{
		if (CurrentMoveDirection != Vector3.zero)
		{
			transform.Translate(_currentMoveDirection * (speed * Time.deltaTime));
		}

		transform.Rotate(_currentRotateDirection * (rotateSpeed * Time.deltaTime));
		
	}

	public void Rotate (Direction direction)
	{
		AddRotateDirection(direction);
	}

	public void StopRotation()
	{
		_currentRotateDirection = Vector3.zero;
	}

	public void MoveToPoint (Vector3 position)
	{
		transform.position = Vector3.Lerp(transform.position, position, Time.deltaTime * speed);
	}

	public void Move (Direction direction)
	{
		AddMoveDirection(direction);
	}

	public void StopMotion()
	{
		_currentMoveDirection = Vector3.zero;
	}

	private void AddMoveDirection (Direction direction)
	{
		_currentMoveDirection += DirectionAxis.GetAxis(direction);
		_currentMoveDirection = _currentMoveDirection.normalized;
	}

	private void AddRotateDirection (Direction direction)
	{
		Vector3 angles = DirectionAxesToRotationAxes(direction);
		_currentRotateDirection += angles;
		_currentRotateDirection = _currentRotateDirection.normalized;
	}

	private Vector3 DirectionAxesToRotationAxes(Direction direction)
	{
		Vector3 angles;

		Vector3 inputVector = DirectionAxis.GetAxis(direction);

		angles.x = inputVector.y;
		angles.y = inputVector.x;
		angles.z = inputVector.z;

		return angles;
	}
}