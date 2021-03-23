using DELTation.Entities;
using JetBrains.Annotations;

namespace Combat.Actions
{
	public interface ICombatAction
	{
		void Perform([NotNull] IEntity thisUnit, [NotNull] IEntity otherUnit);
	}
}