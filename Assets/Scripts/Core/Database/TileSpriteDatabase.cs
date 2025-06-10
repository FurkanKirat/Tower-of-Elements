using System.Collections.Generic;
using UnityEngine;

namespace Core.Database
{
    public static class TileSpriteDatabase
    {
        private static Dictionary<string, Sprite> _data;

        public static void Load()
        {
            _data = SpriteDatabaseHelper.LoadSprites("Art/Tiles");
        }

        public static Sprite Get(string id) => _data.GetValueOrDefault(id);

        public static IReadOnlyDictionary<string, Sprite> AllTiles => _data;
    }
}