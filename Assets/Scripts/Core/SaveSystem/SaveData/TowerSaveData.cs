using System;
using System.Collections.Generic;
using Core.Math;

namespace Core.SaveSystem.SaveData
{
    [Serializable]
    public class TowerSaveData
    {
        public string towerId;
        public int currentLevel;
        public float cooldown;
        public List<int> selectedUpgrades;
        public Vec2Float position;
    }

}