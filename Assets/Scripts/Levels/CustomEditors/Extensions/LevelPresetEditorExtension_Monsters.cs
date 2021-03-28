using Levels.Generation.Monsters;
using UnityEditor;
using UnityEngine;

namespace Levels.CustomEditors.Extensions
{
	public sealed class LevelPresetEditorExtension_Monsters : ILevelPresetEditorExtension
	{
		public LevelPresetEditorExtension_Monsters(int monsterSize, Color monsterColor)
		{
			_monsterSize = monsterSize;
			_monsterColor = monsterColor;
		}

		private readonly int _monsterSize;
		private readonly Color _monsterColor;

		public void Draw(LevelTile tile, Rect tileRect)
		{
			if (tile.Type == LevelTileType.None) return;

			foreach (var position in MonsterPositionsExt.AllPositions)
			{
				if (!tile.MonsterData.Positions.Include(position)) continue;

				DrawMonster(tileRect, position);
			}
		}

		private void DrawMonster(Rect tileRect, MonsterPosition position)
		{
			var monsterHalfSize = _monsterSize * 0.5f;
			var center = position switch
			{
				MonsterPosition.South => new Vector2(tileRect.center.x, tileRect.max.y - monsterHalfSize),
				MonsterPosition.North => new Vector2(tileRect.center.x, tileRect.min.y + monsterHalfSize),
				MonsterPosition.West => new Vector2(tileRect.min.x + monsterHalfSize, tileRect.center.y),
				MonsterPosition.East => new Vector2(tileRect.max.x - monsterHalfSize, tileRect.center.y),
				MonsterPosition.Center => tileRect.center,
				_ => tileRect.center,
			};
			var size = Vector2.one * _monsterSize;

			var monsterRect = new Rect
			{
				size = size,
				center = center,
			};
			EditorGUI.DrawRect(monsterRect, _monsterColor);
		}
	}
}