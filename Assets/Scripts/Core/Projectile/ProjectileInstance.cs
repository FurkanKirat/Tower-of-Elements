#nullable enable
using System;
using Core.Entities;
using Core.Game;
using Core.Interfaces;
using Core.Math;
using Core.Physics;
using UnityEngine;

namespace Core.Projectile
{
    [Serializable]
    public class ProjectileInstance : IGameObject, IGameContextUser
    {
        // 1. Fields
        private IEnemy? _target;
        private float _traveledDistance;
        private IGameContext? _context;

        // 2. Properties (IGameObject’dan)
        public int InstanceId { get; set; }
        public AABB Hitbox    { get; set; }
        public bool IsDestroyed { get; private set; }
        public Vec2Float Position { get; private set; }

        // 3. Data ve Effect
        public ProjectileData Data     { get; }
        public ProjectileEffectData Effects { get; }
        

        // 4. Constructor
        public ProjectileInstance(
            int instanceId,
            ProjectileData data,
            ProjectileEffectData effects,
            Vec2Float startPos,
            IEnemy? target)
        {
            InstanceId        = instanceId;
            Data              = data;
            Effects           = effects;
            Position          = startPos;
            _target           = target;
            _traveledDistance = 0f;
            IsDestroyed       = false;
            Hitbox            = new AABB(startPos, new Vec2Float(0.2f, 0.2f));
        }

        // 5. IGameContextUser
        public void SetContext(IGameContext context)
        {
            _context = context;
        }

        // 6. IGameObject.OnDestroy
        public void OnDestroy()
        {
            IsDestroyed = true;
        }

        public void OnRemoved()
        {
            Debug.Log($"Enemy {InstanceId} destroyed.");
        
        }

        // 7. IGameObject.UpdateLogic(IGameContext)
        public void UpdateLogic(IGameContext context)
        {
            if (IsDestroyed)
                return;

            float delta = Time.deltaTime;
            Vec2Float dir;

            if (_target != null && _target.IsAlive)
                dir = (_target.Hitbox.Center - Position).Normalized();
            else
                dir = new Vec2Float(1f, 0f); //SOR BUNU !!!!!

            float step = Data.speed * delta;
            
            Position = new Vec2Float(
                Position.x + dir.x * step,
                Position.y + dir.y * step
            );

            _traveledDistance += step;

            // Move hitbox the same delta
            var deltaMove = new Vec2Float(dir.x * step, dir.y * step);
            Hitbox.Move(deltaMove);
            


            if (_target != null && _target.IsAlive &&
                Hitbox.Overlaps(_target.Hitbox))
            {
                HandleHit(_target, context);
            }
        }

        // 8. Internal helper
        private void HandleHit(IEnemy enemy, IGameContext context)
        {
            // Base damage
            enemy.TakeDamage(Effects.damage);

          
        }
    }
}
