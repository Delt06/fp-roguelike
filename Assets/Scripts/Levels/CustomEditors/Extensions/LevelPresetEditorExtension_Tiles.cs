using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

namespace Levels.CustomEditors.Extensions
{
	public sealed class LevelPresetEditorExtension_Tiles : ILevelPresetEditorExtension
	{
		public LevelPresetEditorExtension_Tiles([NotNull] IReadOnlyDictionary<LevelTileType, Color> tileColors) =>
			_tileColors = tileColors ?? throw new ArgumentNullException(nameof(tileColors));

		private readonly IReadOnlyDictionary<LevelTileType, Color> _tileColors;

		public void Draw(LevelTile tile, Rect tileRect)
		{
			var color = GetColorFor(tile);
			EditorGUI.DrawRect(tileRect, color);
		}

		private Color GetColorFor(LevelTile tile) =>
			_tileColors.TryGetValue(tile.Type, out var color) ? color : Color.black;
	}
}