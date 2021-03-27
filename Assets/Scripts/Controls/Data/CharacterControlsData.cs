using UnityEngine;

namespace Controls.Data
{
	public sealed class CharacterControlsData : MonoBehaviour
	{
		[SerializeField, Min(0f)] private float _speed = 1f;

		public float RotationAngle { get; set; }

		public bool Move { get; set; }

		public float Speed => _speed;
	}
}