using Combat.Moves.Presets;
using DELTation.Entities;
using UnityEngine;

namespace Combat.Moves
{
	public sealed class RandomMoveMaker : MonoBehaviour, ICombatMoveMaker
	{
		[SerializeField] private MovePreset[] _movePresets = default;

		public void Construct(IEntity entity)
		{
			_entity = entity;
		}

		public bool TryMakeMove(out Move move)
		{
			if (_movePresets.Length == 0)
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

		private IEntity _entity;
	}
}