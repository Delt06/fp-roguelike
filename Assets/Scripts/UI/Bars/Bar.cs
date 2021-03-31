using System;
using Combat;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Bars
{
	public sealed class Bar : MonoBehaviour
	{
		[SerializeField] private Image _fill = default;

		[CanBeNull]
		public IChangingValue ChangingValue
		{
			get => _changingValue;
			set
			{
				if (_changingValue == value) return;

				TryUnsubscribe();
				_changingValue = value;
				TrySubscribe();
				Refresh();
			}
		}

		private void TrySubscribe()
		{
			if (_changingValue != null)
				_changingValue.ValueChanged += _onValueChanged;
		}

		private void TryUnsubscribe()
		{
			if (_changingValue != null)
				_changingValue.ValueChanged -= _onValueChanged;
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

		private float GetFillAmount() => ChangingValue == null ? 0f : Mathf.Clamp01(ChangingValue.Value / ChangingValue.MaxValue);

		[CanBeNull] private IChangingValue _changingValue;

		private Action _onValueChanged;
	}
}