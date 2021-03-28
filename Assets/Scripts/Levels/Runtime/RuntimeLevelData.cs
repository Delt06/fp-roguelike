using UnityEngine;

namespace Levels.Runtime
{
	public struct RuntimeLevelData
	{
		public Transform Root;
		public ILevelPreset LevelPreset;
		public LevelTile[,] Tiles;
		public Room[,] Rooms;
	}
}