using UnityEngine;

namespace Levels.CustomEditors.Extensions
{
	public interface ILevelPresetEditorExtension
	{
		void Draw(LevelTile tile, Rect tileRect);
	}
}