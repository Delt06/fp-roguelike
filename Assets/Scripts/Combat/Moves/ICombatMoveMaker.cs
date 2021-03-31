namespace Combat.Moves
{
	public interface ICombatMoveMaker
	{
		void OnReadyToMakeMove();
		bool TryMakeMove(out Move move);
	}
}