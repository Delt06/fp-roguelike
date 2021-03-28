using System;
using Levels.Generation.Monsters;
using UnityEngine;

namespace Levels
{
	public sealed class Room : MonoBehaviour
	{
		[SerializeField] private Transform _spawnPoint = default;

		[Header("Doors"), SerializeField]
		
		private Transform _northDoor = default;

		[SerializeField] private Transform _southDoor = default;
		[SerializeField] private Transform _westDoor = default;
		[SerializeField] private Transform _eastDoor = default;

		[SerializeField] private MonsterSpawnPoint[] _monsterSpawnPoints = default;

		public Transform SpawnPoint => _spawnPoint;

		public Transform NorthDoor => _northDoor;

		public Transform SouthDoor => _southDoor;

		public Transform WestDoor => _westDoor;

		public Transform EastDoor => _eastDoor;

		public bool TryGetMonsterSpawnPoint(MonsterPosition position, out Transform spawnPoint)
		{
			foreach (var monsterSpawnPoint in _monsterSpawnPoints)
			{
				if (monsterSpawnPoint.Position != position) continue;
				spawnPoint = monsterSpawnPoint.SpawnPoint;
				return true;
			}

			spawnPoint = default;
			return false;
		}
		
		[Serializable]
		private struct MonsterSpawnPoint
		{
			public Transform SpawnPoint;
			public MonsterPosition Position;
		}
	}
}