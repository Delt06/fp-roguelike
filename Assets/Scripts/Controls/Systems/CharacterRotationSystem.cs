using Controls.Data;
using UnityEngine;

namespace Controls.Systems
{
	public sealed class CharacterRotationSystem : MonoBehaviour
	{
		public void Construct(CharacterControlsData data, IRotatedTransform rotatedTransform)
		{
			_data = data;
			_rotatedTransform = rotatedTransform;
		}

		private void Update()
		{
			if (Mathf.Approximately(_data.RotationAngle, 0f)) return;

			_rotatedTransform.Transform.Rotate(Vector3.up, _data.RotationAngle);
			_data.RotationAngle = 0f;
		}

		private void OnEnable()
		{
			_data.RotationAngle = 0f;
		}

		private CharacterControlsData _data;
		private IRotatedTransform _rotatedTransform;
	}
}