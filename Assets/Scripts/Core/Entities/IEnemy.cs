using Core.Interfaces;

namespace Core.Entities
{
    public interface IEnemy : IEntity, ITargetable
    {
        public EnemyStats EnemyStats { get; }
    }
}