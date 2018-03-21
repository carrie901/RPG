using UnityEngine;
using System.Collections;

//=============================================================================
// Author : mashao
// CreateTime : 2017-12-27 17:14:59
// FileName : FadeEffect.cs
//=============================================================================

namespace Summer
{
    /// <summary>
    /// 淡入淡出效果 淡入-停留，淡出
    /// </summary>
    public class FadeEffect
    {
        public const float MAX = 1;
        public float _all_time;                             // 总时间
        public float _fade_in;                              // 淡入时间
        public float _fade_out;                             // 淡出时间 起点 这里的_fade_out=all_time-fade_out
        public float _cur_time;                             // 当前累加时间
        public bool _start = false;
        public void Set(float all_time, float fade_in, float fade_out)
        {
            _all_time = all_time;
            _fade_out = _all_time - fade_out;
            _fade_in = fade_in;
            _start = true;
            if (_fade_out < 0)
            {
                LogManager.Error("淡入淡出效果参数异常.All_Time:[{0}],Fade_In:[{1}],Fade_Out:[{2}]", all_time, fade_in, fade_out);
                _start = false;
            }

            if (MathHelper.IsZero(fade_in) && MathHelper.IsZero(fade_out))
                _start = false;
        }

        public void Reset()
        {
            _all_time = 0;
            _fade_in = 0;
            _fade_out = 0;
            _cur_time = 0;
            _start = false;
        }

        public float OnUpdate(float time)
        {
            if (!_start) return 1f;
            float rate;
            _cur_time += time;
            if (_cur_time < _fade_in)
            {
                rate = _cur_time / _fade_in;
            }
            else if (_cur_time >= _fade_in && _cur_time <= (_fade_out))
            {
                return MAX;
            }
            else
            {
                rate = (_cur_time - _fade_out) / (_all_time - _fade_out);
            }

            // 有效性验证 在0-1之间
            rate = Mathf.Clamp01(rate);
            return rate;
        }
    }
}
