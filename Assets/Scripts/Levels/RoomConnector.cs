using System;
using DELTation.Entities;
using UnityEngine;

namespace Levels
{
	[RequireComponent(typeof(IEntity))]
	public sealed class RoomConnector : MonoBehaviour
	{
		public IEntity Entity { get; private set; }

		private void Awake()
		{
			Entity = GetComponent<IEntity>();
		}
	}
}