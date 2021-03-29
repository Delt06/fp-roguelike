using System.Collections.Generic;
using DELTation.Entities;
using FogOfWar;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

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

		public bool TryDrawIcon(Vector3 worldPosition, [CanBeNull] IEntity referenceEntity = null)
		{
			if (_freeIcons.Count == 0) CreateNewIcon();

			var lastFreeImageIndex = _freeIcons.Count - 1;
			var icon = _freeIcons[lastFreeImageIndex];
			var position = _minimap.WorldToLocalPosition(worldPosition);
			if (!_minimap.IsVisible(position, icon.Size))
				return false;
			if (IsNotRevealed(referenceEntity))
				return false;

			icon.RectTransform.anchoredPosition = position;
			icon.ResetColor();
			ApplyFogOfWar(icon.Image, referenceEntity);
			icon.Image.enabled = true;
			_freeIcons.RemoveAt(lastFreeImageIndex);
			_busyIcons.Add(icon);
			return true;
		}

		private static bool IsNotRevealed([CanBeNull] IEntity referenceEntity) =>
			referenceEntity != null &&
			referenceEntity.TryGet(out FogOfWarObject fogOfWarObject) &&
			!fogOfWarObject.IsRevealed;

		private static void ApplyFogOfWar(Image image, [CanBeNull] IEntity referenceEntity)
		{
			if (referenceEntity == null) return;
			if (!referenceEntity.TryGet(out FogOfWarObject fogOfWarObject)) return;

			var color = image.color;
			color.a *= fogOfWarObject.RevealingProgress;
			image.color = color;
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