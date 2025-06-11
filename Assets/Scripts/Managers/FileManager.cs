using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Managers.Log;
using UnityEditor;
using UnityEngine;

namespace Managers
{
    public static class FileManager
    {
        public static bool FileExists(string path) => 
            File.Exists(path);
        
        public static void WriteAllText(string path, string content) => 
            File.WriteAllText(path, content);
        
        public static void AppendText(string path, string content) =>
            File.AppendAllText(path, content);
        
        public static string ReadText(string path) => 
            File.ReadAllText(path);

        public static void DeleteFile(string path)
        {
#if UNITY_EDITOR
            if (path.StartsWith(Application.dataPath))
            {
                string unityPath = path.Replace(Application.dataPath, "Assets").Replace("\\", "/");
                if (AssetDatabase.DeleteAsset(unityPath))
                    GameLogger.Log($"Deleted asset: {unityPath}");
                else
                    GameLogger.Warn($"Failed to delete asset: {unityPath}");
                
                string metaPath = path + ".meta";
                if (File.Exists(metaPath))
                    File.Delete(metaPath);
                
                AssetDatabase.Refresh();
                Resources.UnloadUnusedAssets(); 
            }
            else
#endif
            {
                if (File.Exists(path))
                    File.Delete(path);
            }
        }

        
        public static List<string> GetFilesInDirectory(string directory, string extension = ".json") => 
            Directory.Exists(directory) ? 
                Directory.EnumerateFiles(directory)
                .Where(file => Path.GetExtension(file).Equals(extension, StringComparison.OrdinalIgnoreCase))
                .ToList() : new List<string>();
        
        public static void EnsureDirectoryExists(string directory)
        {
            if (DirectoryExists(directory)) return;
            CreateDirectory(directory);
        }

        public static bool DirectoryExists(string directory) =>
            Directory.Exists(directory);
        
        public static void CreateDirectory(string directory) =>
            Directory.CreateDirectory(directory);
        
        public static void DeleteDirectory(string directory) =>
            Directory.Delete(directory);
        
        public static string GetDirectoryName(string directory) =>
            Path.GetDirectoryName(directory);
        
    }
}