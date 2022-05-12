using GameFiles.Scripts.Infrastructure.Factory;
using GameFiles.Scripts.Services;
using Scripts.Infrastructure;
using Scripts.Infrastructure.AssetManagement;
using Scripts.Services.Input;
using UnityEngine;

namespace GameFiles.Scripts.Infrastructure.States
{
	public class BootstrapState : IState
	{
		private const string Initial = "InitScene";

		private readonly GameStateMachine _stateMachine;
		private readonly SceneLoader _sceneLoader;
		private readonly AllServices _services;

		public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, AllServices services)
		{
			_stateMachine = stateMachine;
			_sceneLoader = sceneLoader;
			_services = services;

			RegisterServices();
		}

		public void Enter()
		{
			_sceneLoader.Load(Initial, onLoaded: EnterLoadLevel);
		}

		public void Exit()
		{
		}

		private void EnterLoadLevel() =>
			_stateMachine.Enter<LoadLevelState, string>("Game");

		private void RegisterServices()
		{
			_services.RegisterSingle<IInputService>(InputService());
			_services.RegisterSingle<IAssets>(new AssetProvider());
			_services.RegisterSingle<IGameFactory>(new GameFactory(_services.Single<IAssets>()));
		}

		private static IInputService InputService()
		{
			if (Application.isEditor)
				return new StandaloneInputService();
			else
				return new MobileInputService();
		}
	}
}