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
		
		public void Construct(HealthBar healthBar, CombatBehaviour combatBehaviour)
		{
			_combatBehaviour = combatBehaviour;
			_healthBar = healthBar;
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
				_healthBar.Health = opponent.Get<IHealth>();
				_name.text = opponent.Get<INamed>().Name;
				_contents.SetActive(true);
			}
			else
			{
				_healthBar.Health = null;
				_contents.SetActive(false);
			}
		}

		private Action _refresh;
		private CombatBehaviour _combatBehaviour;
		private HealthBar _healthBar;
	}
}