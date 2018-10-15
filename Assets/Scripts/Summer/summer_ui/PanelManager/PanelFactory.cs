
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

namespace Summer
{
    /// <summary>
    /// 界面工场,只提供两个对外的方法,加载界面和销毁界面
    /// 
    /// 主要是用来自定义实际意义上的加载界面和销毁界面的含义
    /// 比如销毁,只销毁其依赖,不销毁Panel,或者销毁整个GameObject
    /// </summary>
    public class PanelFactory
    {
        /// <summary>
        /// 限制了相同的界面只能出现一个
        /// </summary>
        public Dictionary<E_ViewId, BaseView> _panelMap
            = new Dictionary<E_ViewId, BaseView>(PanelComparer.Instance);

        public PoolPanelCache<E_ViewId, PanelHistoryInfo> _panelCache
            = new PoolPanelCache<E_ViewId, PanelHistoryInfo>(2);

        public static PanelFactory Instance = new PanelFactory();

        public PanelFactory()
        {
            //_panel_cache.AddIgnoreKey(E_ViewId.main);
            _panelCache.OnRemoveValueEvent += OnRemoveValueEvent;
        }

        #region public 

        public bool CheckPanelState(E_ViewId viewId)
        {
            BaseView base_view;
            _panelMap.TryGetValue(viewId, out base_view);
            if (base_view == null) return false;

            return base_view.gameObject.activeSelf;
        }

        public BaseView Open(PanelInfo viewInfo, System.Object info = null)
        {
            BaseView baseView = _internal_open(viewInfo);
            _panelCache.Set(viewInfo.ViewId, PanelHistoryInfo.Get(viewInfo.ViewId));
            return baseView;
        }

        public void Close(PanelInfo viewInfo)
        {
            if (viewInfo == null) return;
            _internal_close(viewInfo);
        }

        public void Destory(PanelInfo info)
        {
            if (info == null) return;

            BaseView baseView;
            _panelMap.TryGetValue(info.ViewId, out baseView);
            E_ViewId viewId = info.ViewId;
            if (baseView == null) return;

            _panelMap.Remove(viewId);

            baseView.OnDestroySelf();
            GameObjectHelper.DestroySelf(baseView.gameObject);

            ResLoader.instance.UnLoadRes(info.GetPfbName);
        }

        #endregion

        #region 响应

        public void OnRemoveValueEvent(E_ViewId viewId)
        {
            PanelInfo viewData = PanelManagerConfig.Get(viewId);
            Destory(viewData);
        }

        #endregion

        #region private

        public BaseView _internal_open(PanelInfo data)
        {
            if (data == null) return null;

            E_ViewId viewId = data.ViewId;
            BaseView baseView = !_panelMap.ContainsKey(viewId) ? _first_open_panel(data) : _again_open_panel(data);
            baseView.OnEnter();
            GameObjectHelper.SetActive(baseView.gameObject, true);
            return baseView;
        }

        public void _internal_close(PanelInfo data)
        {
            E_ViewId viewId = data.ViewId;
            if (!_panelMap.ContainsKey(viewId)) return;
            BaseView baseView = _panelMap[viewId];
            baseView.OnExit();
            GameObjectHelper.SetActive(baseView.gameObject, false);
        }

        // 第一次打开界面的相关步骤
        public BaseView _first_open_panel(PanelInfo data)
        {
            E_ViewId viewId = data.ViewId;
            // 2.加载GameObject 并且 _instantiate
            GameObject go = ResManager.instance.LoadPrefab(data.GetPfbName);
            BaseView baseView = go.GetComponent<BaseView>();
            // 3.Instantiate
            //view = _instantiate(obj, data);
            // 4.view的额外操作
            //base_view.OpenExtraOpertion(data);
            baseView.SetPanelData(viewId);
            // 6.添加到层
            //_add_layer(view.gameObject);
            baseView.OnInit();
            // 5.view的额外data
            //base_view.SetPanelData(data);
            // 8.添加到缓存
            _panelMap.Add(viewId, baseView);
            return baseView;
        }
        // 再一次打开
        public BaseView _again_open_panel(PanelInfo data)
        {
            BaseView baseView = _panelMap[data.ViewId];
            baseView.SetPanelData(data.ViewId);
            return baseView;
        }

        #endregion
    }
}
