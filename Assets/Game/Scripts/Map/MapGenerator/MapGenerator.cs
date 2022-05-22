using System.Collections.Generic;
using Additions.Enums.Extension;
using UnityEngine;

namespace Game.Scripts.Map.MapGenerator
{
	public class MapGenerator : MonoBehaviour
	{
		[SerializeField] private List<TileMap> tileMaps;
		[SerializeField] private int countOfInitTiles;

		private TileMap _currentTile;
		private TileMap _prevTile;

		private void Start()
		{
			InitFirstTile();
			for (int i = 0; i < countOfInitTiles - 1; i++)
			{
				_currentTile = TakeRandomTile();
				if (Random.Range(0, 2) == 0)
					_currentTile.ExpandTile();

				ConnectTile(_currentTile);
				_prevTile = _currentTile;
			}
		}

		private void InitFirstTile()
		{
			_currentTile = TakeRandomTile();
			_currentTile.SetPosition(Vector3.zero);
			_prevTile = _currentTile;
		}

		private void ConnectTile(TileMap tile)
		{
			float difference = tile.transform.position.x - tile.ConnectPoints.PrevPointPosition.x;
			tile.SetPosition(_prevTile.ConnectPoints.NextPointPosition + new Vector3(difference, 0f, 0f));
		}

		private TileMap TakeRandomTile() =>
			Instantiate(tileMaps.GetRandomElement(), transform);
	}
}