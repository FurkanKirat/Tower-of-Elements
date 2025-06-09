using System;
using System.Collections.Generic;
using Core.Interfaces;
using Core.Physics;


namespace Core.Tower
{
    [Serializable]
    public class TowerInstance : IGameObject
    {
        public int InstanceId { get; set; }
        public AABB Hitbox { get; set; }

        public int TowerId { get; set; }
        public int CurrentLevel { get; set; } = 1;
        public HashSet<int> SelectedUpgrades = new();
        public TowerBonusStats BonusStats { get; set; }
        public float CurrentCooldown { get; set; }
        public bool IsPlaced { get; set; }
        
        public void UpdateLogic()
        {
            throw new NotImplementedException();
        }

        public void OnDestroy()
        {
            throw new NotImplementedException();
        }

        
    }

}

