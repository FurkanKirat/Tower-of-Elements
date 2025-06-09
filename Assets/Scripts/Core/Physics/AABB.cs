using System;
using Core.Math;

namespace Core.Physics
{
    [Serializable]
    public class AABB
    {
        public Vec2Float Center;
        public Vec2Float Size;

        public Vec2Float Min => new Vec2Float(Center.x - Size.x / 2f, Center.y - Size.y / 2f);
        public Vec2Float Max => new Vec2Float(Center.x + Size.x / 2f, Center.y + Size.y / 2f);

        public AABB(Vec2Float center, Vec2Float size)
        {
            Center = center;
            Size = size;
        }

        public bool Overlaps(AABB other)
        {
            return !(Max.x < other.Min.x || Min.x > other.Max.x ||
                     Max.y < other.Min.y || Min.y > other.Max.y);
        }

        public bool Contains(Vec2Float point)
        {
            return point.x >= Min.x && point.x <= Max.x &&
                   point.y >= Min.y && point.y <= Max.y;
        }

        public void Move(Vec2Float delta)
        {
            Center += delta;
        }

        public override string ToString()
        {
            return $"AABB(Center: {Center}, Size: {Size})";
        }
    }
    

}