using System;
using Controls.Data;
using UI;
using UnityEngine;

namespace Controls.InputHandling
{
	public sealed class CharacterRotationInputHandler : MonoBehaviour
	{
		[SerializeField, Min(0f)] private float _sensitivity = 1f;
		[SerializeField] private bool _invert = true;

		public void Construct(DragProvider dragProvider, CharacterControlsData data)
		{
			_dragProvider = dragProvider;
			_data = data;
		}

		private void OnEnable()
		{
			_dragProvider.OnDragged += _onDragged;
		}

		private void OnDisable()
		{
			_dragProvider.OnDragged -= _onDragged;
			_data.RotationAngle = 0f;
		}

		private void Awake()
		{
			_camera = Camera.main;

			_onDragged = drag =>
			{
				var viewportDrag = _camera.ScreenToViewportPoint(drag);
				var angle = viewportDrag.x * _sensitivity;

				if (_invert)
					angle *= -1f;

				_data.RotationAngle += angle;
			};
		}

		private Action<Vector2> _onDragged;

		private DragProvider _dragProvider;
		private Camera _camera;
		private CharacterControlsData _data;
	}
}