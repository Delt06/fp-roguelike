using System;
using UnityEngine;

namespace Combat.Damage
{
	public sealed class DamageDealer : MonoBehaviour, IDamageDealer
	{
		[SerializeField, Min(0f)] private float _damage = 10f;

		public void DealDamageTo(IDamageTaker damageTaker)
		{
			if (damageTaker == null) throw new ArgumentNullException(nameof(damageTaker));
			damageTaker.Take(_damage);
		}
	}
}