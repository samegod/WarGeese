using System;
using System.Collections.Generic;
using Game.Scripts.Logic;
using UnityEngine;

namespace Game.Scripts.Map.MapGenerator
{
	public class TileMap : MonoBehaviour
	{
		[SerializeField] private TypeOfTile typeOfTile;
		[SerializeField] private ConnectPoints connectPoints;
		[SerializeField] private TriggerObserver triggerObserve;
		[SerializeField] private List<Point> roadPoints;
		[SerializeField] private int index;

		public ConnectPoints ConnectPoints => connectPoints;

		public int Index => index;

		public TypeOfTile TypeOfTile => typeOfTile;

		public static event Action OnCharacterEntered;

		private void OnEnable()
		{
			if(triggerObserve != null)
				triggerObserve.TriggerEnter += CharacterEntered;
		}

		private void OnDisable()
		{
			if(triggerObserve != null)
				triggerObserve.TriggerEnter -= CharacterEntered;
		}

		public void SetPosition(Vector3 position) =>
			transform.position = position;

		public void SetRotation(Vector3 rotation) =>
			transform.localRotation = Quaternion.Euler(rotation);

		public void TurnOverXTile() =>
			transform.localScale = Vector3.Scale(transform.localScale, new Vector3(-1, 1, 1));

		public void TurnOverZTile() =>
			transform.localScale = Vector3.Scale(transform.localScale, new Vector3(1, 1, -1));

		private void CharacterEntered(Collider obj)
		{
			triggerObserve.Disable();
			OnCharacterEntered?.Invoke();
		}
	}

	public enum TypeOfTile
	{
		Forward,
		Angle,
	}
}