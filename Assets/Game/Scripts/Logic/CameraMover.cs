using UnityEngine;

namespace Game.Scripts.Logic
{
	public class CameraMover : MonoBehaviour
	{
		[SerializeField] private float speedX;
		[SerializeField] private float speedY;
		[SerializeField] private float speedZ;

		private Camera _camera;

		private void Awake() =>
			_camera = Camera.main;

		private void Update() =>
			_camera.transform.Translate(new Vector3(speedX , speedY, speedZ) * Time.deltaTime);
	}
}