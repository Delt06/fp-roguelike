using DELTation.Entities;
using UnityEngine;

namespace Combat.Moves.Presets
{
	[CreateAssetMenu(menuName = AssetPath + "Attack")]
	public sealed class MovePreset_Attack : MovePreset
	{
		[SerializeField, Min(0f)] private float _duration = 0.5f;
		[SerializeField, Min(0f)] private float _animationDuration = 0.5f;

		public override Move GetMove(IEntity entity)
		{
			var startAction = ActionsHelper.GetAttackAnimationAction(_animationDuration);
			var finishAction = ActionsHelper.DealDamageAction;
			return new Move(_duration, startAction, finishAction);
		}
	}
}