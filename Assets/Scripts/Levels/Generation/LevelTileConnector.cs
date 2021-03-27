using System;
using JetBrains.Annotations;

namespace Levels.Generation
{
	public static class LevelTileConnector
	{
		public static void Connect([NotNull] LevelTile[,] tiles, TilePosition tile1, TilePosition tile2)
		{
			if (tiles == null) throw new ArgumentNullException(nameof(tiles));
			ConnectHorizontally(tiles, tile1, tile2);
			ConnectVertically(tiles, tile1, tile2);
		}

		private static void ConnectHorizontally(LevelTile[,] tiles, TilePosition tile1, TilePosition tile2)
		{
			var dx = tile2.X - tile1.X;
			if (dx == 0) return;
			
			if (dx > 0)
			{
				tiles[tile1.X, tile1.Y].HasEastDoor = true;
				tiles[tile2.X, tile2.Y].HasWestDoor = true;
			}
			else
			{
				tiles[tile1.X, tile1.Y].HasWestDoor = true;
				tiles[tile2.X, tile2.Y].HasEastDoor = true;
			}
		}

		private static void ConnectVertically(LevelTile[,] tiles, TilePosition tile1, TilePosition tile2)
		{
			var dy = tile2.Y - tile1.Y;
			if (dy == 0) return;
			
			if (dy > 0)
			{
				tiles[tile1.X, tile1.Y].HasNorthDoor = true;
				tiles[tile2.X, tile2.Y].HasSouthDoor = true;
			}
			else
			{
				tiles[tile1.X, tile1.Y].HasSouthDoor = true;
				tiles[tile2.X, tile2.Y].HasNorthDoor = true;
			}
		}
	}
}