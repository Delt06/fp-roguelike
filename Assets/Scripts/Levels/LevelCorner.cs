using System;

namespace Levels
{
	public enum LevelCorner
	{
		SouthWest, SouthEast,
		NorthWest, NorthEast,
	}

	public static class LevelCornerExt
	{
		public static bool IsNorth(this LevelCorner corner)
		{
			switch (corner)
			{
				case LevelCorner.SouthWest:
				case LevelCorner.SouthEast:
					return false;
				case LevelCorner.NorthWest:
				case LevelCorner.NorthEast:
					return true;
				default:
					throw new ArgumentOutOfRangeException(nameof(corner), corner, null);
			}
		} 
		
		public static bool IsSouth(this LevelCorner corner)
		{
			switch (corner)
			{
				case LevelCorner.SouthWest:
				case LevelCorner.SouthEast:
					return true;
				case LevelCorner.NorthWest:
				case LevelCorner.NorthEast:
					return false;
				default:
					throw new ArgumentOutOfRangeException(nameof(corner), corner, null);
			}
		} 
		
		public static bool IsEast(this LevelCorner corner)
		{
			switch (corner)
			{
				case LevelCorner.NorthEast:
				case LevelCorner.SouthEast:
					return true;
				case LevelCorner.NorthWest:
				case LevelCorner.SouthWest:
					return false;
				default:
					throw new ArgumentOutOfRangeException(nameof(corner), corner, null);
			}
		} 
		
		public static bool IsWest(this LevelCorner corner)
		{
			switch (corner)
			{
				case LevelCorner.NorthEast:
				case LevelCorner.SouthEast:
					return false;
				case LevelCorner.NorthWest:
				case LevelCorner.SouthWest:
					return true;
				default:
					throw new ArgumentOutOfRangeException(nameof(corner), corner, null);
			}
		} 
	}
}