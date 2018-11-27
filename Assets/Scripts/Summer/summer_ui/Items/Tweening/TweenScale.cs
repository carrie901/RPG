
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

using UnityEngine;

namespace Summer
{
    public class TweenScale : UTweener
    {

        #region 属性

        public Vector3 _from = Vector3.one;
        public Vector3 _to = Vector3.one;

        Transform _mTrans;

        public Transform CachedTransform { get { if (_mTrans == null) _mTrans = transform; return _mTrans; } }

        public Vector3 Value { get { return CachedTransform.localScale; } set { CachedTransform.localScale = value; } }

        #endregion

        #region MONO Override

        protected override void OnUpdate(float factor, bool isFinished)
        {
            Value = _from * (1f - factor) + _to * factor;
        }

        #endregion

        #region Public

        static public TweenScale Begin(GameObject go, float duration, Vector3 scale)
        {
            TweenScale comp = UTweener.Begin<TweenScale>(go, duration);
            comp._from = comp.Value;
            comp._to = scale;

            if (duration <= 0f)
            {
                comp.Sample(1f, true);
                comp.enabled = false;
            }
            return comp;
        }

        #endregion
    }
}
