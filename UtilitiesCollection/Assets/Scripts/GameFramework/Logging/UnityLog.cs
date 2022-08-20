namespace GameFramework.Logging
{
    public static class UnityLog
    {
        static UnityLog()
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD || ENABLE_LOG
            UnityEngine.Debug.unityLogger.logEnabled = true;
#else
		UnityEngine.Debug.unityLogger.logEnabled = false;		
#endif
        }

        [System.Diagnostics.Conditional("UNITY_EDITOR"), System.Diagnostics.Conditional("DEVELOPMENT_BUILD"), System.Diagnostics.Conditional("ENABLE_LOG")]
        public static void LogI(object obj)
        {
            UnityEngine.Debug.Log(obj);
        }

        [System.Diagnostics.Conditional("UNITY_EDITOR"), System.Diagnostics.Conditional("DEVELOPMENT_BUILD"), System.Diagnostics.Conditional("ENABLE_LOG")]
        public static void LogW(object obj)
        {
            UnityEngine.Debug.LogWarning(obj);
        }

        [System.Diagnostics.Conditional("UNITY_EDITOR"), System.Diagnostics.Conditional("DEVELOPMENT_BUILD"), System.Diagnostics.Conditional("ENABLE_LOG")]
        public static void LogE(object obj)
        {
            UnityEngine.Debug.LogError(obj);
        }

        [System.Diagnostics.Conditional("UNITY_EDITOR"), System.Diagnostics.Conditional("DEVELOPMENT_BUILD"), System.Diagnostics.Conditional("ENABLE_LOG")]
        public static void LogIFormat(string format, params object[] args)
        {
            UnityEngine.Debug.LogFormat(format, args);
        }

        [System.Diagnostics.Conditional("UNITY_EDITOR"), System.Diagnostics.Conditional("DEVELOPMENT_BUILD"), System.Diagnostics.Conditional("ENABLE_LOG")]
        public static void LogWFormat(string format, params object[] args)
        {
            UnityEngine.Debug.LogWarningFormat(format, args);
        }

        [System.Diagnostics.Conditional("UNITY_EDITOR"), System.Diagnostics.Conditional("DEVELOPMENT_BUILD"), System.Diagnostics.Conditional("ENABLE_LOG")]
        public static void LogEFormat(string format, params object[] args)
        {
            UnityEngine.Debug.LogErrorFormat(format, args);
        }

        [System.Diagnostics.Conditional("UNITY_EDITOR"), System.Diagnostics.Conditional("DEVELOPMENT_BUILD"), System.Diagnostics.Conditional("ENABLE_LOG")]
        public static void LogI(string tag, object obj)
        {
            LogI($"[{tag}] {obj}");
        }

        [System.Diagnostics.Conditional("UNITY_EDITOR"), System.Diagnostics.Conditional("DEVELOPMENT_BUILD"), System.Diagnostics.Conditional("ENABLE_LOG")]
        public static void LogW(string tag, object obj)
        {
            LogW($"[{tag}] {obj}");
        }

        [System.Diagnostics.Conditional("UNITY_EDITOR"), System.Diagnostics.Conditional("DEVELOPMENT_BUILD"), System.Diagnostics.Conditional("ENABLE_LOG")]
        public static void LogE(string tag, object obj)
        {
            LogE($"[{tag}] {obj}");
        }

        [System.Diagnostics.Conditional("UNITY_EDITOR"), System.Diagnostics.Conditional("DEVELOPMENT_BUILD"), System.Diagnostics.Conditional("ENABLE_LOG")]
        public static void LogIFormat(string tag, string format, params object[] args)
        {
            LogIFormat($"[{tag}] {format}", args);
        }

        [System.Diagnostics.Conditional("UNITY_EDITOR"), System.Diagnostics.Conditional("DEVELOPMENT_BUILD"), System.Diagnostics.Conditional("ENABLE_LOG")]
        public static void LogWFormat(string tag, string format, params object[] args)
        {
            LogWFormat($"[{tag}] {format}", args);
        }

        [System.Diagnostics.Conditional("UNITY_EDITOR"), System.Diagnostics.Conditional("DEVELOPMENT_BUILD"), System.Diagnostics.Conditional("ENABLE_LOG")]
        public static void LogEFormat(string tag, string format, params object[] args)
        {
            LogEFormat($"[{tag}] {format}", args);
        }

        [System.Diagnostics.Conditional("UNITY_EDITOR"), System.Diagnostics.Conditional("DEVELOPMENT_BUILD"), System.Diagnostics.Conditional("ENABLE_LOG")]
        public static void LogI(this object any, object obj)
        {
            LogI($"{any.GetType()}", obj);
        }

        [System.Diagnostics.Conditional("UNITY_EDITOR"), System.Diagnostics.Conditional("DEVELOPMENT_BUILD"), System.Diagnostics.Conditional("ENABLE_LOG")]
        public static void LogW(this object any, object obj)
        {
            LogE($"{any.GetType()}", obj);
        }

        [System.Diagnostics.Conditional("UNITY_EDITOR"), System.Diagnostics.Conditional("DEVELOPMENT_BUILD"), System.Diagnostics.Conditional("ENABLE_LOG")]
        public static void LogE(this object any, object obj)
        {
            LogE($"{any.GetType()}", obj);
        }
    }
}
