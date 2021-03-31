using System;

namespace Combat
{
	public interface IChangingValue
	{
		float Value { get; }
		float MaxValue { get; }
		event Action ValueChanged;
	}
}