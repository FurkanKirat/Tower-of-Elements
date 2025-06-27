using Core.Math;
using Core.Physics;

namespace Core.Entities
{
    public interface IEntity
    {
        public int EntityId { get; set; }
        AABB Hitbox { get; }
        Vec2Float Velocity { get; set; }
        Vec2Float Position { get; set; }

        bool CollidesWith(AABB box);
        void Update(float deltaTime);
    }
}