using Controls.Data;
using UnityEngine;

namespace Controls.Systems
{
	public sealed class CharacterRotationSystem : MonoBehaviour
	{
		[SerializeField] private Transform _target = default;
		
		public void Construct(CharacterControlsData data)
		{
			_data = data;
		}

		private void Update()
		{
			if (Mathf.Approximately(_data.RotationAngle, 0f)) return;
			
			_target.Rotate(Vector3.up, _data.RotationAngle);
			_data.RotationAngle = 0f;
		}

		private void OnEnable()
		{
			_data.RotationAngle = 0f;
		}

		private CharacterControlsData _data;
	}
}