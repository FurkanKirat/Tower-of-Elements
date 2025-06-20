using Core.Interfaces;

namespace Core.Entities
{
    public interface IEnemy : IGameObject, ITargetable
    {
        public EnemyStats EnemyStats { get; }
        public void MoveAlongPath();
        public void OnReachEnd();
    }
}