using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.Enemy.PathGenerator
{
	public class PathRoad : MonoBehaviour
	{
		[SerializeField] private TypeOfRoad typeOfRoad;
		[SerializeField] private float radiusOfRoadCheck = 2f;

		private List<PathRoad> _neighborsRoad = new List<PathRoad>();
		private Collider[] _pathRoadsColliders = new Collider[4];
		private int _layerMask;

		public IReadOnlyCollection<PathRoad> NeighborsRoad => _neighborsRoad;

		public TypeOfRoad RoadType => typeOfRoad;

		private void Awake() =>
			_layerMask = 1 << LayerMask.NameToLayer("Road");

		public void InitNeighbors()
		{
			Physics.OverlapSphereNonAlloc(transform.position, radiusOfRoadCheck, _pathRoadsColliders, _layerMask);
			foreach (var pathCollider in _pathRoadsColliders)
			{
				if (pathCollider == null) continue;

				var pathRoadComponent = pathCollider.GetComponent<PathRoad>();
				if (pathRoadComponent != null && pathRoadComponent != this)
					_neighborsRoad.Add(pathRoadComponent);
			}
		}

		private void OnDrawGizmosSelected()
		{
			Gizmos.color = new Color(0f, 1f, 0f, 0.5f);
			Gizmos.DrawSphere(transform.position, radiusOfRoadCheck);
		}
	}

	public enum TypeOfRoad
	{
		ForwardRoad,
		AngleRoad,
		TCross,
		XCross
	}
}