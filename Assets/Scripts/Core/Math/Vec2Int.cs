using System;
using UnityEngine;

namespace Core.Math
{
    [Serializable]
    public struct Vec2Int : IEquatable<Vec2Int>
    {
        public int x;
        public int y;

        public Vec2Int(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public static Vec2Int operator +(Vec2Int a, Vec2Int b)
        {
            return new Vec2Int(a.x + b.x, a.y + b.y);
        }
        
        public static Vec2Int operator -(Vec2Int a, Vec2Int b)
        {
            return new Vec2Int(a.x - b.x, a.y - b.y);
        }
        
        public bool Equals(Vec2Int other)
        {
            return this == other;
        }

        public static bool operator ==(Vec2Int a, Vec2Int b)
        {
            return a.x == b.x && a.y == b.y;
        }

        public static bool operator !=(Vec2Int a, Vec2Int b)
        {
            return !(a == b);
        }

        public override bool Equals(object obj)
        {
            if (obj is Vec2Int other)
                return this == other;
            return false;
        }

        public override int GetHashCode()
        {
            return x * 397 ^ y;
        }
        public static implicit operator Vector2Int(Vec2Int v) => new (v.x, v.y);
        public static implicit operator Vec2Int(Vector2Int v) => new(v.x, v.y);

        public override string ToString()
        {
            return $"{nameof(x)}: {x}, {nameof(y)}: {y}";
        }
    }

}