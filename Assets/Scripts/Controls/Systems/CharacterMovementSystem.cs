using Controls.Data;
using UnityEngine;

namespace Controls.Systems
{
	public sealed class CharacterMovementSystem : MonoBehaviour
	{
		public void Construct(CharacterControlsData data, CharacterController characterController)
		{
			_data = data;
			_characterController = characterController;
		}

		private void Update()
		{
			var deltaTime = Time.deltaTime;
			_velocity += Physics.gravity * deltaTime;

			var motion = _velocity * deltaTime;

			if (_data.Move)
			{
				var forward = _characterController.transform.forward;
				motion += _data.Speed * deltaTime * forward;
			}

			_characterController.Move(motion);

			if (_characterController.isGrounded)
				_velocity.y = 0f;
		}

		private Vector3 _velocity;
		private CharacterControlsData _data;
		private CharacterController _characterController;
	}
}