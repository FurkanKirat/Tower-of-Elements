using System.Collections.Generic;
using Core.Tower;
using UnityEngine;

namespace Core.Database
{
    public static class TowerAssetDatabase
    {
        private static Dictionary<string, TowerAssetData> _dict;

        public static void Load()
        {
            _dict = new();
            foreach (var asset in Resources.LoadAll<TowerAssetData>("TowerAssets"))
                _dict[asset.towerId] = asset;
        }

        public static TowerAssetData Get(string id) => _dict[id];
    }

}