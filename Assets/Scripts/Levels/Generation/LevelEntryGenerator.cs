using System;
using JetBrains.Annotations;

namespace Levels.Generation
{
	public sealed class LevelEntryGenerator
	{
		public LevelEntryGenerator([NotNull] Random random, int maxEntryPadding)
		{
			_random = random ?? throw new ArgumentNullException(nameof(random));
			_maxEntryPadding = maxEntryPadding;
		}

		public TilePosition GenerateEntryPosition(int width, int height)
		{
			var corner = GetRandomCorner();
			var rect = GetCornerRect(corner, width, height, _maxEntryPadding);
			var entryPosition = GetRandomPositionIn(rect);
			return entryPosition;
		}
		
		private LevelCorner GetRandomCorner()
		{
			var index = _random.Next() % 4;
			return (LevelCorner) index;
		}

		private TilePosition GetRandomPositionIn(TileRect rect)
		{
			var x = rect.Min.Y + _random.Next() % rect.Width;
			var y = rect.Min.Y + _random.Next() % rect.Height;
			return new TilePosition(x, y);
		}
		
		private static TileRect GetCornerRect(LevelCorner corner, int width, int height, int size)
		{
			int minX, maxX, minY, maxY;

			if (corner.IsSouth())
			{
				minY = 0;
				maxY = size - 1;
			}
			else
			{
				minY = height - size;
				maxY = height - 1;
			}

			if (corner.IsEast())
			{
				minX = 0;
				maxX = size - 1;
			}
			else
			{
				minX = width - size;
				maxX = width - 1;
			}

			var rect = new TileRect(minX, minY, maxX, maxY);
			rect.Clamp(width, height);
			return rect;
		} 
		
		private readonly Random _random;
		private readonly int _maxEntryPadding;
	}
}