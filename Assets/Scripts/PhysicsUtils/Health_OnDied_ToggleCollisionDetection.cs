using Combat;
using UnityEngine;

namespace PhysicsUtils
{
	public sealed class Health_OnDied_ToggleCollisionDetection : Health_OnDied_Base
	{
		[SerializeField] private Rigidbody _rigidbody = default;
		[SerializeField] private bool _enable = true;
		
		protected override void OnDied()
		{
			_rigidbody.detectCollisions = _enable;
		}
	}
}