using System;
using Additions.Enums;
using GameFiles.Scripts.Services;

namespace Scripts.Services.Input
{
	public interface IInputService : IService
	{
		event Action OnDirectionChanged;
		
		Direction Axis { get; }

		bool IsAttackButtonUp();
	}
}