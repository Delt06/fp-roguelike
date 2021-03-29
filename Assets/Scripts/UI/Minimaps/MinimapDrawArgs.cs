using UnityEngine;
using DELTation.Entities;
using JetBrains.Annotations;

namespace UI.Minimaps
{
	public readonly struct MinimapDrawArgs
	{
		public readonly MinimapIcon Icon;
		public readonly Vector2 LocalPosition;

		[CanBeNull] public readonly IEntity ReferenceEntity;

		public MinimapDrawArgs(MinimapIcon icon, Vector2 localPosition,
			[CanBeNull] IEntity referenceEntity = null)
		{
			Icon = icon;
			LocalPosition = localPosition;
			ReferenceEntity = referenceEntity;
		}
	}
}