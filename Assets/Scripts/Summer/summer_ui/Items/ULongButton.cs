
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
    public class ULongButton : BaseButton
    {

        #region 属性

        protected float _longPressInterval;                         // 长按播放的间隔
        protected float _minLongPress;                              // 最小长按时间
        protected float _triggerPressTime;                          // 上一次播放长按的时间
        protected bool _hasLongPress;                               // 开启长按
        #endregion

        #region MONO Override

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (_hasLongPress || !_isPress) return;

            //TODO 长按需要区分第一次触发的时间，和后续触发的时间
            float leftTime = Time.realtimeSinceStartup - _triggerPressTime;

            if (leftTime < 0) return;

            if (_onLongPress != null)
            {
                _onLongPress(gameObject);
                _triggerPressTime = Time.realtimeSinceStartup + _longPressInterval;
            }
        }

        #endregion

        #region Public

        public void AddLongClickListener(VoidDelegate onLongPress, float minLongPress, float intervalTime = float.MaxValue)
        {
            _hasLongPress = true;
            _onLongPress += onLongPress;
            _minLongPress = minLongPress;
            _longPressInterval = intervalTime;
        }


        public void RemoveLongClickListener(VoidDelegate onLongPress)
        {
            _onLongPress -= onLongPress;
            _hasLongPress = false;
        }

        #endregion

        #region EventSystem

        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);
            _triggerPressTime = Time.realtimeSinceStartup + _minLongPress;
        }

        #endregion

        #region Private Methods



        #endregion
    }

}

