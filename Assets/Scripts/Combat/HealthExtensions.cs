using System;
using DELTation.Entities;
using JetBrains.Annotations;

namespace Combat
{
	public static class HealthExtensions
	{
		public static bool IsAlive([NotNull] this IEntity entity)
		{
			if (entity == null) throw new ArgumentNullException(nameof(entity));
			return entity.Get<IHealth>().IsAlive;
		}
	}
}