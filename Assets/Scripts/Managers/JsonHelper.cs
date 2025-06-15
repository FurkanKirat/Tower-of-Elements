using System;
using Managers.Log;
using Newtonsoft.Json;

namespace Managers
{
    public static class JsonHelper
    {
        private static readonly JsonSerializerSettings IndentedSettings = new()
        {
            Formatting = Formatting.Indented,
            NullValueHandling = NullValueHandling.Ignore
        };

        private static readonly JsonSerializerSettings CompactSettings = new()
        {
            Formatting = Formatting.None,
            NullValueHandling = NullValueHandling.Ignore
        };

        

        public static T LoadFromText<T>(string jsonText)
        {
            return JsonConvert.DeserializeObject<T>(jsonText, CompactSettings);
        }
        public static T Load<T>(string filePath)
        {
            if (!FileManager.FileExists(filePath))
            {
                GameLogger.Error($"File not found: {filePath}");
                return default;
            }

            try
            {
                var json = FileManager.ReadText(filePath);
                return JsonConvert.DeserializeObject<T>(json, CompactSettings);
            }
            catch (Exception e)
            {
                GameLogger.Exception(e, $"Failed to load JSON from {filePath}");
                return default;
            }
        }
        
        public static void Save<T>(string filePath, T data, bool indented = false)
        {
            try
            {
                string directory = PathHelper.GetDirectoryName(filePath);
                FileManager.EnsureDirectoryExists(directory);
                var json = JsonConvert.SerializeObject(data, indented ? IndentedSettings : CompactSettings);
                FileManager.WriteAllText(filePath, json);
            }
            catch (Exception e)
            {
                GameLogger.Exception(e, $"Failed to save JSON to {filePath}");
            }
        }
        
        public static void SaveEncrypted<T>(T data, string path)
        {
            string json = JsonConvert.SerializeObject(data);
            string encrypted = EncryptionManager.Encrypt(json);
            FileManager.WriteAllText(path, encrypted);
        }

        public static T LoadEncrypted<T>(string path)
        {
            string encrypted = FileManager.ReadText(path);
            string json = EncryptionManager.Decrypt(encrypted);
            return JsonConvert.DeserializeObject<T>(json);
        }

    }
}