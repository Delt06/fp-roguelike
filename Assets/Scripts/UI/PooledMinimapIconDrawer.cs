using System.Collections.Generic;
using UnityEngine;

namespace UI
{
	public sealed class PooledMinimapIconDrawer : MonoBehaviour
	{
		[SerializeField] private MinimapIcon _iconPrefab = default;

		public void Construct(Minimap minimap)
		{
			_minimap = minimap;
		}

		public void Clear()
		{
			foreach (var busyIcon in _busyIcons)
			{
				busyIcon.Image.enabled = false;
				_freeIcons.Add(busyIcon);
			}
			
			_busyIcons.Clear();
		}

		public void DrawIcon(Vector3 worldPosition)
		{
			if (_freeIcons.Count == 0)
			{
				CreateNewIcon();
			}

			var lastFreeImageIndex = _freeIcons.Count - 1;
			var icon = _freeIcons[lastFreeImageIndex];
			var position = _minimap.WorldToLocalPosition(worldPosition);
			if (!_minimap.IsVisible(position, icon.Size)) return;

			icon.RectTransform.anchoredPosition = position;
			icon.Image.enabled = true;
			_freeIcons.RemoveAt(lastFreeImageIndex);
			_busyIcons.Add(icon);
		}

		private void CreateNewIcon()
		{
			var icon = Instantiate(_iconPrefab, transform);
			icon.Image.enabled = false;
			_freeIcons.Add(icon);
		}

		private Minimap _minimap;
		private readonly List<MinimapIcon> _busyIcons = new List<MinimapIcon>();
		private readonly List<MinimapIcon> _freeIcons = new List<MinimapIcon>();
	}
}