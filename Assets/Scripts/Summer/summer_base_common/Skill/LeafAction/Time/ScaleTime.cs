using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Summer
{
    /// <summary>
    /// 慢动作时间缩放
    /// </summary>
    public class PlayScaleTime : PlayBaseTime
    {
        public const string DES = "慢动作时间缩放";
        public float scale;
        public override void DoAction()
        {
            TimeManager.TimeScale = scale;
        }

        public override void ReAction()
        {
            TimeManager.TimeScale = 1;
        }
        public override string ToDes() { return DES; }
    }
}

