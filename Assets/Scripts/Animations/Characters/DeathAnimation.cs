using Combat;
using UnityEngine;

namespace Animations.Characters
{
	public sealed class DeathAnimation : MonoBehaviour
	{
		public void Construct(IHealth health, Animator animator)
		{
			_health = health;
			_animator = animator;
		}

		private void Update()
		{
			_animator.SetBool(IsDeadId, !_health.IsAlive);
		}

		private IHealth _health;
		private Animator _animator;

		private static readonly int IsDeadId = Animator.StringToHash("Is Dead");
	}
}