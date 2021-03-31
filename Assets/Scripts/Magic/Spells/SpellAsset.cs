using DELTation.Entities;
using UnityEngine;

namespace Magic.Spells
{
	public abstract class SpellAsset : ScriptableObject, ISpell
	{
		[SerializeField, Min(0f)] private float _cost = 10f;

		public float Cost => _cost;

		public abstract void Cast(IEntity caster, IEntity opponent);

		protected const string AssetPath = "Spell/";
	}
}