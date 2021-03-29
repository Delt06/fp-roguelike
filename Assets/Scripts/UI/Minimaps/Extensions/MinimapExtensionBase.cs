using System;
using JetBrains.Annotations;
using UnityEngine;

namespace UI.Minimaps.Extensions
{
	public abstract class MinimapExtensionBase : MonoBehaviour, IMinimapExtension
	{
		public void Initialize([NotNull] Minimap minimap)
		{
			if (minimap == null) throw new ArgumentNullException(nameof(minimap));
			if (_initialized) return;

			Minimap = minimap;
			_initialized = true;
			OnInitialized();
		}

		protected Minimap Minimap { get; private set; }

		protected virtual void OnInitialized() { }

		private bool _initialized;
	}
}