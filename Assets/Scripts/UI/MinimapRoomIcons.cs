using FogOfWar;
using Levels.Runtime;
using UnityEngine;

namespace UI
{
	[RequireComponent(typeof(PooledMinimapIconDrawer))]
	public sealed class MinimapRoomIcons : MonoBehaviour
	{
		public void Construct(RuntimeLevel runtimeLevel)
		{
			_runtimeLevel = runtimeLevel;
		}

		private void Update()
		{
			_iconDrawer.Clear();

			foreach (var room in _runtimeLevel.AllRooms)
			{
				_iconDrawer.TryDrawIcon(room.transform.position, room.Entity);
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