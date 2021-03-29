using UnityEngine;

namespace FogOfWar
{
	public sealed class FogOfWarObject : MonoBehaviour
	{
		[SerializeField, Min(0f)] private float _revealDuration = 1f;

		public bool IsRevealed { get; private set; }
		public float RevealingProgress { get; private set; }

		private const float MaxRevealingProgress = 1f;

		public void Reveal()
		{
			if (IsRevealed) return;
			IsRevealed = true;
		}

		private void Update()
		{ 
			if (!IsRevealed) return;
			if (RevealingProgress >= MaxRevealingProgress) return;
			UpdateRevealingProgress(Time.deltaTime);
		}

		private void UpdateRevealingProgress(float deltaTime)
		{
			var revealSpeed = MaxRevealingProgress / _revealDuration;
			var maxDelta = revealSpeed * deltaTime;
			RevealingProgress = Mathf.MoveTowards(RevealingProgress, MaxRevealingProgress, maxDelta);
		}
	}
}