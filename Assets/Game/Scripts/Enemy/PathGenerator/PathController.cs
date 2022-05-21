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
		private List<List<PathRoad>> _paths;

		private void Start()
		{
			pathRoads.ForEach(road => road.InitNeighbors());
			InitializeStartPoint();
		}

		private void InitializeStartPoint() =>
			_startPoint = pathRoads.Where(road => road.NeighborsRoad.Count == 1).ToList().GetRandomElement();

		private void InitPaths()
		{
			PathRoad currentRoad = _startPoint;
			PathRoad nextRoad;
			PathRoad prevRoad;

			int currentPathNumber = 0;

			/*do
			{
				_paths[currentPathNumber]
					.Add(currentRoad.NeighborsRoad.First(neighbor => neighbor != currentRoad));
			} while ();*/
		}

		private void OnDrawGizmos()
		{
			if (_startPoint == null) return;

			Gizmos.color = Color.red;
			Gizmos.DrawCube(_startPoint.transform.position, Vector3.one);
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