using DELTation.Entities;
using Levels.Generation.Monsters;
using UnityEngine;
using static Levels.Generation.Monsters.MonsterPositionsExt;

namespace Levels.Runtime
{
	public sealed class RuntimeLevelBuilder_SpawnMonsters : MonoBehaviour, IRuntimeLevelBuilder
	{
		[SerializeField] private EntityBase _monsterPrefab = default;

		public void Build(in RuntimeLevelData data)
		{
			var width = data.Tiles.GetWidth();
			var height = data.Tiles.GetHeight();

			for (var tx = 0; tx < width; tx++)
			{
				for (var ty = 0; ty < height; ty++)
				{
					var tile = data.Tiles[tx, ty];
					if (tile.Type == LevelTileType.None) continue;

					var room = data.Rooms[tx, ty];
					TrySpawn(data.Root, tile, room);
				}
			}
		}

		private void TrySpawn(Transform root, LevelTile tile, Room room)
		{
			foreach (var position in AllPositions)
			{
				TrySpawnAtPosition(root, tile, room, position);
			}
		}

		private void TrySpawnAtPosition(Transform root, LevelTile tile, Room room, MonsterPosition position)
		{
			if (!tile.MonsterData.Positions.Include(position)) return;
			if (!room.TryGetMonsterSpawnPoint(position, out var spawnPoint)) return;

			var spawnPosition = spawnPoint.position;
			var spawnRotation = spawnPoint.rotation;

			if (position == MonsterPosition.Center)
				spawnRotation = GetRandomRotationAroundY();

			Instantiate(_monsterPrefab, spawnPosition, spawnRotation, root);
		}

		private static Quaternion GetRandomRotationAroundY()
		{
			var angle = Random.Range(0f, 360f);
			return Quaternion.AngleAxis(angle, Vector3.up);
		}
	}
}