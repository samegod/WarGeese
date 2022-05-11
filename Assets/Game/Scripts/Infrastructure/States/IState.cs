namespace GameFiles.Scripts.Infrastructure.States
{
	public interface IState : IExitableState
	{
		void Enter();
	}

	public interface IPayLoadedState<TPayLoad> : IExitableState
	{
		void Enter(TPayLoad sceneName);
	}

	public interface IExitableState
	{
		void Exit();
	}
}