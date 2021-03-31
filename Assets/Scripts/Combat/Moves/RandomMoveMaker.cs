using Combat.Moves.Presets;
using DELTation.Entities;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Combat.Moves
{
	public sealed class RandomMoveMaker : MonoBehaviour, ICombatMoveMaker
	{
		[SerializeField] private MovePreset[] _movePresets = default;
		[SerializeField, Min(0f)] private float _delay = 0.5f;

		public void Construct(IEntity entity)
		{
			_entity = entity;
		}

		public void OnReadyToMakeMove()
		{
			_remainingTime = _delay;
		}

		public bool TryMakeMove(out Move move)
		{
			if (_remainingTime > 0f || _movePresets.Length == 0)
			{
				move = default;
				return false;
			}

			move = GetRandomMove();
			return true;
		}

		private Move GetRandomMove()
		{
			var index = Random.Range(0, _movePresets.Length);
			var preset = _movePresets[index];
			return preset.GetMove(_entity);
		}

		private void Update()
		{
			_remainingTime -= Time.deltaTime;
		}

		private IEntity _entity;
		private float _remainingTime;
	}
}