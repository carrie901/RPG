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
        private float _amplitude;           //幅度
        private float _periodTime;          //周期
        private float _decay;               //衰减

        public ShakeFunc(float amplitude, int frequency, float lastTime)
        {
            _amplitude = amplitude;
            _periodTime = 1.0f / (float)frequency;
            _decay = amplitude / (lastTime / _periodTime);
        }

        public float _timeFromStart;

        public bool IsEnd()
        {
            return GetAmpWithSinCurve(_timeFromStart, _amplitude, _periodTime, _decay) <= 0;
        }

        public float Get(float deltaTime)
        {
            _timeFromStart = _timeFromStart + deltaTime;
            float retVal = GetWithSinCurve(_timeFromStart, _amplitude, _periodTime, _decay);
            return retVal;
        }

        public static float GetAmpWithSinCurve(float time, float amplitude, float periodTime, float decay)
        {
            return amplitude - decay * time / periodTime;
        }

        public static float GetWithSinCurve(float time, float amplitude, float periodTime, float decay)
        {
            float curPeriod = time / periodTime;
            return GetAmpWithSinCurve(time, amplitude, periodTime, decay) * Mathf.Sin(2 * Mathf.PI * curPeriod);
        }
    }
}