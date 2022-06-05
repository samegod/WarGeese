using UnityEngine;

namespace Game.Scripts.Utils
{
	public static class RandomUtil
	{
		public static bool BoolRandom() =>
			Random.Range(0, 2) == 0;
	}
}