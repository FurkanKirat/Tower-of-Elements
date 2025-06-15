using System.Collections.Generic;
using System;

namespace Core.Tower
{
    [Serializable]
    public class TowerData
    {
        public string towerId;
        public string assetName;
        
        public float range;
        public float baseCooldown;
        public float health;
        public float damage;
        public float attackSpeed;
        public float buildCost;
        public float pierceCount;
        public float splashRadius;
        public float abilityDuration;
        public float chainCount;
        
        public string projectileId;
        public List<TowerStatusEffectData> statusEffects;
    }

}