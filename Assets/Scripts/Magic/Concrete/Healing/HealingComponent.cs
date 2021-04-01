using System;
using Combat;
using UnityEngine;

namespace Magic.Concrete.Healing
{
	public sealed class HealingComponent : MonoBehaviour, ISpellComponent
	{
		public void Construct(Health health)
		{
			_health = health;
		}

		public void Heal(float amount)
		{
			if (amount < 0) throw new ArgumentOutOfRangeException(nameof(amount));
			_health.Value += amount;
			Used?.Invoke();
		}

		public event Action Used;

		private Health _health;
	}
}