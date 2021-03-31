using System;
using Combat;
using Entities;
using TMPro;
using UnityEngine;

namespace UI.Bars
{
	public sealed class OpponentIndicators : MonoBehaviour
	{
		[SerializeField] private GameObject _contents = default;
		[SerializeField] private TMP_Text _name = default;

		public void Construct(Bar bar, CombatBehaviour combatBehaviour)
		{
			_combatBehaviour = combatBehaviour;
			_bar = bar;
		}

		private void OnEnable()
		{
			_combatBehaviour.Started += _refresh;
			_combatBehaviour.Finished += _refresh;
		}

		private void OnDisable()
		{
			_combatBehaviour.Started -= _refresh;
			_combatBehaviour.Finished -= _refresh;
		}

		private void Awake()
		{
			_contents.SetActive(false);
			_refresh = Refresh;
		}

		private void Refresh()
		{
			if (_combatBehaviour.InInProgress(out var state))
			{
				var opponent = state.Unit2;
				_bar.ChangingValue = opponent.Get<IHealth>();
				_name.text = opponent.Get<INamed>().Name;
				_contents.SetActive(true);
			}
			else
			{
				_bar.ChangingValue = null;
				_contents.SetActive(false);
			}
		}

		private Action _refresh;
		private CombatBehaviour _combatBehaviour;
		private Bar _bar;
	}
}