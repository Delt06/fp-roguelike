using UnityEngine;

namespace UI.Minimaps.Extensions
{
	public sealed class MinimapExtension_Culling : MinimapExtensionBase, IMinimapDrawCondition
	{
		public bool IsMet(in MinimapDrawArgs args)
		{
			var rect = new Rect
			{
				size = args.Icon.Size,
				center = args.LocalPosition,
			};
			return _mapRect.Overlaps(rect);
		}

		protected override void OnInitialized()
		{
			base.OnInitialized();

			_mapRect = Minimap.GetComponent<RectTransform>().rect;
			_mapRect.center = Vector2.zero;
		}

		private Rect _mapRect;
	}
}