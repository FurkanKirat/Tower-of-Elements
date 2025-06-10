using System;
using Core.ElementSystem;
using UnityEngine.Serialization;

namespace Core.Entities
{
    [Serializable]
    public class EnemyData
    {
        public string enemyId;
        public ElementType element;
        public float spawnDelay;
        public int level;
    }
}