using System.Collections.Generic;
using System.Linq;
using Additions.Enums.Extension;
using UnityEditor;
using UnityEngine;

namespace Game.Scripts.Enemy.PathGenerator
{
	public class PathController : MonoBehaviour
	{
		[SerializeField] private List<PathRoad> pathRoads;

		private PathRoad _startPoint;
		private List<List<PathRoad>> _paths = new List<List<PathRoad>>() {new List<PathRoad>()};

		private void Start()
		{
			pathRoads.ForEach(road => road.InitNeighbors());
			InitializeStartPoint();
			InitPaths();
		}

		private void InitializeStartPoint() =>
			_startPoint = pathRoads.Where(road => road.NeighborsRoad.Count == 1).ToList().GetRandomElement();

		private void InitPaths()
		{
			PathRoad prevRoad = _startPoint;
			PathRoad currentRoad = _startPoint;
			PathRoad nextRoad;

			int currentPathNumber = 0;

			for (int i = 0;
			     i < pathRoads.Count && TakeNextRoad(currentRoad, prevRoad) != null;
			     i++)
			{
				nextRoad = TakeNextRoad(currentRoad, prevRoad);
				_paths[currentPathNumber]
					.Add(nextRoad);

				prevRoad = currentRoad;
				currentRoad = nextRoad;
			}

			PathRoad TakeNextRoad(PathRoad current, PathRoad prev) =>
				current.NeighborsRoad.FirstOrDefault(neighbor => neighbor != current && neighbor != prev);
		}

		private void OnDrawGizmos()
		{
			if (_startPoint != null)
			{
				Gizmos.color = Color.red;
				Gizmos.DrawCube(_startPoint.transform.position, Vector3.one);
			}

			if (_paths[0].Count != 0)
			{
				Gizmos.color = Color.blue;
				for (int i = 0; i < _paths[0].Count; i++)
					Gizmos.DrawCube(_paths[0][i].transform.position, Vector3.one);
			}
		}

		#region Editor

#if UNITY_EDITOR

		public void FillRoads()
		{
			pathRoads = new List<PathRoad>();
			for (int i = 0; i < transform.childCount; i++)
			{
				var pathRoad = transform.GetChild(i).GetComponent<PathRoad>();
				if (pathRoad)
					pathRoads.Add(pathRoad);
			}
		}

#endif

		#endregion
	}

	[CustomEditor(typeof(PathController))]
	public class PathControllerEditor : Editor
	{
		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();

			var pathController = (PathController) target;
			if (GUILayout.Button("Fill Roads"))
				pathController.FillRoads();
		}
	}
}