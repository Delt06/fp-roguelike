using Combat;

namespace UI.Minimaps.Extensions
{
	public sealed class MinimapExtension_DeadCulling : MinimapExtensionBase, IMinimapDrawCondition
	{
		public bool IsMet(in MinimapDrawArgs args)
		{
			if (args.ReferenceEntity == null) return true;
			if (!args.ReferenceEntity.TryGet(out IHealth health)) return true;
			return health.IsAlive;
		}
	}
}