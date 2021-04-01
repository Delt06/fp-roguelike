using System;
using UnityEngine;

namespace Combat
{
	public sealed class Health : MonoBehaviour, IModifiableHealth
	{
		[SerializeField, Min(0f)] private float _maxValue = 100f;

		public float MaxValue => _maxValue;

		public float Value
		{
			get => IsAlive ? _value : 0f;
			set
			{
				if (!IsAlive) return;

				_value = value;
				if (_value <= 0f)
				{
					IsAlive = false;
					Died?.Invoke();
				}

				_value = Mathf.Clamp(_value, 0f, _maxValue);
				ValueChanged?.Invoke();
			}
		}

		public event Action Died;

		public bool IsAlive { get; private set; } = true;
		public event Action ValueChanged;

		private void OnEnable()
		{
			IsAlive = true;
			Value = _maxValue;
		}

		private float _value;
	}
}