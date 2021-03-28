using System.Collections.Generic;
using Levels.Generation.Monsters;
using UnityEditor;
using UnityEngine;

namespace Levels.CustomEditors
{
	[CustomEditor(typeof(LevelPreset))]
	public class LevelPresetCustomEditor : Editor
	{
		#region Sizes

		private const int TileSize = 50;
		private const int Margin = 5;
		private const int WallSize = 10;
		private const int MonsterSize = 5;

		#endregion

		#region Colors

		private static readonly Color WallColor = new Color(0.5f, 0.25f, 0.5f);
		private static readonly Color MonsterColor = Color.red;

		private static readonly Dictionary<LevelTileType, Color> TileColors = new Dictionary<LevelTileType, Color>()
		{
			{ LevelTileType.None, Color.black },
			{ LevelTileType.Normal, Color.grey },
			{ LevelTileType.Entry, Color.white },
			{ LevelTileType.Exit, Color.green },
		};

		#endregion

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

		private static void DrawTile(Rect rect, LevelTile tile)
		{
			var color = GetColorFor(tile);
			EditorGUI.DrawRect(rect, color);
			if (tile.Type == LevelTileType.None) return;

			DrawDoors(rect, tile);
		}

		private static void DrawDoors(Rect tileRect, LevelTile tile)
		{
			if (!tile.HasEastDoor)
			{
				var rect = tileRect;
				rect.min += new Vector2(TileSize - WallSize, 0f);
				DrawWall(rect);
			}

			if (!tile.HasWestDoor)
			{
				var rect = tileRect;
				rect.max -= new Vector2(TileSize - WallSize, 0f);
				DrawWall(rect);
			}

			if (!tile.HasNorthDoor)
			{
				var rect = tileRect;
				rect.max -= new Vector2(0f, TileSize - WallSize);
				DrawWall(rect);
			}

			if (!tile.HasSouthDoor)
			{
				var rect = tileRect;
				rect.min += new Vector2(0f, TileSize - WallSize);
				DrawWall(rect);
			}

			foreach (var position in MonsterPositionsExt.AllPositions)
			{
				if (!tile.MonsterData.Positions.Include(position)) continue;

				DrawMonster(tileRect, position);
			}
		}

		private static Color GetColorFor(LevelTile tile) =>
			TileColors.TryGetValue(tile.Type, out var color) ? color : Color.black;

		private static void DrawWall(Rect eastRect)
		{
			EditorGUI.DrawRect(eastRect, WallColor);
		}

		private static void DrawMonster(Rect tileRect, MonsterPosition position)
		{
			const float monsterHalfSize = MonsterSize * 0.5f;
			var center = position switch
			{
				MonsterPosition.South => new Vector2(tileRect.center.x, tileRect.max.y - monsterHalfSize),
				MonsterPosition.North => new Vector2(tileRect.center.x, tileRect.min.y + monsterHalfSize),
				MonsterPosition.West => new Vector2(tileRect.min.x + monsterHalfSize, tileRect.center.y),
				MonsterPosition.East => new Vector2(tileRect.max.x - monsterHalfSize, tileRect.center.y),
				MonsterPosition.Center => tileRect.center,
				_ => tileRect.center,
			};
			var size = Vector2.one * MonsterSize;

			var monsterRect = new Rect
			{
				size = size,
				center = center,
			};
			EditorGUI.DrawRect(monsterRect, MonsterColor);
		}
	}
}