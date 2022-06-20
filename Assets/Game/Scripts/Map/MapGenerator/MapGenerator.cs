using Game.Scripts.Utils;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Scripts.Map.MapGenerator
{
	public class MapGenerator : MonoBehaviour
	{
		[SerializeField] private TilesContainer tilesContainer;

		[Header("Parameteres"), Space(15)]
		[SerializeField, Range(0, 10)] private int minForwardRoadsToAngle;
		[SerializeField, Range(0, 100)] private int countOfInitTiles;

		private TileDirection _currentDirection;

		private TileMap _currentTile;
		private TileMap _prevTile;
		private TileMap _lastAngle;

		private int _currentCountRoads;

		private void OnEnable() =>
			TileMap.OnCharacterEntered += SpawnRoad;

		private void OnDisable() =>
			TileMap.OnCharacterEntered -= SpawnRoad;

		private void Start() =>
			InitStartTiles();

		private void InitStartTiles()
		{
			InitStartTile();
			for (int i = 0; i < countOfInitTiles - 1; i++)
				SpawnForwardRoad(false);
		}

		public void SpawnRoad()
		{
			if (_currentCountRoads < minForwardRoadsToAngle)
			{
				SpawnForwardRoad();
			}
			else
			{
				if (RandomUtil.BoolRandom())
					SpawnForwardRoad();
				else
					SpawnAngleRoad();
			}
		}

		private void SpawnForwardRoad(bool isAddCount = true)
		{
			_currentTile = tilesContainer.TakeRandomForwardRoad(_prevTile);
			if (RandomUtil.BoolRandom())
				_currentTile.TurnOverXTile();

			ConnectTile(_currentDirection, _currentTile);
			_prevTile = _currentTile;

			if (isAddCount)
				_currentCountRoads++;
		}

		private void SpawnAngleRoad()
		{
			_currentTile = tilesContainer.TakeRandomAngleRoad(_prevTile);
			_lastAngle = _currentTile;
			if (_currentDirection == TileDirection.Vertical)
				_currentTile.TurnOverXTile();
			else if (Random.Range(0, 2) == 0)
				_currentTile.TurnOverXTile();

			if (IsScaleZNegative(_prevTile))
				_currentTile.TurnOverZTile();

			ConnectTile(_currentDirection, _currentTile);
			ChangeDirection();
			ResetCountOfRoads();

			_prevTile = _currentTile;
		}

		private void InitStartTile()
		{
			SetDirection(TileDirection.Horizontal);

			_currentTile = tilesContainer.TakeRandomStartTile();
			_currentTile.SetPosition(Vector3.zero);
			_prevTile = _currentTile;
		}

		private void ConnectTile(TileDirection tileDirection, TileMap tile)
		{
			if (tileDirection == TileDirection.Horizontal)
				ConnectHorizontal(tile);
			else
				ConnectVertical(tile);
		}

		private void ConnectVertical(TileMap tile)
		{
			if (IsScaleXNegative(_lastAngle) && _currentTile != _lastAngle)
				tile.TurnOverZTile();

			float difference = tile.transform.position.z + tile.ConnectPoints.PrevPointPosition.x;
			tile.SetRotation(new Vector3(0f, 90f, 0f));
			tile.SetPosition(_prevTile.ConnectPoints.NextPointPosition +
			                 new Vector3(0f, 0f, difference));
		}

		private void ConnectHorizontal(TileMap tile)
		{
			float difference = tile.transform.position.x - tile.ConnectPoints.PrevPointPosition.x;
			tile.SetPosition(_prevTile.ConnectPoints.NextPointPosition + new Vector3(difference, 0f, 0f));
		}

		private void SetDirection(TileDirection direction) =>
			_currentDirection = direction;

		private void ChangeDirection()
		{
			SetDirection(_currentDirection == TileDirection.Horizontal
				? TileDirection.Vertical
				: TileDirection.Horizontal);
		}

		private bool IsScaleZNegative(TileMap tileMap) =>
			tileMap.transform.localScale.z < 0;

		private bool IsScaleXNegative(TileMap tileMap) =>
			tileMap.transform.localScale.x < 0;

		private void ResetCountOfRoads() =>
			_currentCountRoads = 0;
	}

	public enum TileDirection
	{
		Horizontal,
		Vertical,
	}
}