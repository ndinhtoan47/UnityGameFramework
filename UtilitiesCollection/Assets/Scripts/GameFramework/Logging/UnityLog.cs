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
    }

}

