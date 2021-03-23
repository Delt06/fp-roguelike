using Combat.Actions;
using DELTation.Entities;
using UnityEngine;

namespace Combat.Moves.Presets
{
	[CreateAssetMenu(menuName = AssetPath + "Skip")]
	public sealed class MovePreset_Skip : MovePreset
	{
		[SerializeField, Min(0f)] private float _delay = 0.5f;
		
		public override Move GetMove(IEntity entity)
		{
			var emptyAction = new CombatActionFromDelegate((unit, otherUnit) => {});
			return new Move(_delay, emptyAction);
		}
	}
}