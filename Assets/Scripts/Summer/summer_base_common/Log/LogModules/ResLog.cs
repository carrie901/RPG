
namespace Summer
{
    public class ResLog
    {
        public readonly static bool s_stepLog = false;
        [System.Diagnostics.Conditional("LOG")]
        public static void LogSetp(string message)
        {
            if (!s_stepLog) return;
            Log(message);
        }
        [System.Diagnostics.Conditional("LOG")]
        public static void LogSetp(string message, params object[] args)
        {
            if (!s_stepLog) return;
            Log(message, args);
        }

        #region Log
        [System.Diagnostics.Conditional("LOG")]
        public static void Log(string message)
        {
            if (!LogManager._openLoadRes) return;
            LogManager.Log(message);
        }
        [System.Diagnostics.Conditional("LOG")]
        public static void Log(string message, params object[] args)
        {
            if (!LogManager._openLoadRes) return;
            LogManager.Log(message, args);
        }
        [System.Diagnostics.Conditional("LOG")]
        public static void Error(string message)
        {
            if (!LogManager._openLoadRes) return;
            LogManager.Error(message);
        }
        [System.Diagnostics.Conditional("LOG")]
        public static void Error(string message, params object[] args)
        {
            if (!LogManager._openLoadRes) return;
            LogManager.Error(message, args);
        }

        [System.Diagnostics.Conditional("LOG")]
        public static void Assert(bool condition, string message)
        {
            if (!LogManager._openLoadRes)return;
            LogManager.Assert(condition, message);
        }

        [System.Diagnostics.Conditional("LOG")]
        public static void Assert(bool condition, string message, params object[] args)
        {
            if (!LogManager._openLoadRes) return;
            LogManager.Assert(condition, message, args);
        }

        #endregion
    }
}
