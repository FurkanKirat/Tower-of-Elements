#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;

namespace Core.Database
{
    public static class TileSpriteDatabase
    {
        private static Dictionary<string, Sprite> _sprites;

        public static void Load()
        {
            _sprites = SpriteDatabaseHelper.LoadSprites("Art/Tiles");
        }

        public static Sprite Get(string id) =>
            _sprites.GetValueOrDefault(id);

        public static IReadOnlyDictionary<string, Sprite> All => _sprites;
    }
}
#endif