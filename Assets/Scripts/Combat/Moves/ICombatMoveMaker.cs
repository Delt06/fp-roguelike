namespace Combat.Moves
{
	public interface ICombatMoveMaker
	{
		bool TryMakeMove(out Move move);
	}
}