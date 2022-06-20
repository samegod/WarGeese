using DG.Tweening;
using UnityEngine;

namespace Game.Scripts.Logic
{
	public class FireImpulse : MonoBehaviour
	{
		[SerializeField] private Light pointLight;

		[Header("Parameters"), Space(15)]
		[SerializeField, Range(0, 1)] private float minValue;
		[SerializeField, Range(0, 1)] private float maxValue;
		[SerializeField] private float duration = 1f;

		private void Update()
		{
			if(pointLight.intensity <= minValue)
				DOTween.To(() => pointLight.intensity, x => pointLight.intensity = x, maxValue, duration);
			else if (pointLight.intensity >= maxValue)
				DOTween.To(() => pointLight.intensity, x => pointLight.intensity = x, minValue, duration);
		}
	}
}