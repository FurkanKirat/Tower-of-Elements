#nullable enable
using System.Collections.Generic;
using System.Linq;
using Core.Game;
using Core.Interfaces;
using UnityEngine;

namespace Core.Entities
{
    public class EnemyManager : MonoBehaviour, IEnemyManager, IGameContextUser, IGameUpdatable
    {
        private IGameContext _gameContext;
        private List<IEnemy> _enemies = new();
        
        public IReadOnlyList<IEnemy> Enemies => _enemies;
        public void SpawnEnemy(string enemyId, Vector2 spawnPos)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateEnemies()
        {
            for (int i = _enemies.Count - 1; i >= 0; i--)
            {
                var enemy = _enemies[i];
                enemy.UpdateLogic(_gameContext);

                if (!enemy.IsAlive)
                {
                    enemy.OnDestroy();
                    _enemies.RemoveAt(i);
                }
            }
        }

        public void ClearAllEnemies()
        {
            foreach (var enemy in _enemies)
                enemy.OnDestroy();
            
            _enemies.Clear();
        }

        public IEnemy? FindClosestEnemy(Vector2 position, float maxRange)
        {
            float minDistSq = maxRange * maxRange;
            IEnemy? closest = null;

            foreach (var enemy in _enemies)
            {
                if (!enemy.IsAlive) continue;
                float distSq = (enemy.Position - position).sqrMagnitude;
                if (distSq < minDistSq)
                {
                    minDistSq = distSq;
                    closest = enemy;
                }
            }

            return closest;
        }

        public List<IEnemy> GetEnemiesInRange(Vector2 center, float radius)
        {
            return _enemies
                .Where((enemy, _) => Vector2.SqrMagnitude(center - enemy.Hitbox.Center) <= radius * radius)
                .ToList();
        }

        public void SetContext(IGameContext context)
        {
            _gameContext = context;
        }

        public void UpdateLogic()
        {
            UpdateEnemies();
        }
    }
}