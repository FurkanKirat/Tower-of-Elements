using System;
using Core.Interfaces;

namespace Core.Entities
{
    public interface IEnemy : IGameObject, ITargetable
    {
        public EnemyStats EnemyStats { get; }
        public void MoveAlongPath();
        public void OnReachEnd();
        public void Die();
        public void TakeDamage(float amount, AttackType type);
        event Action<IEnemy> OnDeath;
        void ApplyStatusEffect(AttackType effect);

    }
}