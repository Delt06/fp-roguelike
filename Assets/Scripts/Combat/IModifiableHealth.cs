namespace Combat
{
	public interface IModifiableHealth : IHealth
	{
		new float Value { get; set; }
	}
}