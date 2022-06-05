using UnityEngine;

namespace Game.Scripts.Logic
{
	public class Point : MonoBehaviour
	{
		public Vector3 VectorPosition => transform.position;

		public Vector3 VectorLocalPosition => transform.localPosition;
	}
}