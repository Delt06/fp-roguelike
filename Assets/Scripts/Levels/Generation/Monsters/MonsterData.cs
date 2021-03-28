namespace Levels.Generation.Monsters
{
	public struct MonsterData
	{
		public MonsterPositions Positions;

		public void Add(MonsterPosition position)
		{
			Positions |= position.ToPositions();
		}
	}
}