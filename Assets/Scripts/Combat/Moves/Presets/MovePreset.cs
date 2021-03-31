using DELTation.Entities;
using JetBrains.Annotations;
using UnityEngine;

namespace Combat.Moves.Presets
{
	public abstract class MovePreset : ScriptableObject
	{
		public virtual bool IsApplicable([NotNull] IEntity entity) => true;
		public abstract Move GetMove([NotNull] IEntity entity);

		protected const string AssetPath = "Combat/Move Preset/";
	}
}