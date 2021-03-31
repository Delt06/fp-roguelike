namespace Combat
{
	public interface IHealth : IChangingValue
	{
		bool IsAlive { get; }
	}
}