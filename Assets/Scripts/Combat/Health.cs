using System;
using UnityEngine;
using UnityEngine.Events;

namespace Combat
{
	public sealed class Health : MonoBehaviour, IModifiableHealth
	{
		[SerializeField, Min(0f)] private float _maxValue = 100f;
		[SerializeField] private UnityEvent _onDied;

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
					_onDied.Invoke();
				}

				_value = Mathf.Clamp(_value, 0f, _maxValue);
				ValueChanged?.Invoke();
			}
		}

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