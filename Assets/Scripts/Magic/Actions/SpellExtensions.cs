using System;
using Combat.Actions;
using JetBrains.Annotations;
using Magic.Spells;

namespace Magic.Actions
{
	public static class SpellExtensions
	{
		public static ICombatAction ToCombatAction([NotNull] this ISpell spell)
		{
			if (spell == null) throw new ArgumentNullException(nameof(spell));
			return new SpellCombatAction(spell);
		}
	}
}