using System;
using JetBrains.Annotations;

namespace Levels.Generation
{
	public sealed class LevelExitGenerator
	{ 
		public LevelExitGenerator([NotNull] Random random, int minDistanceFromExitToEntry)
		{
			_random = random ?? throw new ArgumentNullException(nameof(random));
			_minDistanceFromExitToEntry = minDistanceFromExitToEntry;
		}
		
		public TilePosition GenerateExitPosition(int width, int height, TilePosition entryPosition)
		{
			var minX = entryPosition.X - _minDistanceFromExitToEntry;
			var minY = entryPosition.Y - _minDistanceFromExitToEntry;
			var maxX = entryPosition.X + _minDistanceFromExitToEntry;
			var maxY = entryPosition.Y + _minDistanceFromExitToEntry;
			var entryRect = new TileRect(minX, minY, maxX, maxY);
			entryRect.Clamp(width, height);

			return GetRandomTileExcludingRect(width, height, entryRect);
		}

		private TilePosition GetRandomTileExcludingRect(int width, int height, TileRect excludedRect)
		{
			var totalArea = width * height;
			var entryRectArea = excludedRect.Area;
			var availableTilesCount = totalArea - entryRectArea;
			if (availableTilesCount == 0) return new TilePosition(0, 0);
			var exitTileIndex = _random.Next() % availableTilesCount;

			var tileIndex = 0;

			for (var x = 0; x < width; x++)
			{
				for (var y = 0; y < height; y++)
				{
					var position = new TilePosition(x, y);
					if (excludedRect.Contains(position)) continue;

					if (tileIndex == exitTileIndex)
						return position;

					tileIndex++;
				}
			}
			
			return TilePosition.Zero;
		}
		
		private readonly Random _random;
		private readonly int _minDistanceFromExitToEntry;
	}
}