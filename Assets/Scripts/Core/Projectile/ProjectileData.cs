using System.Collections.Generic;
using System;


namespace Core.Projectile
{
    [Serializable]
    public class ProjectileData
    {
        public string projectileId;
        public string assetName;

        public float speed;
        
        public List<StatusEffectData> statusEffects;
    }


    [Serializable]
    public class StatusEffectData
    {
        public string effectId;
        public float chance; //Efekti uygulama şansı
        public float duration;
    }
}