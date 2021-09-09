using System;
using System.Reflection;
using JetBrains.Annotations;

namespace Plugins.Editor
{
	public static class BuildUtilities
	{
		[UsedImplicitly]
		public static void EmitProjectFiles()
		{
			var T = Type.GetType("UnityEditor.SyncVS,UnityEditor");
			if (T == null)
				throw new InvalidOperationException("Type not found.");

			var syncSolution = T.GetMethod("SyncSolution", BindingFlags.Public | BindingFlags.Static);
			if (syncSolution == null)
				throw new InvalidOperationException("Method not found.");

			syncSolution.Invoke(null, null);
		}
	}
}