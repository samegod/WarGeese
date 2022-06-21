using System.Collections.Generic;
using System.Linq;
using Game.Scripts.Logic;
using Game.Scripts.Map.MapGenerator;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Map.PathGenerators
{
	public class PathGenerator : MonoBehaviour
	{
		[Inject] private TilesContainer _tilesContainer;

		private Queue<TileMap> _currentPath = new Queue<TileMap>();

		private void OnEnable() =>
			_tilesContainer.OnTileGenerated += GeneratePath;

		private void OnDisable() =>
			_tilesContainer.OnTileGenerated -= GeneratePath;

		public Stack<Point> GetPath()
		{
			var path = new Stack<Point>();
			foreach (var points in _currentPath.Select(x => x.RoadPoints))
			{
				foreach (var point in points)
					path.Push(point);
			}

			return path;
		}

		private void GeneratePath(TileMap tile) =>
			_currentPath.Enqueue(tile);

		private void OnDrawGizmosSelected()
		{
			foreach (var points in _currentPath.Select(x => x.RoadPoints))
			{
				foreach (var point in points)
					Gizmos.DrawSphere(point.VectorPosition, 1f);
			}
		}
	}
}