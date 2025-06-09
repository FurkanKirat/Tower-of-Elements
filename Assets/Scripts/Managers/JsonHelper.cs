using System;
using Newtonsoft.Json;

namespace Managers
{
    public static class JsonHelper
    {
        private static readonly JsonSerializerSettings Settings = new()
        {
            Formatting = Formatting.Indented,
            NullValueHandling = NullValueHandling.Ignore
        };

        public static T LoadFromText<T>(string jsonText)
        {
            return JsonConvert.DeserializeObject<T>(jsonText, Settings);
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
                return JsonConvert.DeserializeObject<T>(json, Settings);
            }
            catch (Exception e)
            {
                GameLogger.Error($"Failed to load JSON from {filePath}: {e.Message}");
                return default;
            }
        }
        
        public static void Save<T>(string filePath, T data)
        {
            try
            {
                FileManager.EnsureDirectoryExists(filePath);
                var json = JsonConvert.SerializeObject(data, Settings);
                FileManager.WriteText(filePath, json);
            }
            catch (Exception e)
            {
                GameLogger.Error($"Failed to save JSON to {filePath}: {e.Message}");
            }
        }
        
        
    }
}