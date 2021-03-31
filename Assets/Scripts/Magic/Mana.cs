using System;
using UnityEngine;

namespace Magic
{
	public sealed class Mana : MonoBehaviour, IMana
	{
		[SerializeField, Min(0f)] private float _maxValue = 100f;

		public float Value
		{
			get => _value;
			set
			{
				value = Mathf.Clamp(value, 0, MaxValue);
				if (Mathf.Approximately(_value, value)) return;
				_value = value;
				ValueChanged?.Invoke();
			}
		}

		public float MaxValue => _maxValue;

		public event Action ValueChanged;

		private void OnEnable()
		{
			Value = _maxValue;
		}

		private float _value;
	}
}