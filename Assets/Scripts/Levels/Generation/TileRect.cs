using UnityEngine;

namespace Levels.Generation
{
	public struct TileRect
	{
		public TilePosition Min;
		public TilePosition Max;

		public int Width => Max.X - Min.X + 1;
		public int Height => Max.Y - Min.Y + 1;
		public int Area => Width * Height;

		public TileRect(TilePosition min, TilePosition max)
		{
			Min = min;
			Max = max;
		}

		public TileRect(int minX, int minY, int maxX, int maxY)
		{
			Min = new TilePosition(minX, minY);
			Max = new TilePosition(maxX, maxY);
		}

		public bool Contains(TilePosition position) =>
			Min.X <= position.X && position.X <= Max.X &&
			Min.Y <= position.Y && position.Y <= Max.Y;

		public void Clamp(int width, int height)
		{
			Clamp(ref Min.X, 0, width - 1);
			Clamp(ref Min.Y, 0, height - 1);
			Clamp(ref Max.X, 0, width - 1);
			Clamp(ref Max.Y, 0, height - 1);
		}

		private static void Clamp(ref int value, int min, int max)
		{
			value = Mathf.Clamp(value, min, max);
		}
	}
}