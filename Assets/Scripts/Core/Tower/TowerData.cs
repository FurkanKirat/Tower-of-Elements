using System;

namespace Core.Tower
{
    [Serializable]
    public class TowerData
    {
        public float Range {get; set;}
        public float BaseCooldown {get; set;}
        public float Health {get; set;}
        public float Damage {get; set;}
        public float AttackSpeed {get; set;}
        public float BuildCost {get; set;}
        public float PierceCount {get; set;}
        public float SplashRadius {get; set;}
        public float AbilityDuration {get; set;}
        public float ChainCount {get; set;}
    }
}