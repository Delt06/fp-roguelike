using System.Collections.Generic;
using UnityEngine;

namespace Levels.Runtime
{
	public class RuntimeLevel : MonoBehaviour
	{
		[SerializeField] private LevelPreset _preset = default;
		[SerializeField] private Vector2 _roomsOffset = Vector3.one;
		[SerializeField] private Room _baseRoomPrefab = default;
		[SerializeField] private GameObject _wallPrefab = default;
		[SerializeField] private GameObject _doorPrefab = default;
		[SerializeField] private GameObject _connectorPrefab = default;

		public IReadOnlyList<Room> AllRooms => _allRooms;
		public IReadOnlyList<GameObject> AllRoomConnectors => _allRoomConnectors;
		

		private void Start()
		{
			var tiles = _preset.GetTiles();
			var width = tiles.GetWidth();
			var height = tiles.GetHeight();
			_roomsGrid = new Room[width, height];
			SpawnRooms(tiles, width, height);
			SpawnRoomConnectors(tiles, width, height);

			var data = new RuntimeLevelData
			{
				Root = Root,
				Tiles = tiles,
				LevelPreset = _preset,
				Rooms = _roomsGrid,
			};

			var builders = GetComponentsInChildren<IRuntimeLevelBuilder>();

			foreach (var builder in builders)
			{
				builder.Build(data);
			}
		}

		private Transform Root => transform;

		private void SpawnRooms(LevelTile[,] tiles, int width, int height)
		{
			for (var tx = 0; tx < width; tx++)
			{
				for (var ty = 0; ty < height; ty++)
				{
					var tile = tiles[tx, ty];
					if (tile.Type == LevelTileType.None) continue;

					var position = GetPosition(tx, ty);
					var room = SpawnRoom(tile, position);
					_roomsGrid[tx, ty] = room;
					_allRooms.Add(room);
				}
			}
		}

		private Vector3 GetPosition(int tx, int ty) => new Vector3(_roomsOffset.x * tx, 0f, _roomsOffset.y * ty);

		private Room SpawnRoom(LevelTile tile, Vector3 position)
		{
			var room = Instantiate(_baseRoomPrefab, position, Quaternion.identity, Root);
			Instantiate(tile.HasNorthDoor ? _doorPrefab : _wallPrefab, room.NorthDoor);
			Instantiate(tile.HasSouthDoor ? _doorPrefab : _wallPrefab, room.SouthDoor);
			Instantiate(tile.HasEastDoor ? _doorPrefab : _wallPrefab, room.EastDoor);
			Instantiate(tile.HasWestDoor ? _doorPrefab : _wallPrefab, room.WestDoor);
			return room;
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
						var connector = Instantiate(_connectorPrefab, position, Quaternion.identity, Root);
						_allRoomConnectors.Add(connector);
					}

					if (tile.HasEastDoor)
					{
						var position = GetPosition(tx, ty);
						position.x += _roomsOffset.x * 0.5f;
						var connector = Instantiate(_connectorPrefab, position, Quaternion.Euler(0f, 90f, 0f), Root);
						_allRoomConnectors.Add(connector);
					}
				}
			}
		}

		private Room[,] _roomsGrid;
		private readonly List<Room> _allRooms = new List<Room>();
		private readonly List<GameObject> _allRoomConnectors = new List<GameObject>();
	}
}