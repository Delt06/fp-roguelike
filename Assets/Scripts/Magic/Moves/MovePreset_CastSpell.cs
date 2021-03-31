using System;
using Combat.Actions;
using Combat.Moves;
using Combat.Moves.Presets;
using DELTation.Entities;
using Magic.Actions;
using Magic.Spells;
using UnityEngine;

namespace Magic.Moves
{
	[CreateAssetMenu(menuName = AssetPath + "Cast Spell")]
	public sealed class MovePreset_CastSpell : MovePreset
	{
		[SerializeField] private CombatActionAsset[] _startActions = default;
		[SerializeField] private SpellAsset _spell = default;
		[SerializeField, Min(0f)] private float _duration = 1f;

		public override bool IsApplicable(IEntity entity)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));
			return entity.Get<ISpellCaster>().CanCast(_spell);
		}

		public override Move GetMove(IEntity entity)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));
			
			var finishAction = _spell.ToCombatAction();

			if (_startActions.Length == 0)
				return new Move(_duration, finishAction);

			var startAction = CreateStartAction();
			return new Move(_duration, startAction, finishAction);
		}

		private CombatActionFromDelegate CreateStartAction() =>
			new CombatActionFromDelegate((unit, otherUnit) =>
				{
					foreach (var action in _startActions)
					{
						action.Perform(unit, otherUnit);
					}
				}
			);
	}
}