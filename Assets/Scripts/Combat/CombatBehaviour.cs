using System;
using Combat.Handlers;
using Combat.Moves;
using DELTation.Entities;
using JetBrains.Annotations;
using UnityEngine;

namespace Combat
{
	public sealed class CombatBehaviour : MonoBehaviour
	{
		public void TryStart([NotNull] IEntity unit1, [NotNull] IEntity unit2)
		{
			if (unit1 == null) throw new ArgumentNullException(nameof(unit1));
			if (unit2 == null) throw new ArgumentNullException(nameof(unit2));
			if (_state.IsValid) return;
			if (!unit1.IsAlive()) return;
			if (!unit2.IsAlive()) return;

			unit1.Get<ICombatHandler>().OnStarted(unit1, unit2);
			unit2.Get<ICombatHandler>().OnStarted(unit2, unit1);
			_state = new CombatState(unit1, unit2);
			Started?.Invoke();
		}

		public event Action Started;
		public event Action Finished;

		public bool InInProgress(out CombatState state)
		{
			if (_state.IsValid)
			{
				state = _state;
				return true;
			}

			state = default;
			return false;
		}

		private void Update()
		{
			if (_state.IsValid)
			{
				TryStartMove();
				UpdateCurrentMoveTimer();
			}
		}

		private void TryStartMove()
		{
			if (_state.MoveInProcess) return;

			var thisMoveMaker = ThisUnit.Get<ICombatMoveMaker>();
			if (!thisMoveMaker.TryMakeMove(out var move)) return;

			_moveRemainingTime = move.Duration;
			_currentMove = move;
			move.StartAction?.Perform(ThisUnit, OtherUnit);
			OnStartedMove();
		}

		private void OnStartedMove()
		{
			_state.MoveInProcess = true;
		}

		private void UpdateCurrentMoveTimer()
		{
			if (_currentMove == null) return;

			_moveRemainingTime -= Time.deltaTime;
			if (_moveRemainingTime > 0f) return;

			_currentMove.Value.FinishAction.Perform(ThisUnit, OtherUnit);
			_currentMove = null;
			OnFinishedMove();
		}

		private IEntity ThisUnit => _state.TurnIndex == 0 ? _state.Unit1 : _state.Unit2;
		private IEntity OtherUnit => _state.TurnIndex == 0 ? _state.Unit2 : _state.Unit1;

		private void OnFinishedMove()
		{
			if (!ThisUnit.IsAlive() || !OtherUnit.IsAlive())
			{
				Finish();
			}
			else
			{
				_state.MoveInProcess = false;
				_state.TurnIndex = (_state.TurnIndex + 1) % 2;
				ThisUnit.Get<ICombatMoveMaker>().OnReadyToMakeMove();
			}
		}

		private void Finish()
		{
			var unit1 = _state.Unit1;
			var unit2 = _state.Unit2;
			unit1.Get<ICombatHandler>().OnFinished(unit1, unit2);
			unit2.Get<ICombatHandler>().OnFinished(unit2, unit1);

			_state = default;
			Finished?.Invoke();
		}

		private CombatState _state;
		private float _moveRemainingTime;
		private Move? _currentMove;
	}
}