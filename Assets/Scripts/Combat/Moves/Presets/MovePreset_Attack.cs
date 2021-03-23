using Combat.Actions;
using Combat.Damage;
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
			var startAction = new CombatActionFromDelegate((@this, other) =>
				{
					var animator = @this.Get<Animator>();
					animator.SetFloat(AttackSpeedId, 1f / _animationDuration);
					animator.SetTrigger(AttackId);
				}
			);
			var finishAction = new CombatActionFromDelegate((@this, other) =>
				{
					other.Get<IDamageTaker>().Take(10f);
				}
			);
			return new Move(_duration, finishAction, startAction);
		}

		private static readonly int AttackSpeedId = Animator.StringToHash("Attack Speed");
		private static readonly int AttackId = Animator.StringToHash("Attack");
	}
}