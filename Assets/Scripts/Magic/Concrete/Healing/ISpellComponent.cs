using System;

namespace Magic.Concrete.Healing
{
	public interface ISpellComponent
	{
		public event Action Used;
	}
}