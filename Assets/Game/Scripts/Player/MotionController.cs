using Additions.Enums;
using Additions.Utils;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class MotionController : MonoBehaviour
{
	[SerializeField] private float speed;
	[SerializeField] private float rotateSpeed;
	[SerializeField] private float nogmalGravityKoef = 0.2f;

	private float _currentGravityKoef;
	private CharacterController _controller;
	private Vector3 _currentMoveDirection;
	private Vector3 _currentRotateDirection;

	public Vector3 CurrentMoveDirection => _currentMoveDirection;

	private void Awake()
	{
		_controller = GetComponent<CharacterController>();
	}

	private void Update()
	{
		Vector3 transformDirection = transform.TransformDirection(_currentMoveDirection) + Physics.gravity * _currentGravityKoef;
		if (transformDirection != Vector3.zero)
		{
			_controller.Move(transformDirection * (speed * Time.deltaTime));
		}

		transform.Rotate(_currentRotateDirection * (rotateSpeed * Time.deltaTime));
	}

	public void EnableGravity()
	{
		_currentGravityKoef = nogmalGravityKoef;
	}

	public void DisableGravity()
	{
		_currentGravityKoef = 0f;
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
		Vector3 angles = DirectionAxis.GetRotationAxis(direction);
		_currentRotateDirection += angles;
		_currentRotateDirection = _currentRotateDirection.normalized;
	}
}