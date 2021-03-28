using UnityEditor;
using UnityEngine;

namespace Levels.CustomEditors.Extensions
{
	public sealed class LevelPresetEditorExtension_Walls : ILevelPresetEditorExtension
	{
		public LevelPresetEditorExtension_Walls(int wallSize, Color wallColor)
		{
			_wallSize = wallSize;
			_wallColor = wallColor;
		}

		private readonly int _wallSize;
		private readonly Color _wallColor;

		public void Draw(LevelTile tile, Rect tileRect)
		{
			if (tile.Type == LevelTileType.None) return;

			if (!tile.HasEastDoor)
			{
				var rect = tileRect;
				rect.min += new Vector2(tileRect.width - _wallSize, 0f);
				DrawWall(rect);
			}

			if (!tile.HasWestDoor)
			{
				var rect = tileRect;
				rect.max -= new Vector2(tileRect.width - _wallSize, 0f);
				DrawWall(rect);
			}

			if (!tile.HasNorthDoor)
			{
				var rect = tileRect;
				rect.max -= new Vector2(0f, tileRect.height - _wallSize);
				DrawWall(rect);
			}

			if (!tile.HasSouthDoor)
			{
				var rect = tileRect;
				rect.min += new Vector2(0f, tileRect.height - _wallSize);
				DrawWall(rect);
			}
		}

		private void DrawWall(Rect rect)
		{
			EditorGUI.DrawRect(rect, _wallColor);
		}
	}
}