using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Summer
{

    /// <summary>
    /// 震动需要关注的点
    /// 1.方向
    /// 2.角度
    /// 3.距离
    /// 4.速度
    /// 5.衰减
    /// 6.是否收到time影响
    /// 7.开始/结束
    /// </summary>
    public class ShakeFunc
    {
        public float amplitude;             //幅度
        public float period_time;           //周期
        public float decay;                 //衰减

        public ShakeFunc(float amplitude, int frequency, float last_time)
        {
            this.amplitude = amplitude;
            period_time = 1.0f/(float) frequency;
            decay = amplitude/(last_time/period_time);
        }

        public float _time_from_start = 0;

        public bool IsEnd()
        {
            return GetAmpWithSinCurve(_time_from_start, amplitude, period_time, decay) <= 0;
        }

        public float Get(float delta_time)
        {
            _time_from_start = _time_from_start + delta_time;
            float ret_val = GetWithSinCurve(_time_from_start, amplitude, period_time, decay);
            return ret_val;
        }

        public static float GetAmpWithSinCurve(float time, float amplitude, float period_time, float decay)
        {
            return amplitude - decay * time / period_time;
        }

        public static float GetWithSinCurve(float time, float amplitude, float period_time, float decay)
        {
            float cur_period = time / period_time;
            return GetAmpWithSinCurve(time, amplitude, period_time, decay) * Mathf.Sin(2 * Mathf.PI * cur_period);
        }


    }
}