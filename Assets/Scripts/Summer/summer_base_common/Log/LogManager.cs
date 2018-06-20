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

        public static bool open_debug = true;

        public static bool open_debug_buff = false;
        public static bool open_debug_effect = false;
        public static bool open_load_res = true;
        public static bool open_send_notification = false;
        public static bool open_plot = false;
        public static bool open_skill = false;
        public static bool opne_entity_action = true;
        public static bool animation = true;

        public static List<I_Log> pipelines = new List<I_Log>();

        #region 日志级别

        public static int error_level = ASSET;   // none=0,log=1,waring=2,error=3,asset=4
        public const int NONE = 0;
        public const int LOG = 1;
        public const int WARING = 2;
        public const int ERROR = 3;
        public const int ASSET = 4;

        #endregion

        #endregion

        #region 初始化

        static LogManager()
        {
#if UNITY_EDITOR
            //pipelines.Add(FileLog.Instance);
            //pipelines.Add(StringBuilderLog.Instance);
            pipelines.Add(UnityLog.Instance);
            //pipelines.Add(RuntimeLog.Instance);
#endif

        }

        #endregion

        #region 日志

        public static void Quit()
        {
            int count = pipelines.Count;
            for (int i = 0; i < count; i++)
                pipelines[i].Quit();
        }

        public static void Log(string message)
        {
            if (!IsOpenDebug()) return;
            if (error_level < NONE) return;
            int count = pipelines.Count;
            for (int i = 0; i < count; i++)
                pipelines[i].Log(message);
        }

        public static void Log(string message, params object[] args)
        {
            if (!IsOpenDebug()) return;
            if (error_level < NONE) return;
            int count = pipelines.Count;
            for (int i = 0; i < count; i++)
                pipelines[i].Log(message, args);
        }

        public static void Waring(string message)
        {
            if (!IsOpenDebug()) return;
            if (error_level < WARING) return;
            int count = pipelines.Count;
            for (int i = 0; i < count; i++)
                pipelines[i].Waring(message);
        }

        public static void Warning(string message, params object[] args)
        {
            if (!IsOpenDebug()) return;
            if (error_level < WARING) return;
            int count = pipelines.Count;
            for (int i = 0; i < count; i++)
                pipelines[i].Warning(message, args);
        }

        public static void Error(string message)
        {
            if (!IsOpenDebug()) return;
            if (error_level < ERROR) return;
            int count = pipelines.Count;
            for (int i = 0; i < count; i++)
                pipelines[i].Error(message);
        }

        public static void Error(string message, params object[] args)
        {
            if (!IsOpenDebug()) return;
            if (error_level < ERROR) return;
            int count = pipelines.Count;
            for (int i = 0; i < count; i++)
                pipelines[i].Error(message, args);
        }

        public static bool Assert(bool condition, string message)
        {
            if (!IsOpenDebug()) return condition;
            if (error_level < ERROR) return condition;
            int count = pipelines.Count;
            for (int i = 0; i < count; i++)
                pipelines[i].Assert(condition, message);
            return condition;
        }

        public static bool Assert(bool condition, string message, params object[] args)
        {
            if (!IsOpenDebug()) return condition;
            if (error_level < ERROR) return condition;
            int count = pipelines.Count;
            for (int i = 0; i < count; i++)
                pipelines[i].Assert(condition, message, args);
            return condition;
        }

        #endregion

        #region private

        private static bool IsOpenDebug()
        {
            return open_debug;
        }

        #endregion
    }
}
