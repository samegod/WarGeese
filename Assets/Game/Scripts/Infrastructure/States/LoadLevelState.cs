using Game.Scripts.Logic;
using GameFiles.Scripts.Infrastructure.Factory;
using Scripts.Infrastructure;
using UnityEngine;

namespace GameFiles.Scripts.Infrastructure.States
{
	public class LoadLevelState : IPayLoadedState<string>
	{
		private const string InitialPointTag = "InitialPoint";

		private readonly GameStateMachine _stateMachine;
		private readonly SceneLoader _sceneLoader;
		private readonly LoadingCurtain _loadingCurtain;
		private readonly IGameFactory _gameFactory;

		public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain,
			IGameFactory gameFactory)
		{
			_stateMachine = stateMachine;
			_sceneLoader = sceneLoader;
			_loadingCurtain = loadingCurtain;
			_gameFactory = gameFactory;
		}

		public void Enter(string sceneName)
		{
			_loadingCurtain.Show();
			_sceneLoader.Load(sceneName, OnLoaded);
		}

		public void Exit() =>
			_loadingCurtain.Hide();

		private void OnLoaded()
		{
			InitGameWorld();

			_stateMachine.Enter<GameLoopState>();
		}

		private void InitGameWorld()
		{
			//var character = _gameFactory.CreateHero(at: GameObject.FindWithTag(InitialPointTag));
			//_gameFactory.CreateHUD();

			//CameraFollow(character);
		}

		private void CameraFollow(GameObject character)
		{
			/*Camera.main
				.GetComponent<CameraController>()
				.Follow(character.transform);*/
		}
	}
}