using Combat;
using Tags;
using UnityEngine;

namespace UI.Bars
{
	public sealed class PlayerHealthBar : MonoBehaviour
	{
		public void Construct(HealthBar healthBar, PlayerTag playerTag)
		{
			_healthBar = healthBar;
			_playerTag = playerTag;
		}

		private void Start()
		{
			_healthBar.Health = _playerTag.Entity.Get<IHealth>();
		}

		private HealthBar _healthBar;
		private PlayerTag _playerTag;
	}
}