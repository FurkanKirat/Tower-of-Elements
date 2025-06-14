﻿namespace Managers
{
    using System.IO;
    using UnityEngine;
    using System.Linq;

    public static class PathHelper
    {
        public static string GetDirectoryName(string path)
        {
            return Path.GetDirectoryName(path);
        }

        public static string GetFileNameWithoutExtension(string path)
        {
            return Path.GetFileNameWithoutExtension(path);
        }
        public static string Combine(params string[] parts)
        {
            return Path.Combine(parts);
        }

        public static string CombineWithPersistentData(params string[] subPaths)
        {
            return Combine(new[] { Application.persistentDataPath }.Concat(subPaths).ToArray());
        }

        public static string CombineWithAssets(params string[] subPaths)
        {
            return Combine(new[] { Application.dataPath }.Concat(subPaths).ToArray());
        }
        
        public static string CombineWithResources(params string[] subPaths)
        {
            return Combine(new[] { Application.dataPath, "Resources" }.Concat(subPaths).ToArray());
        }

        public static string CombineWithStreamingAssets(params string[] subPaths)
        {
            return Combine(new[] { Application.streamingAssetsPath }.Concat(subPaths).ToArray());
        }

        public static string CombineWithTemporaryCache(params string[] subPaths)
        {
            return Combine(new[] { Application.temporaryCachePath }.Concat(subPaths).ToArray());
        }
    }

}