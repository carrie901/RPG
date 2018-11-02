using UnityEngine;
using System.Collections.Generic;

//=============================================================================
// Author : mashao
// CreateTime : 2017-12-28 14:24:55
// FileName : CoroutineConst.cs
//=============================================================================

namespace Summer
{
    /// <summary>
    /// 协同的常量
    /// </summary>
    public class CoroutineConst
    {
        /// <summary>
        /// 获取等待帧末
        /// </summary>
        public static readonly WaitForEndOfFrame _waitForEndOfFrame = new WaitForEndOfFrame();

        /// <summary>
        /// 获取等待帧末
        /// </summary>
        public static readonly WaitForFixedUpdate _waitForFixedUpdate = new WaitForFixedUpdate();

        /// <summary>
        /// 获取秒
        /// </summary>
        private static readonly Dictionary<float, WaitForSeconds> wait_for_seconds_dict = new Dictionary<float, WaitForSeconds>();

        /// <summary>
        /// 获取等待秒数
        /// </summary>
        public static WaitForSeconds GetWaitForSeconds(float seconds)
        {
            WaitForSeconds v;
            if (!wait_for_seconds_dict.TryGetValue(seconds, out v))
            {
                wait_for_seconds_dict.Add(seconds, new WaitForSeconds(seconds));
                v = wait_for_seconds_dict[seconds];
            }
            return v;
        }
    }
}
