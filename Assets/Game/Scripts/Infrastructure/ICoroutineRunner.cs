using System.Collections;
using UnityEngine;

namespace Scripts.Infrastructure
{
	public interface ICoroutineRunner
	{
		Coroutine StartCoroutine(IEnumerator coroutine);
	}
}