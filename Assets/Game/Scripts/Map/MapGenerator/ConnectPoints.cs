using UnityEngine;

namespace Game.Scripts.Map.MapGenerator
{
	public class ConnectPoints : MonoBehaviour
	{
		[SerializeField] private Transform prevPoint;
		[SerializeField] private Transform nextPoint;

		public Vector3 PrevPointPosition => prevPoint.position;
		public Vector3 NextPointPosition => nextPoint.position;

		public Vector3 PrevPointLocalPosition => prevPoint.localPosition;
		public Vector3 NextPointLocalPosition => nextPoint.localPosition;
	}
}