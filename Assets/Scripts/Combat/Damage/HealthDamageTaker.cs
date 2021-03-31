using UnityEngine;

namespace Combat.Damage
{
	public sealed class HealthDamageTaker : MonoBehaviour, IDamageTaker
	{
		public void Construct(IModifiableHealth health)
		{
			_health = health;
		}

		public void Take(float damage)
		{
			_health.Value -= damage;
		}

		private IModifiableHealth _health;
	}
}