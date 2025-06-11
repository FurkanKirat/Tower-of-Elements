using System;
using Managers.Log;
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
                GameLogger.Exception(e, $"Failed to load JSON from {filePath}");
                return default;
            }
        }
        
        public static void Save<T>(string filePath, T data, bool isIndented = false)
        {
            try
            {
                string directory = PathHelper.GetDirectoryName(filePath);
                FileManager.EnsureDirectoryExists(directory);
                var json = JsonConvert.SerializeObject(data, Settings);
                FileManager.WriteAllText(filePath, json);
            }
            catch (Exception e)
            {
                GameLogger.Exception(e, $"Failed to save JSON to {filePath}");
            }
        }
    }
}