using Core.Math;

namespace Core.Interfaces
{
    public interface ITargetable
    {
        public bool IsAlive { get; }
        public Vec2Float Position { get; }
        public float CurrentHealth {get;}
        public float MaxHealth { get; }
        public void TakeDamage(float amount);  
    }

}