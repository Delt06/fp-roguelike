using System;
using UnityEngine;

namespace Combat.Moves
{
	public sealed class ManualMoveMaker : MonoBehaviour, ICombatMoveMaker
	{
		public Move? PendingMove { get; set; }

		public bool DemandMove
		{
			get => _demandMove;
			private set
			{
				if (_demandMove == value) return;
				_demandMove = value;
				if (_demandMove)
					MoveDemandBegan?.Invoke();
				else
					MoveDemandEnded?.Invoke();
			}
		}

		public event Action MoveDemandBegan;
		public event Action MoveDemandEnded;

		public void OnReadyToMakeMove() { }

		public bool TryMakeMove(out Move move)
		{
			if (PendingMove.HasValue)
			{
				move = PendingMove.Value;
				PendingMove = null;
				DemandMove = false;
				return true;
			}

			DemandMove = true;
			move = default;
			return false;
		}

		private bool _demandMove;
	}
}