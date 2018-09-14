
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
    /// <summary>
    /// 需求
    ///     1.层级管理
    ///     2.打开和关闭的机制 回退机制
    ///     3.锁屏操作
    ///     4.提供通用二阶确认框
    ///     5.界面自带col/col_and_img
    ///     6.刷新机制 再回退的时候刷新
    /// </summary>
    public class PanelManager : MonoBehaviour
    {

        #region 属性

        protected static PanelManager _instance;
        public static PanelManager Instance { get { return _instance; } }

        protected int _lockCount;
        protected PanelHistoryStack _panelHistoryStack = new PanelHistoryStack();
        protected PanelFactory _panelFactory = new PanelFactory();
        public Transform _panelCanvas;                                  // Panel层
        public Transform _dialogCanvas;                                 // Dialog层
        public Transform _specCanvas;                                   // 特殊层
        public Transform _messageCanvas;                                // 消息层
        public GameObject _lockCanvas;                                  // 锁屏层
        protected Transform _mgrRoot;


        #endregion

        #region MONO Override

        private PanelManager() { }

        // Use this for initialization
        void Awake()
        {
            LogManager.Assert(_instance == null, "多次重复实例化PanelManager");
            _instance = this;
            _mgrRoot = transform;
            PanelManagerConfig.Init();
            _panelHistoryStack.OnOpen += _real_open;
            _panelHistoryStack.OnClose += _real_close;
        }

        #endregion

        #region Public

        public void OnOpen(E_ViewId viewId, System.Object info = null, Action<BaseView> action = null)
        {
            PanelInfo viewData = PanelManagerConfig.Get(viewId);
            _panelHistoryStack.Open(viewData, info);
        }

        public void OnClose(E_ViewId viewId)
        {
            bool result = _panelHistoryStack.AssetView(viewId);
            PanelLog.Assert(result, "回退的界面，和当前的界面历史不一致");
            if (!result) return;
            _panelHistoryStack.Back(viewId);
        }

        // 锁屏操作
        public void Lock(bool value)
        {
            _lockCount += (value ? 1 : -1);
            GameObjectHelper.SetActive(_instance._lockCanvas, value);
        }

        public void ResetLock()
        {
            _lockCount = 0;
            GameObjectHelper.SetActive(_instance._lockCanvas, false);
        }

        #endregion

        #region Private Methods

        public void _real_open(PanelInfo viewData, System.Object info = null, Action<BaseView> action = null)
        {
            BaseView baseView = _panelFactory.Open(viewData, info);
            if (action != null)
                action(baseView);
            baseView.gameObject.SetActive(false);
            GameObjectHelper.SetParent(baseView.gameObject, _panelCanvas.gameObject, true);
            baseView.gameObject.SetActive(true);
        }

        public void _real_close(PanelInfo viewData, System.Object info = null, Action<BaseView> action = null)
        {
            _panelFactory.Close(viewData);
        }

        #endregion
    }
}