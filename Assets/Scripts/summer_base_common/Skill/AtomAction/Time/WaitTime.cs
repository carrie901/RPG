using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Summer
{
    /// <summary>
    /// 等待一定的时间之后发出时间
    /// </summary>
    public class WaitTime : PlayBaseTime
    {
        public string finish_event;
        public override void DoAction()
        {

        }

        public override void ReAction()
        {
            OnSendEvent(finish_event);
        }
    }

}
