using Combat.Actions;
using DELTation.Entities;
using UnityEngine;

namespace Animations.Weapons
{
	[CreateAssetMenu(menuName = AssetPath + "Play Animation")]
	public sealed class CombatActionAsset_SetTrigger : CombatActionAsset
	{
		[SerializeField] private string _triggerName = "Trigger";
		
		public override void Perform(IEntity thisUnit, IEntity otherUnit)
		{
			thisUnit.Get<Animator>().SetTrigger(_triggerName);
		}
	}
}