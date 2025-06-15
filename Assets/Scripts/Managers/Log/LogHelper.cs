using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Managers.Log
{
    using System.Collections.Generic;
    using System.Text;
    
    public static class LogHelper
    {
        public static void LogList<T>(List<T> list, string title = "List")
        {
            if (list == null)
            {
                GameLogger.Warn($"{title} is null");
                return;
            }

            var sb = new StringBuilder();
            sb.AppendLine($"{title} (Count: {list.Count}):");
            for (int i = 0; i < list.Count; i++)
            {
                sb.AppendLine($"[{i}] => {list[i]}");
            }

            GameLogger.Log(sb.ToString());
        }

        public static void LogDictionary<TKey, TValue>(Dictionary<TKey, TValue> dict, string title = "Dictionary")
        {
            if (dict == null)
            {
                GameLogger.Warn($"{title} is null");
                return;
            }

            var sb = new StringBuilder();
            sb.AppendLine($"{title} (Count: {dict.Count}):");
            foreach (var kvp in dict)
            {
                sb.AppendLine($"{kvp.Key} => {kvp.Value}");
            }

            GameLogger.Log(sb.ToString());
        }

        public static void LogGrid<T>(T[,] grid, string title = "Grid")
        {
            if (grid == null)
            {
                GameLogger.Warn($"{title} is null");
                return;
            }

            int width = grid.GetLength(0);
            int height = grid.GetLength(1);
            var sb = new StringBuilder();
            sb.AppendLine($"{title} (Size: {width}x{height}):");

            for (int y = height - 1; y >= 0; y--)
            {
                for (int x = 0; x < width; x++)
                {
                    sb.Append(grid[x, y]?.ToString() ?? "null");
                    sb.Append("\t");
                }
                sb.AppendLine();
            }

            GameLogger.Log(sb.ToString());
        }
        
        public static void LogObject<T>(T obj, string title = "Object")
        {
            if (obj == null)
            {
                GameLogger.Warn($"{title} is null");
                return;
            }
            
            if (obj is UnityEngine.Object unityObj)
            {
                GameLogger.Warn($"{title} is a UnityEngine.Object ({unityObj.GetType().Name}) and cannot be serialized.");
                return;
            }

            try
            {
                string json = JsonConvert.SerializeObject(obj, Formatting.Indented, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    ContractResolver = new SafeUnityResolver()
                });

                GameLogger.Log($"{title}:\n{json}");
            }
            catch (Exception ex)
            {
                GameLogger.Exception(ex, $"LogObject({title}) failed");
            }
        }

        public static void LogObject<T>(T obj, Func<T, string> func, string title = "Object")
        {
            if (obj == null)
            {
                GameLogger.Warn($"{title} is null");
                return;
            }
            GameLogger.Log(func(obj));
        }
        
        private class SafeUnityResolver : DefaultContractResolver
        {
            protected override JsonContract CreateContract(Type objectType)
            {
                if (typeof(UnityEngine.Object).IsAssignableFrom(objectType))
                    return base.CreateObjectContract(typeof(string)); // skip Unity objects

                return base.CreateContract(objectType);
            }
        }
    }
}