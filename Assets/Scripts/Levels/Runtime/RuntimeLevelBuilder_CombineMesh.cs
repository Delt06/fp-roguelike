using UnityEngine;

namespace Levels.Runtime
{
	public sealed class RuntimeLevelBuilder_CombineMesh : MonoBehaviour, IRuntimeLevelBuilder
	{
		public void Build(in RuntimeLevelData data)
		{
			StaticBatchingUtility.Combine(data.Root.gameObject);
		}
	}
}