using UnityEngine;

namespace UI.Minimaps.Extensions
{
	[RequireComponent(typeof(MinimapIcon))]
	public sealed class MinimapReferencePointIcon : MinimapExtensionBase
	{
		private void Update()
		{
			var anchoredPosition = Minimap.WorldToLocalPosition(ReferencePosition);
			var viewAngle = GetReferenceViewAngle();
			UpdateReferenceIcon(anchoredPosition, viewAngle);
		}

		private Vector3 ReferencePosition => Minimap.ReferencePosition;

		private void UpdateReferenceIcon(Vector2 anchoredPosition, float viewAngle)
		{
			_icon.RectTransform.anchoredPosition = anchoredPosition;
			_icon.RectTransform.eulerAngles = new Vector3(0, 0, viewAngle);
		}

		private float GetReferenceViewAngle() => -Minimap.ReferenceRotation.y;

		private void Awake()
		{
			_icon = GetComponent<MinimapIcon>();
		}

		private MinimapIcon _icon;
	}
}