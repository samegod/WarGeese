using System.Collections.Generic;
using Additions.Enums.Extension;
using UnityEngine;

namespace Game.Scripts.Map.MapGenerator
{
	public class TilesContainer : MonoBehaviour
	{
		[SerializeField] private List<TileMap> tilesMap;
		[SerializeField] private List<TileMap> startTilesMap;

		public TileMap TakeRandomTile(TileMap prevTile)
		{
			TileMap tileMap = tilesMap.GetRandomElement();
			if (prevTile == null)
				return Instantiate(tileMap, transform);

			while (tileMap.Index == prevTile.Index)
				tileMap = tilesMap.GetRandomElement();

			return Instantiate(tileMap, transform);
		}

		public TileMap TakeRandomStartTile() =>
			Instantiate(startTilesMap.GetRandomElement(), transform);
	}
}