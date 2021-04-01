using System;
using UnityEngine;

namespace Combat
{
	public abstract class Health_OnDied_Base : MonoBehaviour
	{
		public void Construct(IHealth health)
		{
			_health = health;
		}

		protected virtual void OnEnable()
		{
			_health.Died += _onDied;
		}

		protected virtual void OnDisable()
		{
			_health.Died -= _onDied;
		}

		protected virtual void Awake()
		{
			_onDied = OnDied;
		}

		protected abstract void OnDied();

		private IHealth _health;
		private Action _onDied;
	}
}