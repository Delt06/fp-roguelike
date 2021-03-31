using DELTation.Entities;
using Magic.Spells;
using UnityEngine;

namespace Magic.Concrete.Healing
{
	[CreateAssetMenu(menuName = AssetPath + "Healing")]
	public sealed class SpellAsset_Healing : SpellAsset
	{
		[SerializeField, Min(0f)] private float _amount = 50f;
		
		public override void Cast(IEntity caster, IEntity opponent)
		{
			var healing = caster.Get<HealingComponent>();
			healing.Heal(_amount);
		}
	}
}