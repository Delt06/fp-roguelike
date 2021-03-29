using DELTation.Entities;
using UnityEngine;

namespace FogOfWar
{
	public sealed class FogOfWarObjectTrigger : MonoBehaviour
	{
		[SerializeField] private FogOfWarObject _object = default;

		private void OnTriggerEnter(Collider other)
		{
			if (!other.TryGetInEntity(out FogOfWarRevealer _)) return;

			_object.Reveal();
			enabled = false;
		}
	}
}