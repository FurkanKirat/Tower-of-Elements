using Managers.Log;

namespace Core.Database
{
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using UnityEngine.Tilemaps;
    
    public static class TileDatabase
    {
        private static Dictionary<string, TileBase> _tiles;

        public static void Load()
        {
            _tiles = Resources.LoadAll<TileBase>("Tiles")
                .ToDictionary(t => t.name, t => t);
            
        }

        public static TileBase Get(string id) {
            return _tiles.GetValueOrDefault(id);
        }
            

        public static IReadOnlyDictionary<string, TileBase> All => _tiles;
    }
}