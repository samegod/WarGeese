using UnityEngine;

namespace Game.Scripts.Map.MapGenerator
{
	public class TileMap : MonoBehaviour
	{
		[SerializeField] private TypeOfTile typeOfTile;
		[SerializeField] private ConnectPoints connectPoints;

		public ConnectPoints ConnectPoints => connectPoints;

		public void SetPosition(Vector3 position) =>
			transform.position = position;

		public void ExpandTile() =>
			transform.localScale = Vector3.Scale(transform.localScale, new Vector3(-1, 1, 1));
	}

	public enum TypeOfTile
	{
		Forward,
		Angle,
	}
}