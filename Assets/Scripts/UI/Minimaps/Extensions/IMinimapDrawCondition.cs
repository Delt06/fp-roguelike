namespace UI.Minimaps.Extensions
{
	public interface IMinimapDrawCondition
	{
		bool IsMet(in MinimapDrawArgs args);
	}
}