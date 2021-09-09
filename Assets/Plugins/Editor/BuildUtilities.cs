using UnityEditor;

namespace Plugins.Editor
{
	public static class BuildUtilities
	{
		public static void EmitProjectFiles()
		{
			EditorApplication.ExecuteMenuItem("Assets/Open C# Project");
		}
	}
}