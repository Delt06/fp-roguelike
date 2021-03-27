using DELTation.Entities;
using UnityEngine;

namespace Combat.Handlers
{
	public sealed class PlayerCombatHandler : CharacterCombatHandler
	{
		[SerializeField] private GameObject _controls = default;

		public override void OnStarted(IEntity thisUnit, IEntity otherUnit)
		{
			base.OnStarted(thisUnit, otherUnit);
			_controls.SetActive(false);
		}

		public override void OnFinished(IEntity thisUnit, IEntity otherUnit)
		{
			base.OnFinished(thisUnit, otherUnit);
			if (thisUnit.IsAlive())
				_controls.SetActive(true);
		}
	}
}