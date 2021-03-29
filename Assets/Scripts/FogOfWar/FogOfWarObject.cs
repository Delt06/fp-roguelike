using DG.Tweening;
using DG.Tweening.Core;
using UnityEngine;

namespace FogOfWar
{
	public sealed class FogOfWarObject : MonoBehaviour
	{
		[SerializeField, Min(0f)] private float _revealDuration = 1f;
		[SerializeField] private Ease _revealingEase = Ease.Linear;

		public bool IsRevealed { get; private set; }
		public float RevealingProgress { get; private set; }

		private const float MaxRevealingProgress = 1f;

		public void Reveal()
		{
			if (IsRevealed) return;
			IsRevealed = true;
			DOTween.To(_progressGetter, _progressSetter, MaxRevealingProgress, _revealDuration)
				.SetEase(_revealingEase);
		}

		private void Awake()
		{
			_progressGetter = () => RevealingProgress;
			_progressSetter = value => RevealingProgress = value;
		}

		private DOGetter<float> _progressGetter;
		private DOSetter<float> _progressSetter;
	}
}