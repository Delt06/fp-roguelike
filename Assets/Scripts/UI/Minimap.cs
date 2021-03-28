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
			_referencePointIcon.RectTransform.anchoredPosition = WorldToLocalPosition(_referencePoint.position);
		}

		public Vector2 WorldToLocalPosition(Vector3 worldPosition)
		{
			var offset = worldPosition - _referencePoint.position;
			return new Vector2(offset.x, offset.z) * _scale;
		}
	}
}