using UnityEngine;

namespace Entities
{
	public sealed class NamedComponent : MonoBehaviour, INamed
	{
		[SerializeField] private string _name = "Name";

		public string Name => _name ?? string.Empty;
	}
}