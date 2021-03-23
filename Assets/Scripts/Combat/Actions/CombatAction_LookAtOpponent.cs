using Controls;
using DELTation.Entities;
using DG.Tweening;
using Entities;
using UnityEngine;

namespace Combat.Actions
{
	[CreateAssetMenu(menuName = AssetPath + "Look At Opponent")]
	public sealed class CombatAction_LookAtOpponent : CombatActionAsset
	{
		[SerializeField] private float _lookSnapDuration = 1f;
		[SerializeField] private Ease _lookEase = Ease.Unset;
		
		public override void Perform(IEntity thisUnit, IEntity otherUnit)
		{
			var rotatedTransform = thisUnit.Get<IRotatedTransform>();
			var rotation = GetLookRotation(thisUnit, otherUnit);
			rotatedTransform.Transform.DORotateQuaternion(rotation, _lookSnapDuration)
				.SetEase(_lookEase);
		}

		private static Quaternion GetLookRotation(IEntity thisEntity, IEntity otherEntity)
		{
			var thisPosition = thisEntity.GetPosition();
			var otherPosition = otherEntity.GetPosition();
			var offset = otherPosition - thisPosition;
			offset.y = 0f;
			var direction = offset.normalized;
			var rotation = Quaternion.LookRotation(direction, Vector3.up);
			return rotation;
		}
	}
}