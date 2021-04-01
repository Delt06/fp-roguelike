using System;
using Magic.Concrete.Healing;
using UnityEngine;

namespace VFX
{
	[RequireComponent(typeof(ISpellComponent))]
	public sealed class SpellComponent_OnUsed_PlayParticleEffect : MonoBehaviour
	{
		[SerializeField] private ParticleSystem _effect = default;
		
		private void OnEnable()
		{
			_spell.Used += _onUsed;
		}

		private void OnDisable()
		{
			_spell.Used -= _onUsed;
		}

		private void Awake()
		{
			_spell = GetComponent<ISpellComponent>();
			_onUsed = () => _effect.Play();
		}

		private Action _onUsed;
		private ISpellComponent _spell;
	}
}