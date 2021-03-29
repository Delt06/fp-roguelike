using Levels.Runtime;
using UnityEngine;

namespace UI.Minimaps
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

			for (var index = 0; index < _runtimeLevel.AllRooms.Count; index++)
			{
				var room = _runtimeLevel.AllRooms[index];
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