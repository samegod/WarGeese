using System.Collections;
using Additions.Enums;
using Additions.Utils;
using UnityEngine;

public class PlayerEffects : MonoBehaviour
{
	[SerializeField] private float rotationSpeed = 5f;
	[SerializeField] private float motionRotationAngle = 30f;

	private Vector3 _targetRotation;
	private Coroutine _rotationCoroutine;

	private void Update()
	{
		//transform.localRotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(_targetRotation), Time.deltaTime);
	}
	
	public void StartMoveEffect (Direction direction)
	{
		float koef = DirectionAxis.GetAxis(direction).x;

		Debug.Log("aaaaaaaaaa");
		// if (_rotationCoroutine != null)
		// 	StopCoroutine(_rotationCoroutine);
		//
		// _rotationCoroutine = StartCoroutine(Rotation(new Vector3(0, 0, motionRotationAngle * koef)));
	}

	public void SetTargetRotation (Vector3 target)
	{
		_targetRotation = target;
	}

	private IEnumerator Rotation (Vector3 targetAngles)
	{
		while (Vector3.Distance(transform.eulerAngles, targetAngles) > 0.1f)
		{
			transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(targetAngles), Time.deltaTime * rotationSpeed);
			yield return null;
		}

		yield return null;
	}
}