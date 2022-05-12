using System;
using UnityEngine;

namespace Scripts.Infrastructure
{
	public class GameRunner : MonoBehaviour
	{
		public GameBootstrapper GameBootstrapperPrefab;

		private void Awake()
		{
			var bootstrapper = FindObjectOfType<GameBootstrapper>();
			if (bootstrapper == null)
				Instantiate(GameBootstrapperPrefab);
		}
	}
}