using UnityEngine;

namespace Levels
{
	public sealed class Room : MonoBehaviour
	{
		[SerializeField] private Transform _spawnPoint = default;
		[Header("Doors")]
		[SerializeField] private Transform _northDoor = default;
		[SerializeField] private Transform _southDoor = default;
		[SerializeField] private Transform _westDoor = default;
		[SerializeField] private Transform _eastDoor = default;

		public Transform SpawnPoint => _spawnPoint;

		public Transform NorthDoor => _northDoor;

		public Transform SouthDoor => _southDoor;

		public Transform WestDoor => _westDoor;

		public Transform EastDoor => _eastDoor;
	}
}