using UnityEngine;

namespace Levels.Runtime
{
	public sealed class RuntimeLevelBuilder_CombineMesh : MonoBehaviour, IRuntimeLevelBuilder
	{
		public void Build(Transform root)
		{
			StaticBatchingUtility.Combine(root.gameObject);
		}
	}
}