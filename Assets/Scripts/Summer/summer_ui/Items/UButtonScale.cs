
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
using UnityEngine.EventSystems;
namespace Summer
{
    public class UButtonScale : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {

        #region 属性

        public Transform _tweenTarget;
        public Vector3 _pressed = new Vector3(1.05f, 1.05f, 1.05f);
        public float _duration = 0.2f;

        Vector3 _mScale;
        bool _mStarted;

        #endregion

        #region MONO Override

        // Use this for initialization
        void Start()
        {
            if (!_mStarted)
            {
                _mStarted = true;
                if (_tweenTarget == null) _tweenTarget = transform;
                _mScale = _tweenTarget.localScale;
            }
        }

        void OnEnable()
        {
        }

        void OnDisable()
        {
            if (_mStarted && _tweenTarget != null)
            {
                TweenScale tc = _tweenTarget.GetComponent<TweenScale>();

                if (tc != null)
                {
                    tc.Value = _mScale;
                    tc.enabled = false;
                }
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            OnPress(true);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            OnPress(false);
        }

        #endregion

        #region Private Methods

        private void OnPress(bool isPressed)
        {
            if (enabled)
            {
                if (!_mStarted) Start();
                /*TweenScale.Begin(_tweenTarget.gameObject, _duration, isPressed ? Vector3.Scale(_mScale, _pressed) :
                    isPressed ? Vector3.Scale(_mScale, _mScale) : _mScale)).method = UTweener.Method.EaseInOut;*/
            }
        }

        #endregion

    }
}