using Game.Scripts.Logic;
using GameFiles.Scripts.Infrastructure.States;
using GameFiles.Scripts.Services;

namespace Scripts.Infrastructure
{
	public class Game
	{
		public readonly GameStateMachine StateMachine;

		public Game(ICoroutineRunner coroutineRunner, LoadingCurtain loadingCurtain)
		{
			StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), loadingCurtain, AllServices.Container);
		}
	}
}