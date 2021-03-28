using UnityEngine;

namespace Levels.Runtime
{
	public interface IRuntimeLevelBuilder
	{
		void Build(in RuntimeLevelData data);
	}
}