using UnityEngine;

namespace Levels
{
	public class RuntimeLevel : MonoBehaviour
	{
		[SerializeField] private LevelPreset _preset = default;
		[SerializeField] private Vector2 _roomsOffset = Vector3.one;
		[SerializeField] private Room _baseRoomPrefab = default;
		[SerializeField] private GameObject _wallPrefab = default;
		[SerializeField] private GameObject _doorPrefab = default;
		[SerializeField] private GameObject _connectorPrefab = default;

		private void Start()
		{
			var tiles = _preset.GetTiles();
			var width = tiles.GetWidth();
			var height = tiles.GetHeight();
			SpawnRooms(tiles, width, height);
			SpawnRoomConnectors(tiles, width, height);
		}

		private void SpawnRooms(LevelTile[,] tiles, int width, int height)
		{
			for (var tx = 0; tx < width; tx++)
			{
				for (var ty = 0; ty < height; ty++)
				{
					var tile = tiles[tx, ty];
					if (tile.Type == LevelTileType.None) continue;

					var position = GetPosition(tx, ty);
					SpawnRoom(tile, position);
				}
			}
		}

		private Vector3 GetPosition(int tx, int ty) => new Vector3(_roomsOffset.x * tx, 0f, _roomsOffset.y * ty);

		private void SpawnRoom(LevelTile tile, Vector3 position)
		{
			var room = Instantiate(_baseRoomPrefab, position, Quaternion.identity, transform);
			Instantiate(tile.HasNorthDoor ? _doorPrefab : _wallPrefab, room.NorthDoor);
			Instantiate(tile.HasSouthDoor ? _doorPrefab : _wallPrefab, room.SouthDoor);
			Instantiate(tile.HasEastDoor ? _doorPrefab : _wallPrefab, room.EastDoor);
			Instantiate(tile.HasWestDoor ? _doorPrefab : _wallPrefab, room.WestDoor);
		}

		private void SpawnRoomConnectors(LevelTile[,] tiles, int width, int height)
		{
			for (var tx = 0; tx < width; tx++)
			{
				for (var ty = 0; ty < height; ty++)
				{
					var tile = tiles[tx, ty];
					if (tile.Type == LevelTileType.None) continue;

					if (tile.HasNorthDoor)
					{
						var position = GetPosition(tx, ty);
						position.z += _roomsOffset.y * 0.5f;
						Instantiate(_connectorPrefab, position, Quaternion.identity, transform);
					}

					if (tile.HasEastDoor)
					{
						var position = GetPosition(tx, ty);
						position.x += _roomsOffset.x * 0.5f;
						Instantiate(_connectorPrefab, position, Quaternion.Euler(0f, 90f, 0f), transform);
					}
				}
			}
		}
	}
}