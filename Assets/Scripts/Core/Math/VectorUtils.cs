using UnityEngine;

namespace Core.Math
{
    public static class VectorUtils
    {
        public static Vec2Int ToVec2Int(this Vector2Int v) => new(v.x, v.y);
        public static Vec2Float ToVec2Float(this Vector2Int v) => new(v.x, v.y);
        public static Vec2Float ToVec2Float(this Vector2 v) => new(v.x, v.y);
    }
}