using System;
using Combat.Actions;
using JetBrains.Annotations;

namespace Combat.Moves
{
	public readonly struct Move
	{
		public readonly float Duration;
		[CanBeNull] public readonly ICombatAction StartAction;
		public readonly ICombatAction FinishAction;

		public Move(float duration, [NotNull] ICombatAction finishAction, [CanBeNull] ICombatAction startAction = null)
		{
			Duration = duration;
			StartAction = startAction;
			FinishAction = finishAction ?? throw new ArgumentNullException(nameof(finishAction));
		}
	}
}