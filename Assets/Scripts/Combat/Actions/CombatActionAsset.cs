using DELTation.Entities;
using UnityEngine;

namespace Combat.Actions
{
	public abstract class CombatActionAsset : ScriptableObject, ICombatAction
	{
		public abstract void Perform(IEntity thisUnit, IEntity otherUnit);

		protected const string AssetPath = "Combat/Action/";
	}
}