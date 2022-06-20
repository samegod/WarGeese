using UnityEngine;

namespace Game.Scripts.Logic
{
	public class CameraMoveController : MonoBehaviour
	{
		[SerializeField, Range(0, 100)] private float speed;
		[SerializeField, Range(0, 100)] private float mouseSensitivity;
		[SerializeField] private bool activeCursor;

		private float _xRotation;
		private float _yRotation;

		private const float MaxCameraAngle = 90f;
		private const float MinCameraAngle = -90f;

		private void Awake() =>
			Cursor.visible = activeCursor;

		private void Update()
		{
			if (Input.mouseScrollDelta.y != 0)
				speed += Input.mouseScrollDelta.y;
		}

		private void FixedUpdate()
		{
			transform.transform.Translate(Vector3.forward * speed * Time.deltaTime);

			float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
			float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

			_xRotation -= mouseY;
			_yRotation += mouseX;

			_xRotation = Mathf.Clamp(_xRotation, MinCameraAngle, MaxCameraAngle);
			transform.localRotation = Quaternion.Euler(_xRotation, _yRotation, 0);
		}
	}
}