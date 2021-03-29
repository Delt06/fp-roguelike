using Levels.Runtime;
using UnityEngine;

namespace UI
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

			foreach (var connector in _runtimeLevel.AllRoomConnectors)
			{
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