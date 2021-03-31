using System;
using Combat;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Bars
{
	public sealed class HealthBar : MonoBehaviour
	{
		[SerializeField] private Image _fill = default;
		
		[CanBeNull]
		public IHealth Health
		{
			get => _health;
			set
			{
				if (_health == value) return;
				
				TryUnsubscribe();
				_health = value;
				TrySubscribe();
				Refresh();
			}
		}

		private void TrySubscribe()
		{
			if (_health != null)
				_health.ValueChanged += _onValueChanged;
		}

		private void TryUnsubscribe()
		{
			if (_health != null)
				_health.ValueChanged -= _onValueChanged;
		}

		private void OnEnable()
		{
			Refresh();
			TryUnsubscribe();
			TrySubscribe();
		}

		private void OnDisable()
		{
			TryUnsubscribe();
		}

		private void Awake()
		{
			_onValueChanged = Refresh;
		}

		private void Refresh()
		{
			var fillAmount = GetFillAmount();
			_fill.fillAmount = fillAmount;
		}

		private float GetFillAmount()
		{
			return Health == null ? 0f : Mathf.Clamp01(Health.Value / Health.MaxValue);
		}

		[CanBeNull] private IHealth _health;
		
		private Action _onValueChanged;
	}
}