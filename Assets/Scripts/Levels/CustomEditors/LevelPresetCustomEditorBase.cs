using System.Collections.Generic;
using Levels.CustomEditors.Extensions;
using UnityEditor;
using UnityEngine;

namespace Levels.CustomEditors
{
	public abstract class LevelPresetCustomEditorBase : Editor
	{
		protected abstract int TileSize { get; }
		protected abstract int Margin { get; }

		protected abstract IEnumerable<ILevelPresetEditorExtension> Extensions { get; }

		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();
			serializedObject.Update();

			var preset = (LevelPreset) target;

			var tiles = preset.GetTiles();
			var width = tiles.GetWidth();
			var height = tiles.GetHeight();

			var levelRectWidth = (TileSize + Margin) * width - Margin;
			var levelRectHeight = (TileSize + Margin) * height - Margin;
			var levelRect = GUILayoutUtility.GetRect(levelRectWidth, levelRectHeight);

			var leftTop = levelRect.min;
			leftTop.y += Margin;

			for (var y = height - 1; y >= 0; y--)
			{
				for (var x = 0; x < width; x++)
				{
					var tile = tiles[x, y];
					var rect = new Rect(leftTop, new Vector2(TileSize, TileSize));
					DrawTile(rect, tile);
					leftTop.x += TileSize + Margin;
				}

				leftTop.x = levelRect.min.x;
				leftTop.y += TileSize + Margin;
			}
		}

		private void DrawTile(Rect rect, LevelTile tile)
		{
			foreach (var extension in Extensions)
			{
				extension.Draw(tile, rect);
			}
		}
	}
}