
namespace Summer
{
    public class ActionLog
    {
        public static void Log(string message)
        {
            if (!LogManager.opne_entity_action) return;
            LogManager.Log(message);
        }

        public static void Log(string message, params object[] args)
        {
            if (!LogManager.opne_entity_action) return;
            LogManager.Log(message, args);
        }

        public static void Assert(bool condition, string message)
        {
            if (!LogManager.opne_entity_action) return;
            LogManager.Assert(condition, message);
        }


        public static void Assert(bool condition, string message, params object[] args)
        {
            if (!LogManager.opne_entity_action) return;
            LogManager.Assert(condition, message, args);
        }

        public static void Error(string message)
        {
            if (!LogManager.opne_entity_action) return;
            LogManager.Error(message);
        }


        public static void Error(string message, params object[] args)
        {
            if (!LogManager.opne_entity_action) return;
            LogManager.Error(message, args);
        }
    }
}
