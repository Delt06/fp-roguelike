using JetBrains.Annotations;

namespace Entities
{
	public interface INamed
	{
		[NotNull] string Name { get; }
	}
}