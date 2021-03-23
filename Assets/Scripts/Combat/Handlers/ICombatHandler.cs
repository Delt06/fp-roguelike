using DELTation.Entities;
using JetBrains.Annotations;

namespace Combat.Handlers
{
	public interface ICombatHandler
	{
		void OnStarted([NotNull] IEntity thisUnit, [NotNull] IEntity otherUnit);
		void OnFinished([NotNull] IEntity thisUnit, [NotNull] IEntity otherUnit);
	}
}