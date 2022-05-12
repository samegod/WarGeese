using GameFiles.Scripts.Services;
using UnityEngine;

namespace Scripts.Services.Input
{
	public interface IInputService : IService
	{
		Vector2 Axis { get; }

		bool IsAttackButtonUp();
	}
}