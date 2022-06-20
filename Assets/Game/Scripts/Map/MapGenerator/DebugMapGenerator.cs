using System.Threading.Tasks;
using UnityEngine;

namespace Game.Scripts.Map.MapGenerator
{
	public class DebugMapGenerator : MonoBehaviour
	{
		[SerializeField] private MapGenerator mapGenerator;

		[Header("Parameters"), Space(15)]
		[SerializeField, Range(.1f, 10)] private float delayBetweenSpawn;

		[ContextMenu("StartGenerate")]
		public void DebugGenerate() =>
			StartGeneratingDelayed();

		private async void StartGeneratingDelayed()
		{
			while (true)
			{
				mapGenerator.SpawnRoad();

				await Task.Delay((int)(delayBetweenSpawn * 1000));
			}
		}
	}
}