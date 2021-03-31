using DELTation.Entities;
using UnityEngine;

namespace Tags
{
	[RequireComponent(typeof(IEntity))]
	public abstract class Tag : MonoBehaviour
	{
		public IEntity Entity
		{
			get
			{
				if (_entity != null) return _entity;
				_entity = GetComponentInParent<IEntity>();
				return _entity;
			}
		}

		private IEntity _entity;
	}
}