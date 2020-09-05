namespace Whatever
{
    public class Debug
    {
        private static bool isOpenLog = true;

        public static void LogError(object message)
        {
            if (!isOpenLog)
                return;
            UnityEngine.Debug.LogError(message);
        }

        public static void LogWarning(object message)
        {
            if (!isOpenLog)
                return;
            UnityEngine.Debug.LogWarning(message);
        }

        public static void Log(object message)
        {
            if (!isOpenLog)
                return;

#if UNITY_IPHONE || UNITY_ANDROID

#else
            UnityEngine.Debug.Log(message);
#endif
        }

        public static void PopLog(object message)
        {
            if (!isOpenLog)
                return;
            //todo something
            UnityEngine.Debug.Log(message);
        }

    }

}