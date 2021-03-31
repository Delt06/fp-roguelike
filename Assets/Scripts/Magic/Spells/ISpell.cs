using DELTation.Entities;
using JetBrains.Annotations;

namespace Magic.Spells
{
	public interface ISpell
	{
		float Cost { get; }
		void Cast([NotNull] IEntity caster, [NotNull] IEntity opponent);
	}
}