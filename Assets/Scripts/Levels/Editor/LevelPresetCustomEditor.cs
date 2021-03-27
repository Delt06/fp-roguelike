using System;
using UnityEditor;
using UnityEngine;

namespace Levels.Editor
{
	[CustomEditor(typeof(LevelPreset))]
	public class LevelPresetCustomEditor : UnityEditor.Editor
	{
		private const int TileSize = 50;
		private const int Margin = 5;
		private const int WallSize = 10;

		private GUILayoutOption _tileWidth;
		private GUILayoutOption _tileHeight;
		private RectOffset _zeroOffset;
		private RectOffset _margin;
		private RectOffset _southWallMargin;
		private GUILayoutOption _wallWidth;
		private GUILayoutOption _wallHeight;

		private Texture2D _tileTexture;
		private GUIStyle _tileStyle;

		private Texture2D _noRoomTileTexture;
		private GUIStyle _noRoomTileStyle;

		private Texture2D _entryTileTexture;
		private GUIStyle _entryTileStyle;

		private Texture2D _exitTileTexture;
		private GUIStyle _exitTileStyle;

		private Texture2D _wallTexture;
		private GUIStyle _wallStyle;
		private GUIStyle _southWallStyle;

		private void OnEnable()
		{
			_tileWidth = GUILayout.Width(TileSize);
			_tileHeight = GUILayout.Height(TileSize);
			_wallWidth = GUILayout.Width(WallSize);
			_wallHeight = GUILayout.Height(WallSize);
			_zeroOffset = new RectOffset(0, 0, 0, 0);
			_margin = new RectOffset(Margin, Margin, Margin, Margin);
			_southWallMargin = new RectOffset(_margin.left, _margin.right, _margin.top + TileSize - WallSize,
				_margin.bottom
			);

			_noRoomTileTexture = CreateSolidColorTexture(Color.black);
			_tileTexture = CreateSolidColorTexture(Color.grey);
			_entryTileTexture = CreateSolidColorTexture(Color.white);
			_exitTileTexture = CreateSolidColorTexture(Color.green);

			_wallTexture = CreateSolidColorTexture(new Color(0.5f, 0.25f, 0.25f));
		}

		private static Texture2D CreateSolidColorTexture(Color color)
		{
			var texture = new Texture2D(1, 1);
			texture.SetPixel(0, 0, color);
			texture.Apply();
			return texture;
		}

		public override void OnInspectorGUI()
		{
			_tileStyle = new GUIStyle(GUI.skin.box)
			{
				normal = { background = _tileTexture }, margin = _margin, padding = _zeroOffset, border = _zeroOffset,
			};
			_noRoomTileStyle = new GUIStyle(_tileStyle) { normal = { background = _noRoomTileTexture } };
			_entryTileStyle = new GUIStyle(_tileStyle) { normal = { background = _entryTileTexture } };
			_exitTileStyle = new GUIStyle(_tileStyle) { normal = { background = _exitTileTexture } };
			_wallStyle = new GUIStyle(_tileStyle) { normal = { background = _wallTexture } };
			_southWallStyle = new GUIStyle(_wallStyle) { margin = _southWallMargin };

			base.OnInspectorGUI();
			serializedObject.Update();

			var preset = (LevelPreset) target;

			var tiles = preset.GetTiles();
			var width = tiles.GetWidth();
			var height = tiles.GetHeight();

			for (var y = height - 1; y >= 0; y--)
			{
				GUILayout.BeginHorizontal();

				for (var x = 0; x < width; x++)
				{
					var tile = tiles[x, y];
					DrawTile(tile);
				}

				GUILayout.EndHorizontal();
			}
		}

		private void DrawTile(LevelTile tile)
		{
			var style = GetStyleFor(tile);
			DrawRect(style, _tileWidth, _tileHeight);
			if (tile.Type == LevelTileType.None) return;

			if (!tile.HasEastDoor)
			{
				GUILayout.Space(-WallSize - Margin);
				DrawRect(_wallStyle, _wallWidth, _tileHeight);
			}

			if (!tile.HasWestDoor)
			{
				TileSpaceBack();
				DrawRect(_wallStyle, _wallWidth, _tileHeight);
				GUILayout.Space(TileSize - WallSize);
			}

			if (!tile.HasNorthDoor)
			{
				TileSpaceBack();
				DrawRect(_wallStyle, _tileWidth, _wallHeight);
			}

			if (!tile.HasSouthDoor)
			{
				TileSpaceBack();
				DrawRect(_southWallStyle, _tileWidth, _wallHeight);
			}
		}

		private static void DrawRect(GUIStyle style, params GUILayoutOption[] options)
		{
			GUILayout.Box(string.Empty, style, options);
		}

		private static void TileSpaceBack()
		{
			GUILayout.Space(-TileSize - Margin);
		}

		private GUIStyle GetStyleFor(LevelTile tile) =>
			tile.Type switch
			{
				LevelTileType.None => _noRoomTileStyle,
				LevelTileType.Normal => _tileStyle,
				LevelTileType.Entry => _entryTileStyle,
				LevelTileType.Exit => _exitTileStyle,
				_ => throw new ArgumentOutOfRangeException(),
			};
	}
}