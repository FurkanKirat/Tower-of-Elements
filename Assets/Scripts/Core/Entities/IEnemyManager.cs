 #nullable enable
using System.Collections.Generic;
using Core.Math;
using UnityEngine;

namespace Core.Entities
{
    public interface IEnemyManager
    {
        public IReadOnlyList<IEnemy> Enemies  { get; }
        
        public void SpawnEnemy(string enemyId, Vector2 spawnPos);

        public void UpdateEnemies();

        public void ClearAllEnemies();

        public IEnemy? FindClosestEnemy(Vector2 position, float maxRange);

        public List<IEnemy> GetEnemiesInRange(Vector2 center, float radius);
    }
}