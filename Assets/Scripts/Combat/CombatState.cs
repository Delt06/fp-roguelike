using DELTation.Entities;

namespace Combat
{
	public struct CombatState
	{
		public readonly bool IsValid;
		public readonly IEntity Unit1;
		public readonly IEntity Unit2;
		public int TurnIndex;
		public bool MoveInProcess;

		public CombatState(IEntity unit1, IEntity unit2)
		{
			IsValid = true;
			Unit1 = unit1;
			Unit2 = unit2;
			TurnIndex = 0;
			MoveInProcess = false;
		}
	}
}