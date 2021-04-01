using System;

namespace Combat
{
	public interface IHealth : IChangingValue
	{
		bool IsAlive { get; }
		event Action Died;
	}
}