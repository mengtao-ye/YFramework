namespace YFramework
{
    public abstract class Log
    {
        public static Log Instance;
        protected abstract void LogMsg<T>(T msg);
        protected abstract void LogWarningMsg<T>(T msg);
        protected abstract void LogErrorMsg<T>(T msg);
        public static void Loger<T>(T msg)
        {
            Instance.LogMsg(msg);
        }
        public static void LogWarning<T>(T msg) {
            Instance.LogWarningMsg(msg);
        }
        public static void LogError<T>(T msg) {
            Instance.LogErrorMsg(msg);
        }

    }
}
