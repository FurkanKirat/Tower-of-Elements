using System.Collections.Generic;
using UnityEngine;

namespace Core.Database
{
    public static class SpriteDatabaseHelper
    {
        public static Dictionary<string, Sprite> LoadSprites(string folder)
        {
            var dict = new Dictionary<string, Sprite>();
            foreach (var sprite in Resources.LoadAll<Sprite>(folder))
            {
                dict[sprite.name] = sprite;
            }
            return dict;
        }
    }

}