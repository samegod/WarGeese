using GameFiles.Scripts.Infrastructure.States;
using GameFiles.Scripts.Services;
using Scripts.Logic;

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