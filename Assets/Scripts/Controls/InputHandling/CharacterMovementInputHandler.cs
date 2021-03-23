using System;
using Controls.Data;
using UnityEngine;

namespace Controls.InputHandling
{
	public sealed class CharacterMovementInputHandler : MonoBehaviour
	{
		public void Construct(IHoldProvider holdProvider, CharacterControlsData data)
		{
			_holdProvider = holdProvider;
			_data = data;
		}

		private void Update()
		{
			_data.Move = _holdProvider.IsHolding;
		}

		private void OnDisable()
		{
			_data.Move = false;
		}

		private IHoldProvider _holdProvider;
		private CharacterControlsData _data;
	}
}