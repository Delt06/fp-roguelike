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
			
			Debug.Log($"{_health} took {damage}.");
			if (!_health.IsAlive)
				Debug.Log($"{_health} is dead.");
		}

		private IModifiableHealth _health;
	}
}