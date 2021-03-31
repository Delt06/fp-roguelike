using System;
using Combat;
using Events;
using UnityEngine;
using UnityEngine.Events;

namespace Magic.Concrete.Healing
{
	public sealed class HealingComponent : MonoBehaviour
	{
		[SerializeField] private SimpleUnityEvent _onHealed = default;
		
		public void Construct(Health health)
		{
			_health = health;
		}

		public void Heal(float amount)
		{
			if (amount < 0) throw new ArgumentOutOfRangeException(nameof(amount));
			_health.Value += amount;
			_onHealed?.Invoke();
		}

		private Health _health;
	}
}