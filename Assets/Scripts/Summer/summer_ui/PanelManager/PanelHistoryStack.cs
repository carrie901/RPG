
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

namespace Summer
{

    /// <summary>
    /// 界面是通道
    /// </summary>
    public class PanelHistoryStack
    {
        #region 属性

        public delegate BaseView OnStack(PanelInfo view_data);

        public event OnStack OnOpen;
        public event OnStack OnClose;

        protected readonly List<PanelInfo> _panel_history = new List<PanelInfo>(64);
        protected PanelInfo _curr_view;



        #endregion

        #region Public

        public BaseView Open(PanelInfo view_data)
        {
            BaseView base_view = _internal_open(view_data);

            //PanelLog.Log("-->当前界面:[{0}]",_curr_view.ViewId);
            return base_view;
        }

        // 返回上一个界面，返回的含义包含了关闭和返回
        public void Back(E_ViewId view_id)
        {
            _internal_back(view_id);
            //PanelLog.Log("<--当前界面:[{0}]", _curr_view.ViewId);
        }

        // 关闭指定的界面，这里特指某一个指定的界面
        public void Close(E_ViewId view_id)
        {
            _internal_close(view_id);
        }

        public PanelInfo GetCurrView() { return _curr_view; }

        public bool AssetView(E_ViewId view_id)
        {
            if (_curr_view == null) return false;
            return _curr_view.ViewId == view_id;
        }

        #endregion

        #region Private Methods Close Open

        public BaseView _internal_open(PanelInfo view_data)
        {
            if (view_data.IsPanel() && _curr_view != null)
            {
                for (int i = _panel_history.Count - 1; i >= 0; i--)
                {
                    _internal_history_close(_panel_history[i]);
                }
            }
            BaseView curr_view = _internal_real_open(view_data);

            // 回退

            return curr_view;
        }

        public void _internal_back(E_ViewId view_id, bool flag = false)
        {
            if (_internal_close_top_view(view_id)) return;

            while (_panel_history.Count >= 1 && _curr_view != null)
            {
                if (_curr_view.IsPanel())
                {
                    _internal_history_open(_curr_view);
                    break;
                }

                _internal_real_close(_curr_view);
            }
        }

        public void _internal_close(E_ViewId view_id)
        {
            if (_internal_close_top_view(view_id)) return;

            PanelInfo tmp_data = _curr_view;
            int index = _panel_history.Count - 1;
            while (_panel_history.Count >= 1 && tmp_data != null)
            {
                if (tmp_data.IsPanel())
                {
                    _internal_history_open(tmp_data);
                    break;
                }
                _internal_history_close(tmp_data);

                index--;
                tmp_data = _panel_history[index];
            }
        }

        // 关闭最上层的UI
        public bool _internal_close_top_view(E_ViewId view_id)
        {
            PanelLog.Assert(_curr_view != null, "当前界面为空");
            if (_curr_view == null) return true;

            // 正常通道的历史界面必定是Panel打开头
            PanelLog.Assert(_panel_history.Count != 1, "已经回退到最后一个历史界面");
            if (_panel_history.Count <= 1) return true;

            if (view_id != _curr_view.ViewId) return true;

            bool result = _curr_view.IsPanel();
            // 关闭当前界面
            _internal_real_close(_curr_view);
            if (result) return false;
            return true;
        }

        // 添加到历史记录并且打开
        public BaseView _internal_real_open(PanelInfo view_data)
        {
            _curr_view = view_data;
            PushHistory(view_data);
            if (OnOpen != null)
                return OnOpen(view_data);
            return null;
        }

        // 剔除历史记录，并且关闭
        public void _internal_real_close(PanelInfo view_data)
        {
            Remove(view_data);
            _curr_view = PeekHistory();
            if (OnClose != null)
                OnClose(view_data);
        }

        public void _internal_history_open(PanelInfo view_data)
        {
            if (OnOpen != null)
                OnOpen(view_data);
        }

        public void _internal_history_close(PanelInfo view_data)
        {
            if (OnClose != null)
                OnClose(view_data);
        }

        #endregion

        #region 界面历史

        public void PushHistory(PanelInfo view_data)
        {
            PanelLog.Log("Add :[{0}]", view_data.ViewId);
            _panel_history.Add(view_data);
        }

        public PanelInfo PopHistory()
        {
            PanelInfo view_data = null;
            if (_panel_history.Count > 0)
            {
                view_data = _panel_history[_panel_history.Count - 1];

                PanelLog.Log("Remove :[{0}]", view_data.ViewId);
                _panel_history.RemoveAt(_panel_history.Count - 1);
            }
            return view_data;
        }

        public PanelInfo PeekHistory()
        {
            if (_panel_history.Count > 0)
                return _panel_history[_panel_history.Count - 1];
            return null;
        }

        public bool Remove(PanelInfo view_data)
        {
            for (int i = _panel_history.Count - 1; i >= 0; i--)
            {
                if (view_data == _panel_history[i])
                {
                    PanelLog.Log("Remove :[{0}]", view_data.ViewId);
                    _panel_history.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }

        #endregion
    }

    public class PanelHistoryInfo : I_PoolCacheRef
    {
        //public static PanelHistoryInfo Instance = new PanelHistoryInfo();

        public E_ViewId _view_id;

        public static Dictionary<E_ViewId, PanelHistoryInfo> _map = new Dictionary<E_ViewId, PanelHistoryInfo>();
        public static PanelHistoryInfo Get(E_ViewId view_id)
        {
            if (_map.ContainsKey(view_id))
                return _map[view_id];
            else
            {
                PanelHistoryInfo info = new PanelHistoryInfo();
                info._view_id = view_id;
                _map.Add(view_id, info);
                return info;
            }
        }

        public int GetRefCount()
        {
            bool result = PanelFactory.Instance.CheckPanelState(_view_id);
            if (result)
                return 1;
            else
                return 0;
        }
    }
}