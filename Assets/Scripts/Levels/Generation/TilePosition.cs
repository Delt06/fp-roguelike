using System;

namespace Levels.Generation
{
	public struct TilePosition : IEquatable<TilePosition>
	{
		public int X;
		public int Y;

		public TilePosition(int x, int y)
		{
			X = x;
			Y = y;
		}

		public TilePosition Add(int x = 0, int y = 0)
		{
			var position = this;
			position.X += x;
			position.Y += y;
			return position;
		}

		public static TilePosition Zero => new TilePosition(0, 0);


		public bool Equals(TilePosition other) => X == other.X && Y == other.Y;

		public override bool Equals(object obj) => obj is TilePosition other && Equals(other);

		public override int GetHashCode()
		{
			unchecked
			{
				return (X * 397) ^ Y;
			}
		}
	}
}