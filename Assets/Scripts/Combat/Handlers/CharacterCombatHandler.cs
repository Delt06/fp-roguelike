using Combat.Actions;
using Controls;
using DELTation.Entities;
using UnityEngine;

namespace Combat.Handlers
{
	public class CharacterCombatHandler : MonoBehaviour, ICombatHandler
	{
		[SerializeField] private CombatActionAsset[] _startActions = default;
		[SerializeField] private CombatActionAsset[] _finishActions = default;

		public void Construct(IRotatedTransform rotatedTransform)
		{
			_rotatedTransform = rotatedTransform;
		}

		public virtual void OnStarted(IEntity thisUnit, IEntity otherUnit)
		{
			foreach (var action in _startActions)
			{
				action.Perform(thisUnit, otherUnit);
			}
		}

		public virtual void OnFinished(IEntity thisUnit, IEntity otherUnit)
		{
			foreach (var action in _finishActions)
			{
				action.Perform(thisUnit, otherUnit);
			}
		}

		private IRotatedTransform _rotatedTransform;
	}
}