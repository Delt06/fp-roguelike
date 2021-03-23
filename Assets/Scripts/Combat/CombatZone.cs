using Combat.Moves;
using DELTation.Entities;
using UnityEngine;

namespace Combat
{
	public sealed class CombatZone : MonoBehaviour
	{
		[SerializeField] private bool _startAsSecond = true;
		
		public void Construct(CombatBehaviour behaviour, IEntity entity)
		{
			_behaviour = behaviour;
			_entity = entity;
		}

		private void OnTriggerEnter(Collider other)
		{
			if (!other.TryGetComponent(out IEntity otherEntity)) return;
			if (!otherEntity.TryGet<ICombatMoveMaker>(out _)) return;
			
			if (_startAsSecond)
				_behaviour.TryStart(otherEntity, _entity);
			else
				_behaviour.TryStart(_entity, otherEntity);
		}

		private CombatBehaviour _behaviour;
		private IEntity _entity;
	}
}