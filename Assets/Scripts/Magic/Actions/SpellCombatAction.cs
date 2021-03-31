using System;
using Combat.Actions;
using DELTation.Entities;
using JetBrains.Annotations;
using Magic.Spells;

namespace Magic.Actions
{
	public sealed class SpellCombatAction : ICombatAction
	{
		public SpellCombatAction([NotNull] ISpell spell) => _spell = spell ?? throw new ArgumentNullException(nameof(spell));

		public void Perform(IEntity thisUnit, IEntity otherUnit)
		{
			if (thisUnit == null) throw new ArgumentNullException(nameof(thisUnit));
			if (otherUnit == null) throw new ArgumentNullException(nameof(otherUnit));

			var spellCaster = thisUnit.Get<ISpellCaster>();
			spellCaster.TryCast(_spell, otherUnit);
		}

		private readonly ISpell _spell;
	}
}