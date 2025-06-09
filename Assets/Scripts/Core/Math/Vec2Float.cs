using System;
using UnityEngine;

namespace Core.Math
{
    [Serializable]
    public struct Vec2Float : IEquatable<Vec2Float>
    {
        public float x;
        public float y;

        public Vec2Float(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public static Vec2Float operator +(Vec2Float a, Vec2Float b)
        {
            return new Vec2Float(a.x + b.x, a.y + b.y);
        }

        public static Vec2Float operator -(Vec2Float a, Vec2Float b)
        {
            return new Vec2Float(a.x - b.x, a.y - b.y);
        }

        public bool Equals(Vec2Float other)
        {
            return this == other;
        }

        public static bool operator ==(Vec2Float a, Vec2Float b)
        {
            return Mathf.Approximately(a.x, b.x) && Mathf.Approximately(a.y, b.y);
        }

        public static bool operator !=(Vec2Float a, Vec2Float b)
        {
            return !(a == b);
        }

        public override bool Equals(object obj)
        {
            if (obj is Vec2Float other)
                return this == other;
            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + x.GetHashCode();
                hash = hash * 23 + y.GetHashCode();
                return hash;
            }
        }

        public static implicit operator Vector2(Vec2Float v) => new Vector2(v.x, v.y);
        public static implicit operator Vec2Float(Vector2 v) => new Vec2Float(v.x, v.y);

        public override string ToString()
        {
            return $"{nameof(x)}: {x}, {nameof(y)}: {y}";
        }
    }
}