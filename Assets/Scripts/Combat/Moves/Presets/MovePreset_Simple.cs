using Combat.Actions;
using DELTation.Entities;
using UnityEngine;

namespace Combat.Moves.Presets
{
	[CreateAssetMenu(menuName = AssetPath + "Simple")]
	public sealed class MovePreset_Simple : MovePreset
	{
		[SerializeField, Min(0f)] private float _duration = 0.5f;
		[SerializeField] private CombatActionAsset _startAction = default;
		[SerializeField] private CombatActionAsset _finishAction = default;

		public override Move GetMove(IEntity entity) => new Move(_duration, _startAction, _finishAction);
	}
}