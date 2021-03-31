using System;

namespace Levels.Generation.Monsters
{
	[Serializable]
	public struct MonsterData
	{
		public MonsterPositions Positions;

		public void Add(MonsterPosition position)
		{
			Positions |= position.ToPositions();
		}
	}
}