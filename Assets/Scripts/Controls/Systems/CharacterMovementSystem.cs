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

			if (_data.Direction.HasValue)
			{
				var characterTransform = _characterController.transform;
				var forward = characterTransform.forward;
				var right = characterTransform.right;
				var direction = forward * _data.Direction.Value.y + right * _data.Direction.Value.x;
				var velocity = direction * _data.Speed;
				motion += velocity * deltaTime;
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