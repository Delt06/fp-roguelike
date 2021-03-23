using UnityEngine;

namespace Combat.Moves
{
	public sealed class ManualMoveMaker : MonoBehaviour, ICombatMoveMaker
	{
		public Move? Move { get; set; }

		public bool TryMakeMove(out Move move)
		{
			if (Move.HasValue)
			{
				move = Move.Value;
				return true;
			}

			move = default;
			return false;
		}
	}
}