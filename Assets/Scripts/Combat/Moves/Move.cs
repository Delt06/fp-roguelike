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

		public Move(float duration, [NotNull] ICombatAction startAction, [NotNull] ICombatAction finishAction)
		{
			Duration = duration;
			StartAction = startAction ?? throw new ArgumentNullException(nameof(startAction));
			FinishAction = finishAction ?? throw new ArgumentNullException(nameof(finishAction));
		}

		public Move(float duration, [NotNull] ICombatAction finishAction)
		{
			Duration = duration;
			StartAction = null;
			FinishAction = finishAction ?? throw new ArgumentNullException(nameof(finishAction));
		}
	}
}