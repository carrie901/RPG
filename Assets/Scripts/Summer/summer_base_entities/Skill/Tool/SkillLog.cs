using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Summer
{
    public class SkillLog
    {
        public static void Log(string message, params object[] args)
        {
            if (!LogManager.open_skill) return;
            LogManager.Log(message, args);
        }

        public static void Assert(bool condition, string message)
        {
            if (!LogManager.open_skill) return;
            LogManager.Assert(condition, message);
        }

        public static void Assert(bool condition, string message, params object[] args)
        {
            if (!LogManager.open_skill) return;
            LogManager.Assert(condition, message, args);
        }

        #region 日志

        public static void LogEnter(SkillNode node)
        {
            if (!LogManager.open_skill) return;
            //LogManager.Log("Time: {1}   Enter [{0}] 节点", node.ToDes(), TimeManager.FrameCount);
        }

        public static void LogExit(SkillNode node)
        {
            if (!LogManager.open_skill) return;
            //LogManager.Log("Time: {1}   Exit [{0}] 节点,开始跳转到下一个节点 ", node.ToDes(), TimeManager.FrameCount);
        }

        public static void LogStart(SkillNode node)
        {
            if (!LogManager.open_skill) return;
            LogManager.Log("Time: {1}   OnStart：[{0}] 节点,接受[{2}]事件", node.ToDes(), TimeManager.FrameCount, node._start_transition);


        }

        #endregion


    }
}

