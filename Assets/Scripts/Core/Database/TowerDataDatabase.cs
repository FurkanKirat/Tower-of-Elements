using System.Collections.Generic;
using Core.Tower;
using Managers;
using Newtonsoft.Json;

namespace Core.Database
{
    public static class TowerDataDatabase
    {
        private static Dictionary<string, TowerData> _towers;

        public static void Load()
        {
            _towers = InfoDatabaseHelper.LoadFromFolder<TowerData, string>("Data/Towers",
                getId: data => data.towerId,
                deserializer: data => JsonHelper.LoadFromText<TowerData>(data)
                );
        }

        public static TowerData Get(string id)
        {
            return _towers.GetValueOrDefault(id);
        }
    }
}