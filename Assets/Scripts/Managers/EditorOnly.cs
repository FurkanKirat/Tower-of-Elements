using System;
using System.Diagnostics;

namespace Managers
{
    public static class EditorOnly
    {
        public const bool IsEditor = 
#if UNITY_EDITOR
        true;
#else
        false;
#endif
            
            [Conditional("UNITY_EDITOR")]
        public static void Execute(Action action)
        {
#if UNITY_EDITOR
            action?.Invoke();
#endif
        }

        public static T Execute<T>(Func<T> func, T fallback = default)
        {
#if UNITY_EDITOR
                return func != null ? func() : fallback;
#else
                return fallback;
#endif
        }
    }
}