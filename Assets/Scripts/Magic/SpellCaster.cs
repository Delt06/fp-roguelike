using System;
using DELTation.Entities;
using JetBrains.Annotations;
using Magic.Spells;
using UnityEngine;

namespace Magic
{
	public interface ISpellCaster
	{
		bool CanCast([NotNull] ISpell spell);
		bool TryCast([NotNull] ISpell spell, [NotNull] IEntity opponent);
	}

	public sealed class SpellCaster : MonoBehaviour, ISpellCaster
	{
		public void Construct(IEntity owner)
		{
			_owner = owner;
		}

		public bool CanCast(ISpell spell)
		{
			if (spell == null) throw new ArgumentNullException(nameof(spell));
			
			var mana = _owner.Get<Mana>();
			return mana.Value >= spell.Cost;
		}

		public bool TryCast(ISpell spell, IEntity opponent)
		{
			if (opponent == null) throw new ArgumentNullException(nameof(opponent));
			if (!CanCast(spell)) return false;

			var mana = _owner.Get<Mana>();
			mana.Value -= spell.Cost;
			
			spell.Cast(_owner, opponent);
			return true;
		}

		private IEntity _owner;
	}
}