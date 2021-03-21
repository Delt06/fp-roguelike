using System;
using UnityEngine;

namespace Controls
{
	public interface IDragProvider
	{
		event Action<Vector2> OnDragged;
	}
}