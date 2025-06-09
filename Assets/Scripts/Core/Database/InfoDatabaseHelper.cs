using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Database
{
    public static class InfoDatabaseHelper
    {
        public static Dictionary<TId, T> LoadFromFolder<T, TId>(
            string folder,
            Func<T, TId> getId,
            Func<string, T> deserializer
        ) where T : class
        {
            var dict = new Dictionary<TId, T>();
            foreach (var file in Resources.LoadAll<TextAsset>(folder))
            {
                T obj = deserializer(file.text);
                dict[getId(obj)] = obj;
            }
            return dict;
        }
    }
}