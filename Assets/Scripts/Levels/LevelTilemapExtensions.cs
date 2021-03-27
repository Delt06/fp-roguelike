using System;
using JetBrains.Annotations;
using Levels.Generation;

namespace Levels
{
	public static class LevelTilemapExtensions
	{
		public static int GetWidth([NotNull] this LevelTile[,] tiles)
		{
			if (tiles == null) throw new ArgumentNullException(nameof(tiles));
			return tiles.GetLength(0);
		}

		public static int GetHeight([NotNull] this LevelTile[,] tiles)
		{
			if (tiles == null) throw new ArgumentNullException(nameof(tiles));
			return tiles.GetLength(1);
		}

		public static void ClearWith([NotNull] this LevelTile[,] tiles, LevelTile tile)
		{
			if (tiles == null) throw new ArgumentNullException(nameof(tiles));

			var width = tiles.GetWidth();
			var height = tiles.GetHeight();

			for (var x = 0; x < width; x++)
			{
				for (var y = 0; y < height; y++)
				{
					tiles[x, y] = tile;
				}
			}
		}

		public static bool IsInsideBounds(TilePosition position, int width, int height) =>
			0 <= position.X && position.X < width &&
			0 <= position.Y && position.Y < height;
	}
}