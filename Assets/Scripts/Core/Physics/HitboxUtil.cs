using UnityEngine;

namespace Core.Physics
{
    public static class HitboxUtil
    {
        /// <summary>
        /// Returns the closest point within AABB to the given point.
        /// </summary>
        public static Vector2 GetClosestPointToAABB(Vector2 point, AABB aabb)
        {
            float x = Mathf.Clamp(point.x, aabb.Min.x, aabb.Max.x);
            float y = Mathf.Clamp(point.y, aabb.Min.y, aabb.Max.y);
            return new Vector2(x, y);
        }

        /// <summary>
        /// Gives the distance between the tower position and the nearest point within AABB.
        /// </summary>
        public static float GetDistanceToAABB(Vector2 point, AABB aabb)
        {
            Vector2 closest = GetClosestPointToAABB(point, aabb);
            return Vector2.Distance(point, closest);
        }

        /// <summary>
        /// Checks if the circle intersects AABB (is it in range?).
        /// </summary>
        public static bool IntersectsCircle(Vector2 center, float radius, AABB aabb)
        {
            Vector2 closest = GetClosestPointToAABB(center, aabb);
            float distance = Vector2.SqrMagnitude(center - closest);
            return distance <= radius * radius;
        }
    }
}