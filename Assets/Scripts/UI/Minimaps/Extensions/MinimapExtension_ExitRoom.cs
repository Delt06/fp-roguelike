using Levels;
using UnityEngine;

namespace UI.Minimaps.Extensions
{
	public sealed class MinimapExtension_ExitRoom : MinimapExtensionBase, IMinimapDrawHandler
	{
		[SerializeField] private Color _color = Color.cyan;

		public void OnDrawn(in MinimapDrawArgs args)
		{
			if (args.ReferenceEntity == null) return;
			if (!args.ReferenceEntity.TryGet(out ExitRoomTag _)) return;

			args.Icon.Image.color = _color;
		}
	}
}