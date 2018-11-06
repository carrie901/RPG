using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Summer
{
    public class SkillLog
    {
        [System.Diagnostics.Conditional("LOG")]
        public static void Log(string message, params object[] args)
        {
            if (!LogManager._openSkill) return;
            LogManager.Log(message, args);
        }
        [System.Diagnostics.Conditional("LOG")]
        public static void Assert(bool condition, string message)
        {
            if (!LogManager._openSkill) return;
            LogManager.Assert(condition, message);
        }
        [System.Diagnostics.Conditional("LOG")]
        public static void Assert(bool condition, string message, params object[] args)
        {
            if (!LogManager._openSkill) return;
            LogManager.Assert(condition, message, args);
        }

        #region 日志

        public static void LogEnter(SkillNode node)
        {
            if (!LogManager._openSkill) return;
            //LogManager.Log("Time: {1}   Enter [{0}] 节点", node.ToDes(), TimeManager.FrameCount);
        }

        public static void LogExit(SkillNode node)
        {
            if (!LogManager._openSkill) return;
            //LogManager.Log("Time: {1}   Exit [{0}] 节点,开始跳转到下一个节点 ", node.ToDes(), TimeManager.FrameCount);
        }

        public static void LogStart(SkillNode node)
        {
            if (!LogManager._openSkill) return;
            LogManager.Log("Time: {1}    进入[{0}]节点,同时这个节点接受[{2}]事件", node.ToDes(), TimeModule.FrameCount, node._start_transition);
        }

        #endregion


    }
}

