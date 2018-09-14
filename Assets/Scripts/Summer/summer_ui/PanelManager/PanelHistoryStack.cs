
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
using Object=System.Object;
using System.Collections.Generic;

namespace Summer
{

    /// <summary>
    /// 界面是通道
    /// </summary>
    public class PanelHistoryStack
    {
        #region 属性

        public delegate void OnStack(PanelInfo viewData, Object info = null, Action<BaseView> action = null);

        public event OnStack OnOpen;
        public event OnStack OnClose;

        protected readonly List<PanelInfo> _panelHistory = new List<PanelInfo>(64);
        protected PanelInfo _currView;

        #endregion

        #region Public

        public void Open(PanelInfo viewData, Object info, Action<BaseView> action = null)
        {
            if (viewData.IsPanel() && _currView != null)
            {
                for (int i = _panelHistory.Count - 1; i >= 0; i--)
                {
                    _internal_history_close(_panelHistory[i]);
                }
            }
            _internal_real_open(viewData, info, action);
            //PanelLog.Log("-->当前界面:[{0}]",_curr_view.ViewId);
        }

        // 返回上一个界面，返回的含义包含了关闭和返回
        public void Back(E_ViewId viewId)
        {
            _internal_back(viewId);
            //PanelLog.Log("<--当前界面:[{0}]", _curr_view.ViewId);
        }

        // 关闭指定的界面，这里特指某一个指定的界面
        public void Close(E_ViewId viewId)
        {
            _internal_close(viewId);
        }

        public PanelInfo GetCurrView() { return _currView; }

        public bool AssetView(E_ViewId viewId)
        {
            if (_currView == null) return false;
            return _currView.ViewId == viewId;
        }

        #endregion

        #region Private Methods Close Open

        public void _internal_back(E_ViewId viewId, bool flag = false)
        {
            if (_internal_close_top_view(viewId)) return;

            while (_panelHistory.Count >= 1 && _currView != null)
            {
                if (_currView.IsPanel())
                {
                    _internal_history_open(_currView);
                    break;
                }

                _internal_real_close(_currView);
            }
        }

        public void _internal_close(E_ViewId viewId)
        {
            if (_internal_close_top_view(viewId)) return;

            PanelInfo tmpData = _currView;
            int index = _panelHistory.Count - 1;
            while (_panelHistory.Count >= 1 && tmpData != null)
            {
                if (tmpData.IsPanel())
                {
                    _internal_history_open(tmpData);
                    break;
                }
                _internal_history_close(tmpData);

                index--;
                tmpData = _panelHistory[index];
            }
        }

        // 关闭最上层的UI
        public bool _internal_close_top_view(E_ViewId viewId)
        {
            PanelLog.Assert(_currView != null, "当前界面为空");
            if (_currView == null) return true;

            // 正常通道的历史界面必定是Panel打开头
            PanelLog.Assert(_panelHistory.Count != 1, "已经回退到最后一个历史界面");
            if (_panelHistory.Count <= 1) return true;

            if (viewId != _currView.ViewId) return true;

            bool result = _currView.IsPanel();
            // 关闭当前界面
            _internal_real_close(_currView);
            if (result) return false;
            return true;
        }

        // 添加到历史记录并且打开
        public void _internal_real_open(PanelInfo viewData, Object info, Action<BaseView> action = null)
        {
            _currView = viewData;
            PushHistory(viewData);
            if (OnOpen != null)
                OnOpen(viewData, info, action);
        }

        // 剔除历史记录，并且关闭
        public void _internal_real_close(PanelInfo viewData, Action<BaseView> action = null)
        {
            Remove(viewData);
            _currView = PeekHistory();
            if (OnClose != null)
                OnClose(viewData, action);
        }

        public void _internal_history_open(PanelInfo viewData, Action<BaseView> action = null)
        {
            if (OnOpen != null)
                OnOpen(viewData, action);
        }

        public void _internal_history_close(PanelInfo viewData, Action<BaseView> action = null)
        {
            if (OnClose != null)
                OnClose(viewData, action);
        }

        #endregion

        #region 界面历史

        public void PushHistory(PanelInfo viewData)
        {
            PanelLog.Log("Add :[{0}]", viewData.ViewId);
            _panelHistory.Add(viewData);
        }

        public PanelInfo PopHistory()
        {
            PanelInfo viewData = null;
            if (_panelHistory.Count > 0)
            {
                viewData = _panelHistory[_panelHistory.Count - 1];

                PanelLog.Log("Remove :[{0}]", viewData.ViewId);
                _panelHistory.RemoveAt(_panelHistory.Count - 1);
            }
            return viewData;
        }

        public PanelInfo PeekHistory()
        {
            if (_panelHistory.Count > 0)
                return _panelHistory[_panelHistory.Count - 1];
            return null;
        }

        public bool Remove(PanelInfo viewData)
        {
            for (int i = _panelHistory.Count - 1; i >= 0; i--)
            {
                if (viewData == _panelHistory[i])
                {
                    PanelLog.Log("Remove :[{0}]", viewData.ViewId);
                    _panelHistory.RemoveAt(i);
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

        public E_ViewId _viewId;

        public static Dictionary<E_ViewId, PanelHistoryInfo> _map = new Dictionary<E_ViewId, PanelHistoryInfo>();
        public static PanelHistoryInfo Get(E_ViewId viewId)
        {
            if (_map.ContainsKey(viewId))
                return _map[viewId];
            else
            {
                PanelHistoryInfo info = new PanelHistoryInfo {_viewId = viewId};
                _map.Add(viewId, info);
                return info;
            }
        }

        public int GetRefCount()
        {
            bool result = PanelFactory.Instance.CheckPanelState(_viewId);
            if (result)
                return 1;
            else
                return 0;
        }
    }
}