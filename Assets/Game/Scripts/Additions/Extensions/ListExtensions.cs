using System.Collections.Generic;

namespace Additions.Enums.Extension
{
	public static class ListExtensions
	{
		public static void ShuffleThis<T>(this IList<T> list)
		{
			int count = list.Count;
			if (count < 2)
				return;

			for (; count > 0; --count)
			{
				int index = UnityEngine.Random.Range(0, count);
				(list[count - 1], list[index]) = (list[index], list[count - 1]);
			}
		}

		public static T GetRandomElement<T>(this IList<T> list) => list[UnityEngine.Random.Range(0, list.Count)];
	}
}