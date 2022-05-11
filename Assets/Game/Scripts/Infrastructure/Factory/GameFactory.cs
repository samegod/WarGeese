using System;
using Scripts.Infrastructure.AssetManagement;
using UnityEngine;

namespace GameFiles.Scripts.Infrastructure.Factory
{
	public class GameFactory : IGameFactory
	{
		private readonly IAssets _assets;

		public event Action HeroCreated;

		public GameObject HeroGameObject { get; set; }


		public GameFactory(IAssets assets)
		{
			_assets = assets;
		}

		public GameObject CreateHero(GameObject at)
		{
			HeroGameObject = InstantiateRegistered(AssetPath.CharacterPath, at.transform.position);
			HeroCreated?.Invoke();

			return HeroGameObject;
		}

		public void CreateHUD() =>
			InstantiateRegistered(AssetPath.HUDPath);

		private GameObject InstantiateRegistered(string prefabPath, Vector3 at)
		{
			GameObject gameObject = _assets.Instantiate(prefabPath, at: at);
			return gameObject;
		}

		private GameObject InstantiateRegistered(string prefabPath)
		{
			GameObject gameObject = _assets.Instantiate(prefabPath);
			return gameObject;
		}
	}
}