using System.Collections.Generic;
using Core.Projectile;
using UnityEngine;

namespace Core.Database
{
    public static class ProjectileAssetDatabase
    {
        private static Dictionary<string, ProjectileAssetData> _dict;

        
        public static void Load()
        {
            _dict = new();
            foreach (var asset in Resources.LoadAll<ProjectileAssetData>("ProjectileAssets"))
            {
                _dict[asset.projectileId] = asset;
            }
        }
        
        public static ProjectileAssetData Get(string id) => _dict[id];
        
        public static IEnumerable<ProjectileAssetData> All()
        {
            return _dict != null
                ? _dict.Values
                : new List<ProjectileAssetData>();
        }
    }
}