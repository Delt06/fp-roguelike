using UnityEngine;

namespace Controls
{
	public interface IRotatedTransform
	{
		Transform Transform { get; }
	}

	public sealed class RotatedTransform : MonoBehaviour, IRotatedTransform
	{
		[SerializeField] private Transform _transform = default;

		public Transform Transform => _transform;
	}
}