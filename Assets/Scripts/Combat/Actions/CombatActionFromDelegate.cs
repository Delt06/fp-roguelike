using System;
using DELTation.Entities;
using JetBrains.Annotations;

namespace Combat.Actions
{
	public delegate void CombatAction([NotNull] IEntity thisUnit, [NotNull] IEntity otherUnit);

	public sealed class CombatActionFromDelegate : ICombatAction
	{
		public CombatActionFromDelegate(CombatAction perform) => _perform = perform;

		public void Perform(IEntity thisUnit, IEntity otherUnit)
		{
			_perform(thisUnit, otherUnit);
		}

		private readonly CombatAction _perform;
	}
}