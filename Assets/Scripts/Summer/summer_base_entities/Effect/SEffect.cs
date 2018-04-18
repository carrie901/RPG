

namespace Summer
{
    /// <summary>
    /// 业务逻辑单一性质
    /// 
    /// </summary>
    public abstract class SEffect
    {
        public EffCnf _cnf;
        public BaseEntity _owner;         // 效果的拥有者

        public Buff _buff;                              // 效果属于哪一个Buff

        public virtual void Init(EffCnf eff_cnf, BaseEntity owner, Buff parent)
        {
            // cnf = obj;
            _cnf = eff_cnf;
            _owner = owner;
            _buff = parent;
            ParseParam();
        }

        // 
        public virtual void OnAttach()
        {

        }

        public virtual void OnDetach()
        {

        }

        // 解析参数
        public virtual void ParseParam()
        {
            _on_parse();
        }

        /// <summary>
        /// 参数效果
        /// </summary>
        public virtual string GetValueText()
        {
            return string.Empty;
        }

        /// <summary>
        /// 提供一个傻逼设定吧，效果想得到外部的数据
        /// </summary>
        /// <param name="data"></param>
        public virtual void ResetInfo(System.Object data)
        {

        }

        public bool OnExcute()
        {
            return _on_excute();
        }

        public void OnReverse()
        {
            _on_reverse();
        }

        public string ToDes()
        {
            return _cnf.desc;
        }

        #region 内部

        public abstract void _on_parse();

        public abstract bool _on_excute();

        public abstract void _on_reverse();

        #endregion

        #region Log
        public static void Log(string message)
        {
            if (!LogManager.open_debug_effect) return;
            LogManager.Log(message);
        }

        public static void Log(string message, params object[] args)
        {
            if (!LogManager.open_debug_effect) return;
            LogManager.Log(message, args);
        }

        public static void Error(string message)
        {
            if (!LogManager.open_debug_effect) return;
            LogManager.Error(message);
        }

        public static void Error(string message, params object[] args)
        {
            if (!LogManager.open_debug_effect) return;
            LogManager.Error(message, args);
        }

        public static void Assert(bool condition, string message)
        {
            if (!LogManager.open_debug_effect) return;
            LogManager.Assert(condition, message);
        }

        public static void Assert(bool condition, string message, params object[] args)
        {
            if (!LogManager.open_debug_effect) return;
            LogManager.Assert(condition, message, args);
        }

        #endregion
    }

    public static class EffectLog
    {
        public static void Log(string message)
        {
            if (!LogManager.open_debug_effect) return;
            LogManager.Log(message);
        }

        public static void Log(string message, params object[] args)
        {
            if (!LogManager.open_debug_effect) return;
            LogManager.Log(message, args);
        }

        public static void Error(string message)
        {
            if (!LogManager.open_debug_effect) return;
            LogManager.Error(message);
        }

        public static void Error(string message, params object[] args)
        {
            if (!LogManager.open_debug_effect) return;
            LogManager.Error(message, args);
        }

        public static void Assert(bool condition, string message)
        {
            if (!LogManager.open_debug_effect) return;
            LogManager.Assert(condition, message);
        }

        public static void Assert(bool condition, string message, params object[] args)
        {
            if (!LogManager.open_debug_effect) return;
            LogManager.Assert(condition, message, args);
        }

    }
}