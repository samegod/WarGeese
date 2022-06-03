using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Scripts.Map.MapGenerator
{
	public class MapGenerator : MonoBehaviour
	{
		[SerializeField] private TilesContainer tilesContainer;
		[SerializeField] private int countOfInitTiles;

		private TileMap _currentTile;
		private TileMap _prevTile;

		private void OnEnable() =>
			TileMap.OnCharacterEntered += SpawnRandomTile;

		private void OnDisable() =>
			TileMap.OnCharacterEntered -= SpawnRandomTile;

		private void Start() =>
			InitStartTiles();

		private void InitStartTiles()
		{
			InitFirstTile();
			for (int i = 0; i < countOfInitTiles - 1; i++)
				SpawnRandomTile();
		}

		private void SpawnRandomTile()
		{
			_currentTile = tilesContainer.TakeRandomTile(_prevTile);
			if (Random.Range(0, 2) == 0)
				_currentTile.ExpandTile();

			ConnectTile(_currentTile);
			_prevTile = _currentTile;
		}

		private void InitFirstTile()
		{
			_currentTile = tilesContainer.TakeRandomStartTile();
			_currentTile.SetPosition(Vector3.zero);
			_prevTile = _currentTile;
		}

		private void ConnectTile(TileMap tile)
		{
			float difference = tile.transform.position.x - tile.ConnectPoints.PrevPointPosition.x;
			tile.SetPosition(_prevTile.ConnectPoints.NextPointPosition + new Vector3(difference, 0f, 0f));
		}
	}
}