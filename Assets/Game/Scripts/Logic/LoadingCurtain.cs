using DG.Tweening;
using UnityEngine;

namespace Game.Scripts.Logic
{
	public class LoadingCurtain : MonoBehaviour
	{
		[SerializeField] private CanvasGroup curtain;

		private const float DurationOfFade = 3f;

		private void Awake()
		{
			DontDestroyOnLoad(this);
		}

		public void Show()
		{
			gameObject.SetActive(true);
			curtain.alpha = 1;
		}

		public void Hide()
		{
			curtain
				.DOFade(0, DurationOfFade)
				.onComplete = () => gameObject.SetActive(false);
		}
	}
}