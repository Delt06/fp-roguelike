using Controls;
using UnityEngine;

namespace UI
{
	public sealed class JoystickMovementInputProvider : MonoBehaviour, IMovementInputProvider
	{
		[SerializeField] private Joystick _joystick = default;

		public Vector2? Input => _joystick.Direction.sqrMagnitude >= DeadZone ? _joystick.Direction : (Vector2?) null;
		
		public void Hide() => JoystickSetActive(false);

		public void Show() => JoystickSetActive(true);

		private void JoystickSetActive(bool active) => _joystick.gameObject.SetActive(active);

		private float DeadZone => _joystick.DeadZone * _joystick.DeadZone;
	}
}