using System;
using DELTation.Entities;
using JetBrains.Annotations;
using UnityEngine;

namespace Entities
{
	public static class EntityPositionExt
	{
		public static Vector3 GetPosition([NotNull] this IEntity entity)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));
			return entity.GameObject.transform.position;
		}
	}
}