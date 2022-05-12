using GameFiles.Scripts.Infrastructure.States;
using Scripts.Logic;
using UnityEngine;

namespace Scripts.Infrastructure
{
	public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
	{
		[SerializeField] private LoadingCurtain loadingCurtainPrefab;

		private Game _game;

		private void Awake()
		{
			_game = new Game(this, Instantiate(loadingCurtainPrefab));
			_game.StateMachine.Enter<BootstrapState>();

			DontDestroyOnLoad(this);
		}
	}
}