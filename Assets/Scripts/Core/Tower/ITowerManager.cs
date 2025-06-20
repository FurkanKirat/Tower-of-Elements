using System.Collections.Generic;
using UnityEngine;

namespace Core.Tower
{
    public interface ITowerManager
    {
        public IReadOnlyDictionary<Vector2Int,TowerInstance> ActiveTowers { get; }
        public void BuildTower(string towerId, Vector2Int gridPos);
        bool CanBuild(string towerId, Vector2Int gridPos);
        bool RemoveTower(Vector2Int gridPos);
        bool HasTowerAt(Vector2Int gridPos);
    }
}