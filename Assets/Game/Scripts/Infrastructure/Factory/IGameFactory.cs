using System;
using GameFiles.Scripts.Services;
using UnityEngine;

namespace GameFiles.Scripts.Infrastructure.Factory
{
	public interface IGameFactory : IService
	{
		GameObject HeroGameObject { get; }

		GameObject CreateHero(GameObject at);

		event Action HeroCreated;

		void CreateHUD();
	}
}