using System;
using Controls;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
	public sealed class ButtonHoldProvider : MonoBehaviour, IHoldProvider, IPointerDownHandler, IPointerUpHandler,
		IPointerExitHandler
	{
		public bool IsHolding => _pointerId.HasValue;

		void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
		{
			_pointerId ??= eventData.pointerId;
		}

		void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
		{
			if (_pointerId == eventData.pointerId)
				_pointerId = null;
		}

		void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
		{
			if (_pointerId == eventData.pointerId)
				_pointerId = null;
		}

		private void OnEnable()
		{
			_pointerId = null;
		}

		private void OnDisable()
		{
			_pointerId = null;
		}

		private int? _pointerId;
	}
}