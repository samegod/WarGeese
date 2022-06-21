using System;
using System.Collections.Generic;
using System.Linq;
using Additions.Enums.Extension;
using UnityEngine;

namespace Game.Scripts.Map.MapGenerator
{
	public class TilesContainer : MonoBehaviour
	{
		[SerializeField] private List<TileMap> tilesMap;
		[SerializeField] private List<TileMap> startTilesMap;

		private List<TileMap> _forwardRoads;
		private List<TileMap> _angleRoads;

		public event Action<TileMap> OnTileGenerated;
		
		private void Awake() =>
			DetermineRoads();

		public TileMap TakeRandomTile(TileMap prevTile)
		{
			TileMap tileMap = tilesMap.GetRandomElement();

			return InstantiateTile(prevTile, tileMap, tilesMap);
		}

		public TileMap TakeRandomForwardRoad(TileMap prevTile)
		{
			TileMap tileMap = _forwardRoads.GetRandomElement();

			return InstantiateTile(prevTile, tileMap, _forwardRoads);
		}

		public TileMap TakeRandomAngleRoad(TileMap prevTile)
		{
			TileMap tileMap = _angleRoads.GetRandomElement();

			return InstantiateTile(prevTile, tileMap, _angleRoads);
		}

		public TileMap TakeRandomStartTile() =>
			Instantiate(startTilesMap.GetRandomElement(), transform);

		private TileMap InstantiateTile(TileMap prevTile, TileMap tileMap, List<TileMap> tilesForReRandom)
		{
			if (prevTile == null)
				return RegisterInstantiateTile(tileMap);

			while (tileMap.Index == prevTile.Index)
				tileMap = tilesForReRandom.GetRandomElement();

			return RegisterInstantiateTile(tileMap);
		}

		private TileMap RegisterInstantiateTile(TileMap tileMap)
		{
			TileMap targetTile = Instantiate(tileMap, transform);

			OnTileGenerated?.Invoke(targetTile);

			return targetTile;
		}

		private void DetermineRoads()
		{
			_forwardRoads = tilesMap.Where(x => x.TypeOfTile == TypeOfTile.Forward).ToList();
			_angleRoads = tilesMap.Where(x => x.TypeOfTile == TypeOfTile.Angle).ToList();
		}
	}
}