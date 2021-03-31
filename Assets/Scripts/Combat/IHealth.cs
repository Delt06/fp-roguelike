using System;

namespace Combat
{
	public interface IHealth
	{
		float Value { get; }
		float MaxValue { get; }
		bool IsAlive { get; }
		event Action ValueChanged;
	}
}