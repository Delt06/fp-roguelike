using Entities;
using Levels.Runtime;
using UnityEngine;

namespace UI.Minimaps
{
	public sealed class MinimapMonstersIcons : MonoBehaviour
	{
		public void Construct(RuntimeLevel runtimeLevel)
		{
			_runtimeLevel = runtimeLevel;
		}

		private void Update()
		{
			if (!_runtimeLevel.TryGetBuilder(out RuntimeLevelBuilder_SpawnMonsters monsters)) return;

			_iconDrawer.Clear();

			for (var index = 0; index < monsters.AllMonsters.Count; index++)
			{
				var monster = monsters.AllMonsters[index];
				_iconDrawer.TryDrawIcon(monster.GetPosition(), monster);
			}
		}

		private void Awake()
		{
			_iconDrawer = GetComponent<PooledMinimapIconDrawer>();
		}

		private RuntimeLevel _runtimeLevel;
		private PooledMinimapIconDrawer _iconDrawer;
	}
}