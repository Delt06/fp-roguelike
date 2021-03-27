using JetBrains.Annotations;

namespace Combat.Damage
{
	public interface IDamageDealer
	{
		void DealDamageTo([NotNull] IDamageTaker damageTaker);
	}
}