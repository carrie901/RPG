using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Summer
{
    /// <summary>
    /// 过程化时间动作，在开始和结束设置回调
    /// </summary>
    public abstract class PlayBaseTime : ASkillAction
    {
        public float duration;

        public bool _real_time;         //真实时间
        public float _start_time;       //开始的时间
        public float _timer;            //累加的时间

        public override void OnEnter()
        {


            if (duration < 0)
            {
                LogManager.Assert(duration >= 0, "PlayBaseTime 间隔时间必须大于0");
                DoAction();
                ReAction();
                Finish();
                return;
            }

            _start_time = TimeManager.RealtimeSinceStartup;
            _timer = 0;
            DoAction();
        }

        public override void OnExit()
        {
            _start_time = Time.realtimeSinceStartup;
            _timer = 0;
        }

        public override void OnUpdate(float dt)
        {
            _timer += dt;
            if (_timer >= duration)
            {
                ReAction();
                Finish();
            }
        }

        public abstract void DoAction();

        public abstract void ReAction();
    }

}
