using System;

namespace Levels
{
	[Serializable]
	public struct LevelTile
	{
		public LevelTileType Type;
		public bool HasNorthDoor;
		public bool HasSouthDoor;
		public bool HasEastDoor;
		public bool HasWestDoor;

		public static LevelTile Empty => new LevelTile
		{
			Type = LevelTileType.None,
		};

		public static LevelTile Room(LevelTileType type = LevelTileType.Normal) => new LevelTile { Type = type };
	}
}