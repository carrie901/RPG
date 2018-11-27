
//
//                            _ooOoo_
//                           o8888888o
//                           88" . "88
//                           (| -_- |)
//                           O\  =  /O
//                        ____/`---'\____
//                      .'  \\|     |//  `.
//                     /  \\|||  :  |||//  \
//                    /  _||||| -:- |||||-  \
//                    |   | \\\  -  /// |   |
//                    | \_|  ''\---/''  |   |
//                    \  .-\__  `-`  ___/-. /
//                  ___`. .'  /--.--\  `. . __
//               ."" '<  `.___\_<|>_/___.'  >'"".
//              | | :  `- \`.;`\ _ /`;.`/ - ` : | |
//              \  \ `-.   \_ __\ /__ _/   .-` /  /
//         ======`-.____`-.___\_____/___.-`____.-'======
//                            `=---='
//        ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
//                 			 佛祖 保佑             

using System;
using UnityEngine;

namespace Summer
{
    public abstract class UTweener : MonoBehaviour
    {

        #region 属性

        public enum Method
        {
            Linear,
            EaseIn,
            EaseOut,
            EaseInOut,
            BounceIn,
            BounceOut,
        }

        public enum Style
        {
            Once,
            Loop,
            PingPong,
        }

        [HideInInspector]
        public Method _method = Method.Linear;
        [HideInInspector]
        public Style _style = Style.Once;
        [HideInInspector]
        public float _delay = 0f;
        [HideInInspector]
        public float _duration = 1f;

        bool _mStarted;
        float _mStartTime;
        float _mDuration;
        float _mAmountPerDelta = 1000f;
        float _mFactor;

        public float AmountPerDelta
        {
            get
            {
                if (Math.Abs(_mDuration - _duration) > float.Epsilon)
                {
                    _mDuration = _duration;
                    _mAmountPerDelta = Mathf.Abs((_duration > 0f) ? 1f / _duration : 1000f) * Mathf.Sign(_mAmountPerDelta);
                }
                return _mAmountPerDelta;
            }
        }
        public float TweenFactor { get { return _mFactor; } set { _mFactor = Mathf.Clamp01(value); } }

        void Reset()
        {
            if (!_mStarted)
            {
                SetStartToCurrentValue();
                SetEndToCurrentValue();
            }
        }
        #endregion

        #region MONO Override

        protected virtual void Start() { Update(); }

        void Update()
        {
            float delta = Time.deltaTime;
            float time = Time.time;

            if (!_mStarted)
            {
                _mStarted = true;
                _mStartTime = time + _delay;
            }

            if (time < _mStartTime) return;

            // Advance the sampling factor
            _mFactor += AmountPerDelta * delta;

            // Loop style simply resets the play factor after it exceeds 1.
            if (_style == Style.Loop)
            {
                if (_mFactor > 1f)
                {
                    _mFactor -= Mathf.Floor(_mFactor);
                }
            }
            else if (_style == Style.PingPong)
            {
                // Ping-pong style reverses the direction
                if (_mFactor > 1f)
                {
                    _mFactor = 1f - (_mFactor - Mathf.Floor(_mFactor));
                    _mAmountPerDelta = -_mAmountPerDelta;
                }
                else if (_mFactor < 0f)
                {
                    _mFactor = -_mFactor;
                    _mFactor -= Mathf.Floor(_mFactor);
                    _mAmountPerDelta = -_mAmountPerDelta;
                }
            }

            // If the factor goes out of range and this is a one-time tweening operation, disable the script
            if ((_style == Style.Once) && (_duration == 0f || _mFactor > 1f || _mFactor < 0f))
            {
                _mFactor = Mathf.Clamp01(_mFactor);
                Sample(_mFactor, true);

                // Disable this script unless the function calls above changed something
                if (_duration == 0f || (_mFactor == 1f && _mAmountPerDelta > 0f || _mFactor == 0f && _mAmountPerDelta < 0f))
                    enabled = false;
            }
            else Sample(_mFactor, false);
        }

        #endregion

        void OnDisable() { _mStarted = false; }

        /// <summary>
        /// Sample the tween at the specified factor.
        /// </summary>

        public void Sample(float factor, bool isFinished)
        {
            // Calculate the sampling value
            float val = Mathf.Clamp01(factor);

            if (_method == Method.EaseIn)
            {
                val = 1f - Mathf.Sin(0.5f * Mathf.PI * (1f - val));
            }
            else if (_method == Method.EaseOut)
            {
                val = Mathf.Sin(0.5f * Mathf.PI * val);
            }
            else if (_method == Method.EaseInOut)
            {
                const float pi2 = Mathf.PI * 2f;
                val = val - Mathf.Sin(val * pi2) / pi2;
            }
            else if (_method == Method.BounceIn)
            {
                val = BounceLogic(val);
            }
            else if (_method == Method.BounceOut)
            {
                val = 1f - BounceLogic(1f - val);
            }

            OnUpdate(val, isFinished);
        }

        float BounceLogic(float val)
        {
            if (val < 0.363636f) // 0.363636 = (1/ 2.75)
            {
                val = 7.5685f * val * val;
            }
            else if (val < 0.727272f) // 0.727272 = (2 / 2.75)
            {
                val = 7.5625f * (val -= 0.545454f) * val + 0.75f; // 0.545454f = (1.5 / 2.75) 
            }
            else if (val < 0.909090f) // 0.909090 = (2.5 / 2.75) 
            {
                val = 7.5625f * (val -= 0.818181f) * val + 0.9375f; // 0.818181 = (2.25 / 2.75) 
            }
            else
            {
                val = 7.5625f * (val -= 0.9545454f) * val + 0.984375f; // 0.9545454 = (2.625 / 2.75) 
            }
            return val;
        }
        /// <summary>
        /// Play the tween forward.
        /// </summary>

        public void PlayForward() { Play(true); }

        /// <summary>
        /// Play the tween in reverse.
        /// </summary>

        public void PlayReverse() { Play(false); }

        /// <summary>
        /// Manually activate the tweening process, reversing it if necessary.
        /// </summary>

        public void Play(bool forward)
        {
            _mAmountPerDelta = Mathf.Abs(AmountPerDelta);
            if (!forward) _mAmountPerDelta = -_mAmountPerDelta;
            enabled = true;
            Update();
        }

        public void ResetToBeginning()
        {
            _mStarted = false;
            _mFactor = (AmountPerDelta < 0f) ? 1f : 0f;
            Sample(_mFactor, false);
        }

        /// <summary>
        /// Manually start the tweening process, reversing its direction.
        /// </summary>

        public void Toggle()
        {
            if (_mFactor > 0f)
            {
                _mAmountPerDelta = -AmountPerDelta;
            }
            else
            {
                _mAmountPerDelta = Mathf.Abs(AmountPerDelta);
            }
            enabled = true;
        }

        /// <summary>
        /// Actual tweening logic should go here.
        /// </summary>

        abstract protected void OnUpdate(float factor, bool isFinished);

        /// <summary>
        /// Starts the tweening operation.
        /// </summary>

        static public T Begin<T>(GameObject go, float duration) where T : UTweener
        {
            T comp = go.GetComponent<T>();

            if (comp == null)
            {
                comp = go.AddComponent<T>();
                if (comp == null)
                {
                    //Debug.LogError("Unable to add " + typeof(T) + " to " + NGUITools.GetHierarchy(go), go);
                    return null;
                }
            }
            comp._mStarted = false;
            comp._duration = duration;
            comp._mFactor = 0f;
            comp._mAmountPerDelta = Mathf.Abs(comp.AmountPerDelta);
            comp._style = Style.Once;
            comp.enabled = true;
            return comp;
        }

        /// <summary>
        /// Set the 'from' value to the current one.
        /// </summary>

        public virtual void SetStartToCurrentValue() { }

        /// <summary>
        /// Set the 'to' value to the current one.
        /// </summary>

        public virtual void SetEndToCurrentValue() { }
    }
}