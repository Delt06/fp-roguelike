using System.Collections.Generic;
using UnityEngine;

namespace Levels.Runtime
{
	public sealed class RuntimeLevelBuilder_PlacePlayer : MonoBehaviour, IRuntimeLevelBuilder
	{
		[SerializeField] private Transform _player = default;
		
		public void Build(in RuntimeLevelData data)
		{
			var entryPosition = data.LevelPreset.EntryPosition;
			var entryRoom = data.Rooms[entryPosition.X, entryPosition.Y];
			_player.position = entryRoom.SpawnPoint.position;
			
			var entryTile = data.Tiles[entryPosition.X, entryPosition.Y];
			SetLookDirection(entryTile);
		}

		private void SetLookDirection(LevelTile entryTile)
		{
			_directions.Clear();

			if (entryTile.HasNorthDoor)
				_directions.Add(Vector3.forward);
			if (entryTile.HasSouthDoor)
				_directions.Add(Vector3.back);
			if (entryTile.HasEastDoor)
				_directions.Add(Vector3.right);
			if (entryTile.HasWestDoor)
				_directions.Add(Vector3.left);

			if (_directions.Count == 0) return;

			var direction = _directions[Random.Range(0, _directions.Count)];
			_player.forward = direction;
		}

		private readonly List<Vector3> _directions = new List<Vector3>();
	}
}