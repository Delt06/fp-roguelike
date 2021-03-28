using Controls;
using UnityEngine;

namespace UI
{
	public sealed class JoystickMovementInputProvider : MonoBehaviour, IMovementInputProvider
	{
		[SerializeField] private Joystick _joystick = default;

		public Vector2? Input => _joystick.Direction.sqrMagnitude >= DeadZone ? _joystick.Direction : (Vector2?) null;

		private float DeadZone => _joystick.DeadZone * _joystick.DeadZone;
	}
}