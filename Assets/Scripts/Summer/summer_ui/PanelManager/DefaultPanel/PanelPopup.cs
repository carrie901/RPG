
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


using UnityEngine.UI;
namespace Summer
{
    /// <summary>
    /// 基础的弹出界面 文本,确定按钮，取消按钮，关闭按钮
    /// </summary>
    public class PanelPopup : BaseView
    {
        #region 属性

        [UIChild("Content_Text")]
        public Text _content_text;
        [UIChild("Ok_Text")]
        public Text _ok_text;
        [UIChild("Cancel_Text")]
        public Text _cancel_text;
        [UIChild("OK_Btn")]
        public Button _ok_btn;
        [UIChild("Cancel_Btn")]
        public Button _cancel_btn;
        [UIChild("Close_Btn")]
        public Button _close_btn;
        protected PopUpEndEvent _fun_callback;
        #endregion

        #region MONO Override

        // Use this for initialization
        void Start()
        {
            if (_ok_btn != null)
                _ok_btn.onClick.AddListener(OnClickOkBtn);
            if (_cancel_btn != null)
                _cancel_btn.onClick.AddListener(OnClickCancelBtn);
            if (_close_btn != null)
                _close_btn.onClick.AddListener(OnClickCloseBtn);
        }

        private void OnDisable()
        {
            SetContentText(string.Empty);
            SetCancelText(string.Empty);
            SetOkText(string.Empty);
        }

        #endregion

        #region Public

        public void SetContentText(string text)
        {
            if (_content_text != null)
                _content_text.text = text;
        }

        public void SetOkText(string text)
        {
            if (_ok_text != null)
                _ok_text.text = text;
        }

        public void SetCancelText(string text)
        {
            if (_cancel_text != null)
                _cancel_text.text = text;
        }

        public void Show(PopUpEndEvent function)
        {
            if (function != null)
                _fun_callback = function;
        }

        private void ClosePopup()
        {
            _fun_callback = null;
            CloseView();
        }

        #endregion

        #region 按钮响应

        public void OnClickOkBtn()
        {
            if (_fun_callback != null)
            {
                _fun_callback(E_PopupEndResult.ok);
            }
            ClosePopup();
        }

        public void OnClickCancelBtn()
        {
            if (_fun_callback != null)
            {
                _fun_callback(E_PopupEndResult.cancel);
            }
            ClosePopup();
        }

        public void OnClickCloseBtn()
        {
            if (_fun_callback != null)
            {
                _fun_callback(E_PopupEndResult.cancel);
            }
            ClosePopup();
        }

        #endregion
    }
}