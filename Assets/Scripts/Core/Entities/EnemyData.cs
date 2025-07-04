﻿using System;
using Core.ElementSystem;

namespace Core.Entities
{
    [Serializable]
    public class EnemyData
    {
        public string enemyId;
        public EnemyAssetData assetData;
        public ElementType element;
        public float spawnDelay;
    }
}