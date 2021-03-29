using System;
using Combat.Moves;
using Combat.Moves.Presets;
using DELTation.Entities;
using UnityEngine;

namespace UI
{
	public sealed class MoveMakingScreen : MonoBehaviour
	{
		[SerializeField] private GameObject _content = default;
		[SerializeField] private EntityBase _entity = default;

		public void OnClick(MovePreset move)
		{
			MoveMaker.PendingMove = move.GetMove(_entity);
		}

		private void OnEnable()
		{
			Refresh();
			MoveMaker.MoveDemandBegan += _refresh;
			MoveMaker.MoveDemandEnded += _refresh;
		}

		private void OnDisable()
		{
			MoveMaker.MoveDemandBegan -= _refresh;
			MoveMaker.MoveDemandEnded -= _refresh;
		}

		private void Awake()
		{
			_refresh = Refresh;
		}

		private void Refresh()
		{
			_content.SetActive(MoveMaker.DemandMove);
		}

		private ManualMoveMaker MoveMaker => _entity.Get<ManualMoveMaker>();

		private Action _refresh;
	}
}