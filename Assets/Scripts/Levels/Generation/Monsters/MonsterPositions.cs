using System;
using System.Collections.Generic;

namespace Levels.Generation.Monsters
{
	[Flags]
	public enum MonsterPositions
	{
		South = 1,
		North = 1 << 1,
		West = 1 << 2,
		East = 1 << 3,
		Center = 1 << 4,
	}

	public enum MonsterPosition
	{
		South,
		North,
		West,
		East,
		Center,
	}

	public static class MonsterPositionsExt
	{
		public static bool Include(this MonsterPositions positions, MonsterPositions otherPositions) =>
			(positions & otherPositions) != 0;

		public static bool Include(this MonsterPositions positions, MonsterPosition otherPosition) =>
			positions.Include(otherPosition.ToPositions());

		public static MonsterPositions ToPositions(this MonsterPosition position)
		{
			return position switch
			{
				MonsterPosition.South => MonsterPositions.South,
				MonsterPosition.North => MonsterPositions.North,
				MonsterPosition.West => MonsterPositions.West,
				MonsterPosition.East => MonsterPositions.East,
				MonsterPosition.Center => MonsterPositions.Center,
				_ => throw new ArgumentOutOfRangeException(nameof(position), position, null),
			};
		}

		public static IReadOnlyList<MonsterPosition> AllPositions { get; } = new[]
		{
			MonsterPosition.Center,
			MonsterPosition.East, MonsterPosition.West,
			MonsterPosition.North, MonsterPosition.South,
		};
	}
}