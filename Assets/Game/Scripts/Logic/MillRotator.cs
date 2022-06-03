using UnityEngine;

namespace Game.Scripts.Logic
{
	public class MillRotator : MonoBehaviour
	{
		[SerializeField] private float rotateSpeed;

		private void Update() =>
			transform.Rotate(Vector3.forward * rotateSpeed);
	}
}