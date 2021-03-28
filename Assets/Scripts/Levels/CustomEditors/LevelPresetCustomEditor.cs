using System.Collections.Generic;
using Levels.CustomEditors.Extensions;
using UnityEditor;
using UnityEngine;

namespace Levels.CustomEditors
{
	[CustomEditor(typeof(LevelPreset))]
	public sealed class LevelPresetCustomEditor : LevelPresetCustomEditorBase
	{
		protected override int TileSize => 50;
		protected override int Margin => 5;

		protected override IEnumerable<ILevelPresetEditorExtension> Extensions { get; } =
			new ILevelPresetEditorExtension[]
			{
				new LevelPresetEditorExtension_Tiles(new Dictionary<LevelTileType, Color>
					{
						{ LevelTileType.None, Color.black },
						{ LevelTileType.Normal, Color.grey },
						{ LevelTileType.Entry, Color.white },
						{ LevelTileType.Exit, Color.green },
					}
				),
				new LevelPresetEditorExtension_Walls(10, new Color(0.5f, 0.25f, 0.5f)),
				new LevelPresetEditorExtension_Monsters(5, Color.red),
			};
	}
}