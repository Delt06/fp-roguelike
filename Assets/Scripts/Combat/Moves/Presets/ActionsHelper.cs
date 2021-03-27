using Combat.Actions;
using Combat.Damage;
using UnityEngine;

namespace Combat.Moves.Presets
{
	public sealed class ActionsHelper : MonoBehaviour
	{
		public static ICombatAction GetAttackAnimationAction(float duration)
		{
			return new CombatActionFromDelegate((@this, other) =>
				{
					var animator = @this.Get<Animator>();
					animator.SetFloat(AttackSpeedId, 1f / duration);
					animator.SetTrigger(AttackId);
				}
			);
		}

		private static readonly int AttackSpeedId = Animator.StringToHash("Attack Speed");
		private static readonly int AttackId = Animator.StringToHash("Attack");

		public static ICombatAction DealDamageAction { get; } = new CombatActionFromDelegate((@this, other) =>
			{
				var damageDealer = @this.Get<IDamageDealer>();
				var damageTaker = other.Get<IDamageTaker>();
				damageDealer.DealDamageTo(damageTaker);
			}
		);

		public static ICombatAction NullAction { get; } = new CombatActionFromDelegate((@this, other) => { });
	}
}