using System;
using UnityEngine;

namespace Managers
{
    public static class GameLogger
    {
        private static bool _logToConsole = EditorOnly.IsEditor;
        private static bool _logToFile = true;
        
        public static void SetLogToConsole(bool enabled) => _logToConsole = enabled;
        public static void SetLogToFile(bool enabled) => _logToFile = enabled;
        
        public static void Log(string message) =>
            LogInternal("INFO", message, () => EditorOnly.Execute(() => Debug.Log(message)));

        public static void Warn(string message) =>
            LogInternal("WARN", message, () => EditorOnly.Execute(() => Debug.LogWarning(message)));

        public static void Error(string message) =>
            LogInternal("ERROR", message, () => EditorOnly.Execute(() => Debug.LogError(message)));

        public static void Exception(Exception ex, string context = null)
        {
            string fullMessage = $"{context ?? "Exception"}\n{ex}";
            LogInternal("EXCEPTION", fullMessage, () => EditorOnly.Execute(() => Debug.LogException(ex)));
        }
        
        
        private static void LogInternal(string level, string message, Action logToConsole)
        {
            if (_logToConsole)
                logToConsole?.Invoke();

            if (_logToFile)
                TryWriteToFile(level, message);
        }
        
        private static void TryWriteToFile(string level, string message)
        {
            try
            {
                string logPath = PathHelper.CombineWithPersistentData("logs", "game.log");
                FileManager.EnsureDirectoryExists(FileManager.GetDirectoryName(logPath));

                string logLine = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{level}] {message}\n";
                FileManager.AppendText(logPath, logLine);
            }
            catch (Exception ex)
            {
                EditorOnly.Execute(() =>
                    Debug.LogWarning($"[GameLogger] Log yazılamadı: {ex.Message}")
                );
            }
        }
    }
}