using System.Collections;
using Additions.Enums;
using Additions.Utils;
using UnityEngine;

public class TurnEffect : MonoBehaviour
{
	[SerializeField] private float rotationSpeed = 5f;
	[SerializeField] private float angle = 30f;

	private Vector3 _targetRotation;

	private void Update()
	{
		transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(_targetRotation), Time.deltaTime * rotationSpeed);
	}

	public void StartEffect (Direction direction)
	{
		Vector3 newTargetRotation = Vector3.zero;
		newTargetRotation.z = DirectionAxis.GetAxis(direction).x;

		newTargetRotation *= -angle;
		_targetRotation = newTargetRotation;
	}
	public void StopEffect()
	{
		_targetRotation = Vector3.zero;
	}
}