using System;
using Controls;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
	public sealed class DragProvider : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler,
		IPointerExitHandler, IDragProvider
	{
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

		void IDragHandler.OnDrag(PointerEventData eventData)
		{
			if (_pointerId == eventData.pointerId)
				OnDragged?.Invoke(eventData.delta);
		}

		public event Action<Vector2> OnDragged;

		private void OnEnable()
		{
			_pointerId = null;
		}

		private int? _pointerId;
	}
}