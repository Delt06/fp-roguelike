using Combat;
using Tags;
using UnityEngine;

namespace UI.Bars
{
	public abstract class PlayerBar<T> : MonoBehaviour where T : class, IChangingValue
	{
		public void Construct(Bar bar, PlayerTag playerTag)
		{
			_bar = bar;
			_playerTag = playerTag;
		}

		protected void Start()
		{
			_bar.ChangingValue = _playerTag.Entity.Get<T>();
		}

		private Bar _bar;
		private PlayerTag _playerTag;
	}
}