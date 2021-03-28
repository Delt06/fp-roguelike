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
				_iconDrawer.DrawIcon(room.transform.position);
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