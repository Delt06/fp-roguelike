using System;
using Controls.Data;
using UnityEngine;

namespace Controls.InputHandling
{
	public sealed class CharacterMovementInputHandler : MonoBehaviour
	{
		[SerializeField, Min(0f)] private float _directionSmoothTime = 1f;
		[SerializeField, Min(0f)] private float _directionMaxChangeSpeed = 10f;
		[SerializeField, Min(0f)] private float _nullThreshold = 0.01f;

		public void Construct(IMovementInputProvider movementInputProvider, CharacterControlsData data)
		{
			_movementInputProvider = movementInputProvider;
			_data = data;
		}

		private void Update()
		{
			var currentDirection = MoveTowardsTargetDirection(Time.deltaTime);

			if (IsAboveNullThreshold(currentDirection))
				_data.Direction = currentDirection;
			else
				_data.Direction = null;
		}

		private Vector2 MoveTowardsTargetDirection(float deltaTime)
		{
			var targetDirection = GetTargetDirection();
			var currentDirection = GetCurrentDirection();
			currentDirection = Vector2.SmoothDamp(currentDirection, targetDirection, ref _changeVelocity,
				_directionSmoothTime, _directionMaxChangeSpeed, deltaTime
			);
			return currentDirection;
		}

		private Vector2 GetTargetDirection()
		{
			var targetDirection = _movementInputProvider.Input.GetValueOrDefault();
			if (targetDirection.sqrMagnitude >= 1f)
				targetDirection.Normalize();
			return targetDirection;
		}


		private Vector2 GetCurrentDirection() => _data.Direction.GetValueOrDefault();

		private bool IsAboveNullThreshold(Vector2 direction) =>
			direction.sqrMagnitude >= _nullThreshold * _nullThreshold;

		private void OnDisable()
		{
			_data.Direction = null;
			_changeVelocity = Vector2.zero;
		}

		private IMovementInputProvider _movementInputProvider;
		private CharacterControlsData _data;
		private Vector2 _changeVelocity;
	}
}