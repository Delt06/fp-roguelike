using UI.Minimaps.Extensions;
using UnityEngine;

namespace UI.Minimaps
{
	public sealed class Minimap : MonoBehaviour
	{
		[SerializeField] private float _scale = 100f;
		[SerializeField] private Transform _referencePoint = default;

		public Vector3 ReferencePosition => _referencePoint.position;
		public Vector3 ReferenceRotation => _referencePoint.eulerAngles;

		public bool ShouldBeDrawn(in MinimapDrawArgs args)
		{
			foreach (var drawCondition in _drawConditions)
			{
				if (!drawCondition.IsMet(args))
					return false;
			}

			return true;
		}

		public void OnDrawn(in MinimapDrawArgs args)
		{
			foreach (var drawHandler in _drawHandlers)
			{
				drawHandler.OnDrawn(args);
			}
		}

		public Vector2 WorldToLocalPosition(Vector3 worldPosition)
		{
			var offset = worldPosition - ReferencePosition;
			return new Vector2(offset.x, offset.z) * _scale;
		}

		private void Start()
		{
			foreach (var extension in _extensions)
			{
				extension.Initialize(this);
			}
		}

		private void Awake()
		{
			_extensions = GetComponentsInChildren<IMinimapExtension>();
			_drawConditions = GetComponentsInChildren<IMinimapDrawCondition>();
			_drawHandlers = GetComponentsInChildren<IMinimapDrawHandler>();
		}

		private IMinimapExtension[] _extensions;
		private IMinimapDrawCondition[] _drawConditions;
		private IMinimapDrawHandler[] _drawHandlers;
	}
}