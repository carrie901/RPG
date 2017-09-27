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
// 1.日子的级别
// 2.输出包含的指定信息
// 3.信息过滤问题,方便查看对应的信息
//=============================================================================

namespace Summer
{
    public class LogManager
    {
        public static bool open_debug = true;
        public static List<ILog> pipelines = new List<ILog>();

        static LogManager()
        {
#if UNITY_EDITOR
            pipelines.Add(FileLog.Instance);
#endif
            pipelines.Add(UnityLog.Instance);
        }

        public static void Log(string message)
        {
            if (!IsOpenDebug()) return;
            int count = pipelines.Count;
            for (int i = 0; i < count; i++)
                pipelines[i].Log(message);
        }

        public static void Log(string message, params object[] args)
        {
            if (!IsOpenDebug()) return;
            int count = pipelines.Count;
            for (int i = 0; i < count; i++)
                pipelines[i].Log(message, args);
        }

        /*public static void Waring(string message)
        {

        }*/

        public static void Warning(string message, params object[] args)
        {
            if (!IsOpenDebug()) return;
            int count = pipelines.Count;
            for (int i = 0; i < count; i++)
                pipelines[i].Warning(message, args);
        }

        /*public static void Error(string message)
        {

        }*/

        public static void Error(string message, params object[] args)
        {
            if (!IsOpenDebug()) return;
            int count = pipelines.Count;
            for (int i = 0; i < count; i++)
                pipelines[i].Error(message, args);
        }

        /*public static void Assert(bool condition, string message)
        {

        }*/

        public static void Assert(bool condition, string message, params object[] args)
        {
            if (!IsOpenDebug()) return;
            if (condition) return;
            int count = pipelines.Count;
            for (int i = 0; i < count; i++)
                pipelines[i].Assert(false, message, args);
        }

        public static float begin_time = 0f;
        public static string begin_time_des = string.Empty;
        public static void BeginTime(string des = "")
        {
            begin_time = Time.realtimeSinceStartup;
            begin_time_des = des;
        }

        public static void EndTime()
        {
            begin_time = Time.realtimeSinceStartup - begin_time;
            Log(begin_time_des + "耗时:[" + begin_time + "]");
        }

        private static bool IsOpenDebug()
        {
            return open_debug;
        }

        //TODO 特殊的时间，后面从别的地方拿，目前只适用于关卡
        public static float level_time()
        {
            //return LevelTimeModule.LevelTimeCost;
            return 0;
        }
    }

    public enum EDebugLevel
    {
        e_none,
        e_log,
        e_waring,
        e_error,
        e_asset,
        e_max,
    }

}
