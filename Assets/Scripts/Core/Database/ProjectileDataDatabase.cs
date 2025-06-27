using System.Collections.Generic;
using Core.Projectile;
using Managers;
using Newtonsoft.Json;

namespace Core.Database
{
    public static class ProjectileDataDatabase
    {
        private static Dictionary<string, ProjectileData> _projectiles;


        public static void Load()
        {
            _projectiles = InfoDatabaseHelper.LoadFromFolder<ProjectileData, string>("Data/Projectiles",
                getId: data => data.projectileId,
                deserializer: jsonText => JsonHelper.LoadFromText<ProjectileData>(jsonText)
            );
        }

        public static ProjectileData Get(string id)
        {
            return _projectiles.GetValueOrDefault(id);
        }
        
        public static IEnumerable<ProjectileData> All()
        {
            return _projectiles != null
                ? _projectiles.Values
                : new List<ProjectileData>();
        }
    }
}