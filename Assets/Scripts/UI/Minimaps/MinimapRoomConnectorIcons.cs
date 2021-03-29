using Levels.Runtime;
using UnityEngine;

namespace UI.Minimaps
{
	[RequireComponent(typeof(PooledMinimapIconDrawer))]
	public sealed class MinimapRoomConnectorIcons : MonoBehaviour
	{
		public void Construct(RuntimeLevel runtimeLevel)
		{
			_runtimeLevel = runtimeLevel;
		}

		private void Update()
		{
			_iconDrawer.Clear();

			for (var index = 0; index < _runtimeLevel.AllRoomConnectors.Count; index++)
			{
				var connector = _runtimeLevel.AllRoomConnectors[index];
				_iconDrawer.TryDrawIcon(connector.transform.position, connector.Entity);
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