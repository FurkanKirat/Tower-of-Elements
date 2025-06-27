using System;
using System.Collections.Generic;
using Core.Tower;  

namespace Core.Projectile
{

    [Serializable]
    public class ProjectileEffectData
    {

        public float damage;

        public int pierceCount;

        public float splashRadius;

        public float slowAmount;

        public float slowDuration;

        public float freezeDuration;

        public int chainCount;
        
        public List<TowerStatusEffectData> statusEffects = new List<TowerStatusEffectData>();
    }
}