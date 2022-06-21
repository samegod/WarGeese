using Game.Scripts.Map.MapGenerator;
using Game.Scripts.Map.PathGenerators;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Map
{
	public class MapGeneratorInstaller : MonoInstaller
	{
		[SerializeField] private TilesContainer tilesContainer;
		[SerializeField] private PathGenerator pathGenerator;

		public override void InstallBindings()
		{
			Container.Bind<TilesContainer>().FromInstance(tilesContainer).AsSingle().NonLazy();
			Container.Bind<PathGenerator>().FromInstance(pathGenerator).AsSingle().NonLazy();
		}
	}
}