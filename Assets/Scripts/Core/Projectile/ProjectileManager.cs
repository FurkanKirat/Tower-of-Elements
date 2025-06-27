#nullable enable
using System.Collections.Generic;
using Core.Game;
using Core.Interfaces;
using Core.Math;
using Core.Database;
using Core.Entities;
using Core.Tower;
using UnityEngine;

namespace Core.Projectile
{

    public class ProjectileManager : MonoBehaviour, IGameContextUser, IGameUpdatable
    {
        [SerializeField] private Transform projectileParent;

        private IGameContext? _gameContext;

        

        // Active instances & their behaviours
        private readonly List<ProjectileInstance> _instances = new();
        private readonly Dictionary<ProjectileInstance, ProjectileBehaviour> _views = new();

        private void Awake()
        {
            ProjectileDataDatabase.Load();
            ProjectileAssetDatabase.Load();
        }

        public void SetContext(IGameContext context)
        {
            _gameContext = context;
        }

        public void UpdateLogic()
        {
            if (_gameContext == null) return;

            // Update all projectiles; remove those marked destroyed
            for (int i = _instances.Count - 1; i >= 0; i--)
            {
                var inst = _instances[i];
                inst.UpdateLogic(_gameContext);
                if (inst.IsDestroyed)
                    RemoveProjectile(inst);
            }
        }
        
        public void SpawnProjectile(
            string projectileId,
            Vec2Float startPos,
            IEnemy? target,
            ProjectileEffectData effects)
        {
            if (_gameContext == null) return;
            var data = ProjectileDataDatabase.Get(projectileId);
            var asset = ProjectileAssetDatabase.Get(projectileId);
            if (data == null || asset == null) 
            {
                Debug.LogWarning($"Projectile '{projectileId}' not found.");
                return;
            }

            // Create instance
            int instanceId = _instances.Count + 1;
            var instance = new ProjectileInstance(
                instanceId,
                data,
                effects,
                startPos,
                target
            );
            instance.SetContext(_gameContext);
            _instances.Add(instance);

            // Instantiate view
            var worldPos = new Vector3(startPos.x, startPos.y, 0f);
            var go = Instantiate(asset.prefab, worldPos, Quaternion.identity, projectileParent);
            var view = go.GetComponent<ProjectileBehaviour>();
            view.Initialize(instance);
            view.SetContext(_gameContext);
            _views[instance] = view;
        }

        private void RemoveProjectile(ProjectileInstance inst)
        {
            _instances.Remove(inst);
            if (_views.TryGetValue(inst, out var view))
            {
                Destroy(view.gameObject);
                _views.Remove(inst);
            }
        }
    }
}
