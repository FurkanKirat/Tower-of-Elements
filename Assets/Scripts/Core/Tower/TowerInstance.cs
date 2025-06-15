using System;
using System.Collections.Generic;
using Core.Database;
using Core.Interfaces;
using Core.Math;
using Core.Physics;
using Core.SaveSystem;
using Core.SaveSystem.SaveData;


namespace Core.Tower
{
    [Serializable]
    public class TowerInstance : IGameObject, ISaveable<TowerSaveData>
{
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

    public TowerInstance(TowerData data, TowerAssetData asset)
    {
        TowerId = data.towerId;
        TowerStats = data;
        AssetData = asset;
    }

    public void ApplyUpgrade(int upgradeId)
    {
        SelectedUpgrades.Add(upgradeId);
        RecalculateBonusStats();
    }

    private void RecalculateBonusStats()
    {
        BonusStats = new TowerBonusStats();
        foreach (var id in SelectedUpgrades)
        {
            //var upgrade = TowerUpgradeDatabase.Get(id);
            //BonusStats.Apply(upgrade);
        }
    }

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

