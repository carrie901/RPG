
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

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
namespace Summer
{
    public class BaseButton : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
    {

        #region param VoidDelegate

        public delegate void VoidDelegate(GameObject ob);           //只需传入目标监听UI对象即可
        public VoidDelegate _onClick;
        public VoidDelegate _onDoubleClick;
        public VoidDelegate _onDown;
        public VoidDelegate _onUp;
        public VoidDelegate _onExit;                                //移出UI上方事件
        public VoidDelegate _onEnter;                               //移出UI上方事件
        public VoidDelegate _onLongPress;                           //长按UI事件
        public VoidDelegate _onLongPressUp;

        #endregion
        [SerializeField]
        protected float _intervalClick = 0.3f;                      // 按钮的点击间隔时间
        [SerializeField]
        protected bool _specialClickSound = false;                  // 是否开始特殊声音
        [SerializeField]
        protected string _clickSoundName;                           // 播放的声音
        protected static float LastOnClickTime;                     // 上一次OnClick时间，防止频繁点击按钮



        protected bool _isPress;                                     // 是否按下去
        protected float _lastLongPressTime;                        // 最后一次长按发送的消息


        public bool IsEnabled
        {
            get { return false; }
            set { }
        }

        protected void PlaySoundDelegate(GameObject obj)
        {
            if (_specialClickSound)
            {
                //LogManager.Log("播放特殊声音");
            }
            else
            {
                //LogManager.Log("播放默认声音声音");
                //SoundManager.instance.Play(1);
                //SoundPlay.Instance.PlayEffectByID(1);
            }
        }

        /*
        private void Update()
        {
            if (!_has_long_press || !_is_press) return;

            //TODO 长按需要区分第一次触发的时间，和后续触发的时间

            float left_time = Time.realtimeSinceStartup - _trigger_press_time;

            if (left_time < 0) return;

            if (_on_long_press != null)
            {
                _on_long_press(gameObject);
                _trigger_press_time = Time.realtimeSinceStartup + _long_press_interval;
            }
        }
        */
        #region EventSystem

        public virtual void OnPointerDown(PointerEventData eventData)
        {
            if (_onDown != null) _onDown(gameObject);
            _lastLongPressTime = Time.realtimeSinceStartup;
            _isPress = true;
        }

        public virtual void OnPointerUp(PointerEventData eventData)
        {
            if (_onUp != null) _onUp(gameObject);
            _isPress = false;
        }

        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            if (_onEnter != null) _onEnter(gameObject);
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            if (_onExit != null) _onExit(gameObject);
            _isPress = false;
        }

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left)
                return;
            if (Time.realtimeSinceStartup - LastOnClickTime <= _intervalClick) return;
            LastOnClickTime = Time.realtimeSinceStartup;

            PlaySoundDelegate(gameObject);
            //TODO 前置响应，等待添加 
            if (_onClick != null) _onClick(gameObject);

        }



        #endregion

        #region register

        public void AddClickListener(VoidDelegate onClick)
        {
            _onClick += onClick;
        }

        public void RemoveClickListener(VoidDelegate onClick)
        {
            _onClick -= onClick;
        }

        public void AddClickDownListener(VoidDelegate onClickDown)
        {
            _onDown += onClickDown;
        }

        public void RemoveClickDownListener(VoidDelegate onClickDown)
        {
            _onDown -= onClickDown;
        }

        #endregion
    }
}
