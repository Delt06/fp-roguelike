using DELTation.Entities;
using FogOfWar;
using JetBrains.Annotations;
using UnityEngine.UI;

namespace UI.Minimaps.Extensions
{
	public sealed class MinimapExtension_FogOfWar : MinimapExtensionBase, IMinimapDrawCondition, IMinimapDrawHandler
	{
		public bool IsMet(in MinimapDrawArgs args)
		{
			if (args.ReferenceEntity == null) return true;
			if (!args.ReferenceEntity.TryGet(out FogOfWarObject fogOfWarObject)) return true;
			return fogOfWarObject.IsRevealed;
		}

		public void OnDrawn(in MinimapDrawArgs args)
		{
			ApplyFogOfWar(args.Icon.Image, args.ReferenceEntity);
		}

		private static void ApplyFogOfWar(Image image, [CanBeNull] IEntity referenceEntity)
		{
			if (referenceEntity == null) return;
			if (!referenceEntity.TryGet(out FogOfWarObject fogOfWarObject)) return;

			var color = image.color;
			color.a *= fogOfWarObject.RevealingProgress;
			image.color = color;
		}
	}
}