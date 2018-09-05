
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

        protected int _lock_count;
        protected PanelHistoryStack panel_history_stack = new PanelHistoryStack();
        protected PanelFactory _panel_factory = new PanelFactory();
        public Transform panel_canvas;                                  // Panel层
        public Transform dialog_canvas;                                 // Dialog层
        public Transform spec_canvas;                                   // 特殊层
        public Transform message_canvas;                                // 消息层
        public GameObject lock_canvas;                                  // 锁屏层
        protected Transform _mgr_root;


        #endregion

        #region MONO Override

        private PanelManager() { }

        // Use this for initialization
        void Awake()
        {
            LogManager.Assert(_instance == null, "多次重复实例化PanelManager");
            _instance = this;
            _mgr_root = transform;
            PanelManagerConfig.Init();
            panel_history_stack.OnOpen += _real_open;
            panel_history_stack.OnClose += _real_close;
        }

        #endregion

        #region Public

        public void OnOpen(E_ViewId view_id, System.Object info = null, Action<BaseView> action = null)
        {
            PanelInfo view_data = PanelManagerConfig.Get(view_id);
            panel_history_stack.Open(view_data, info);
        }

        public void OnClose(E_ViewId view_id)
        {
            bool result = panel_history_stack.AssetView(view_id);
            PanelLog.Assert(result, "回退的界面，和当前的界面历史不一致");
            if (!result) return;
            panel_history_stack.Back(view_id);
        }

        // 锁屏操作
        public void Lock(bool value)
        {
            _lock_count += (value ? 1 : -1);
            GameObjectHelper.SetActive(_instance.lock_canvas, value);
        }

        public void ResetLock()
        {
            _lock_count = 0;
            GameObjectHelper.SetActive(_instance.lock_canvas, false);
        }

        #endregion

        #region Private Methods

        public void _real_open(PanelInfo view_data, System.Object info = null, Action<BaseView> action = null)
        {
            BaseView base_view = _panel_factory.Open(view_data, info);
            if (action != null)
                action(base_view);
            base_view.gameObject.SetActive(false);
            GameObjectHelper.SetParent(base_view.gameObject, panel_canvas.gameObject, true);
            base_view.gameObject.SetActive(true);
        }

        public void _real_close(PanelInfo view_data, System.Object info = null, Action<BaseView> action = null)
        {
            _panel_factory.Close(view_data);
        }

        #endregion
    }
}