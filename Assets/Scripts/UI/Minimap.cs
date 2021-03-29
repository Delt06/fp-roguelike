using UnityEngine;

namespace UI
{
	public sealed class Minimap : MonoBehaviour
	{
		[SerializeField] private float _scale = 100f;
		[SerializeField] private Transform _referencePoint = default;
		[SerializeField] private MinimapIcon _referencePointIcon = default;

		private void Update()
		{
			var anchoredPosition = WorldToLocalPosition(ReferencePosition);
			var viewAngle = GetReferenceViewAngle();
			UpdateReferenceIcon(anchoredPosition, viewAngle);
		}

		private void UpdateReferenceIcon(Vector2 anchoredPosition, float viewAngle)
		{
			_referencePointIcon.RectTransform.anchoredPosition = anchoredPosition;
			_referencePointIcon.RectTransform.eulerAngles = new Vector3(0, 0, viewAngle);
		}

		private float GetReferenceViewAngle()
		{
			return _referencePoint.eulerAngles.y;
		}

		public Vector2 WorldToLocalPosition(Vector3 worldPosition)
		{
			var offset = worldPosition - ReferencePosition;
			return new Vector2(offset.x, offset.z) * _scale;
		}

		private Vector3 ReferencePosition => _referencePoint.position;

		public bool IsVisible(Vector2 localPosition, Vector2 size)
		{
			var rect = new Rect()
			{
				center = localPosition,
				size = size,
			};
			return _mapRect.Overlaps(rect);
		}

		private void Awake()
		{
			_mapRect = GetComponent<RectTransform>().rect;
			_mapRect.center = Vector2.zero;
		}

		private Rect _mapRect;
	}
}