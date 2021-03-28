using System;
using JetBrains.Annotations;
using UnityEngine;

namespace Levels.Generation.Monsters
{
	[Serializable]
	public struct MonsterPositionProbabilities
	{
		[Range(0f, 1f)]
		public float DoorProbability;
		[Range(0f, 1f)]
		public float CenterProbability;

		[Pure]
		public float GetProbability(MonsterPosition position) =>
			position switch
			{
				MonsterPosition.Center => CenterProbability,
				_ => DoorProbability,
			};
	}
}