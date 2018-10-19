using System.Collections.Generic;
using UnityEngine;

//=============================================================================
// Author : msm 
// CreateTime : 2017-5-31 15:50:49
// FileName : LogManager.cs
// 日志输出类
// 1.增加debug管道，可以调整UnityDebug和FileDebug，等不同类型的Debug管道
// 2.String.Format的性能消耗蛮大的，通过关闭外部的opengDebug属性来关闭增加Debug日志的输出，同时也屏蔽原有的String.Format的性能消耗
// 
// 后续功能(也可以通过其他手段,针对File文件信息，进行信息过滤)
// 1.日志的级别
// 2.输出包含的指定信息
// 3.信息过滤问题,方便查看对应的信息
//=============================================================================

namespace Summer
{
    /// <summary>
    /// 有什么好的办法处理模型性质的Log
    /// </summary>
    public class LogManager
    {
        #region 属性

        public static bool OpenDebug = true;

        public static bool ErrorLog = false;                       // 网络的错误日志

        public static bool _openNet = true;
        public static bool _openDebugBuff = false;
        public static bool _openDebugEffect = true;
        public static bool _openLoadRes = true;
        public static bool _openPanel = true;
        public static bool _openSkill = true;
        public static bool _opneEntityAction = true;
        public static bool _animation = true;

        /// <summary>
        /// 能显示的级别
        /// </summary>
        public static int _errorLevel = ASSET;   // none=0,log=1,waring=2,error=3,asset=4

        #region 日志级别

        public const int NONE = 0;
        public const int LOG = 1;
        public const int WARING = 2;
        public const int ERROR = 3;
        public const int ASSET = 4;

        #endregion

        #region Log通道

        public static List<I_Log> _pipelines = new List<I_Log>();

        #endregion

        #endregion

        #region 初始化

        static LogManager()
        {
#if UNITY_EDITOR
            //_pipelines.Add(FileLog.Instance);
            _pipelines.Add(StringBuilderLog.Instance);
            _pipelines.Add(UnityLog.Instance);
            //_pipelines.Add(RuntimeLog.Instance);
#endif
            //Debug.logger.logEnabled = !IgnoreUnityDebug;

        }

        #endregion

        #region 日志

        public static void Quit()
        {
            int count = _pipelines.Count;
            for (int i = 0; i < count; i++)
                _pipelines[i].Quit();
        }
        [System.Diagnostics.Conditional("LOG")]
        public static void Log(string message)
        {
            if (!IsOpenDebug()) return;
            if (_errorLevel < NONE) return;
            int count = _pipelines.Count;
            for (int i = 0; i < count; i++)
                _pipelines[i].Log(message);
        }
        [System.Diagnostics.Conditional("LOG")]
        public static void Log(string message, params object[] args)
        {
            if (!IsOpenDebug()) return;
            if (_errorLevel < NONE) return;
            int count = _pipelines.Count;
            for (int i = 0; i < count; i++)
                _pipelines[i].Log(message, args);
        }
        [System.Diagnostics.Conditional("LOG")]
        public static void Waring(string message)
        {
            if (!IsOpenDebug()) return;
            if (_errorLevel < WARING) return;
            int count = _pipelines.Count;
            for (int i = 0; i < count; i++)
                _pipelines[i].Waring(message);
        }
        [System.Diagnostics.Conditional("LOG")]
        public static void Warning(string message, params object[] args)
        {
            if (!IsOpenDebug()) return;
            if (_errorLevel < WARING) return;
            int count = _pipelines.Count;
            for (int i = 0; i < count; i++)
                _pipelines[i].Warning(message, args);
        }
        [System.Diagnostics.Conditional("LOG")]
        public static void Error(string message)
        {
            if (!IsOpenDebug()) return;
            if (_errorLevel < ERROR) return;
            int count = _pipelines.Count;
            for (int i = 0; i < count; i++)
                _pipelines[i].Error(message);
        }
        [System.Diagnostics.Conditional("LOG")]
        public static void Error(string message, params object[] args)
        {
            if (!IsOpenDebug()) return;
            if (_errorLevel < ERROR) return;
            int count = _pipelines.Count;
            for (int i = 0; i < count; i++)
                _pipelines[i].Error(message, args);
        }
        [System.Diagnostics.Conditional("LOG")]
        public static void Assert(bool condition, string message)
        {
            if (!IsOpenDebug()) return;
            if (_errorLevel < ERROR) return;
            int count = _pipelines.Count;
            for (int i = 0; i < count; i++)
                _pipelines[i].Assert(condition, message);
        }
        [System.Diagnostics.Conditional("LOG")]
        public static void Assert(bool condition, string message, params object[] args)
        {
            if (!IsOpenDebug()) return;
            if (_errorLevel < ERROR) return;
            int count = _pipelines.Count;
            for (int i = 0; i < count; i++)
                _pipelines[i].Assert(condition, message, args);
        }

        #endregion

        #region private

        private static bool IsOpenDebug()
        {
            return OpenDebug;
        }

        #endregion
    }
}
