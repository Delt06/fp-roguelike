using Levels.Generation;

namespace Levels
{
	public interface ILevelPreset
	{
		LevelTile[,] GetTiles();
		TilePosition EntryPosition { get; }
		TilePosition ExitPosition { get; }
	}
}