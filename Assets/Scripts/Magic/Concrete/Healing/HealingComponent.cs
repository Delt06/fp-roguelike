using System;
using Combat;
using UnityEngine;

namespace Magic.Concrete.Healing
{
	public sealed class HealingComponent : MonoBehaviour
	{
		public void Construct(Health health)
		{
			_health = health;
		}

		public void Heal(float amount)
		{
			if (amount < 0) throw new ArgumentOutOfRangeException(nameof(amount));
			_health.Value += amount;
		}

		private Health _health;
	}
}