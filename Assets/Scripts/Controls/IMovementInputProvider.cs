using UnityEngine;

namespace Controls
{
	public interface IMovementInputProvider
	{
		Vector2? Input { get; }
	}
}