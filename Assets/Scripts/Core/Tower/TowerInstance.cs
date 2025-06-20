#nullable enable
using System;
using System.Collections.Generic;
using Core.Database;
using Core.Entities;
using Core.Game;
using Core.Interfaces;
using Core.Math;
using Core.Physics;
using Core.SaveSystem;
using Core.SaveSystem.SaveData;
using UnityEngine;


namespace Core.Tower
{
    [Serializable]
    public class TowerInstance : IAttacker, IGameObject, ISaveable<TowerSaveData>
    {
        // 1. Fields (private/protected)
        private IEnemy? _target;

        // 2. Properties (public get/set)
        public int InstanceId { get; set; }
        public AABB Hitbox { get; set; }
        public string TowerId { get; private set; }
        public int CurrentLevel { get; private set; } = 1;
        public HashSet<int> SelectedUpgrades { get; private set; } = new();
        public TowerBonusStats BonusStats { get; private set; } = new();
        public float CurrentCooldown { get; set; }
        public bool IsPlaced { get; set; }
        public TowerData TowerStats { get; private set; }
        public TowerAssetData AssetData { get; private set; }
        public float AttackSpeed => TowerStats.attackSpeed;
        public float Range => TowerStats.range;

        // 3. Constructor
        public TowerInstance(TowerData data, TowerAssetData asset, Vec2Float position)
        {
            TowerId = data.towerId;
            TowerStats = data;
            AssetData = asset;
            Hitbox = new AABB(position, new Vec2Float(1,1));
        }
        

        // 4. Public API Methods
        public void ApplyUpgrade(int upgradeId)
        {
            SelectedUpgrades.Add(upgradeId);
            RecalculateBonusStats();
        }
        
        // 5. Game Loop
        public void OnDestroy()
        {
            _target = null;
            SelectedUpgrades.Clear();
            BonusStats = new TowerBonusStats();
            CurrentCooldown = 0f;
            IsPlaced = false;
        }
        
        public void UpdateAttack(IGameContext context)
        {
            if (!IsPlaced)
                return;

            CurrentCooldown -= Time.deltaTime;

            if (CurrentCooldown > 0)
                return;
            
            _target ??= context.EnemyManager.FindClosestEnemy(Hitbox.Center, Range);

            // In range eklenecek
            if (_target != null && _target.IsAlive)
            {
                _target.TakeDamage(TowerStats.damage);
                CurrentCooldown = 1f / AttackSpeed;
            }
            else
            {
                _target = null;
            }
        }
        
        public void UpdateLogic(IGameContext gameContext)
        {
            UpdateAttack(gameContext);
        }
        
        // 6. Save/Load
        public TowerSaveData ToSaveData() => new()
        {
            towerId = TowerId,
            currentLevel = CurrentLevel,
            cooldown = CurrentCooldown,
            selectedUpgrades = new List<int>(SelectedUpgrades),
            position = Hitbox.Center
        };

        public void LoadFromSaveData(TowerSaveData data)
        {
            TowerId = data.towerId;
            CurrentLevel = data.currentLevel;
            CurrentCooldown = data.cooldown;
            SelectedUpgrades = new HashSet<int>(data.selectedUpgrades);
            Hitbox = new AABB(data.position, new Vec2Float(1, 1));
            IsPlaced = true;

            TowerStats = TowerDataDatabase.Get(TowerId);
            AssetData = TowerAssetDatabase.Get(TowerId);
            RecalculateBonusStats();
        }
        
        // 7. Internal helpers
        private void RecalculateBonusStats()
        {
            BonusStats = new TowerBonusStats();
            foreach (var id in SelectedUpgrades)
            {
                //var upgrade = TowerUpgradeDatabase.Get(id);
                //BonusStats.Apply(upgrade);
            }
        }
       

      
        
       
    }


}

