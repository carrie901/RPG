﻿
namespace Summer
{
    public class ResLog
    {
        #region Log
        public static void Log(string message)
        {
            if (!LogManager._openLoadRes) return;
            LogManager.Log(message);
        }

        public static void Log(string message, params object[] args)
        {
            if (!LogManager._openLoadRes) return;
            LogManager.Log(message, args);
        }

        public static void Error(string message)
        {
            if (!LogManager._openLoadRes) return;
            LogManager.Error(message);
        }

        public static void Error(string message, params object[] args)
        {
            if (!LogManager._openLoadRes) return;
            LogManager.Error(message, args);
        }


        public static bool Assert(bool condition, string message)
        {
            if (!LogManager._openLoadRes) return condition;
            LogManager.Assert(condition, message);
            return condition;
        }

        public static bool Assert(bool condition, string message, params object[] args)
        {
            if (!LogManager._openLoadRes) return condition;
            LogManager.Assert(condition, message, args);
            return condition;
        }

        #endregion
    }
}
