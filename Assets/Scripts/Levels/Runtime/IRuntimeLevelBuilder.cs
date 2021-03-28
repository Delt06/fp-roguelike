using UnityEngine;

namespace Levels.Runtime
{
	public interface IRuntimeLevelBuilder
	{
		void Build(Transform root);
	}
}