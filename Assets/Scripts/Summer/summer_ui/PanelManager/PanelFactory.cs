
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
        public Dictionary<E_ViewId, BaseView> _panel_map
            = new Dictionary<E_ViewId, BaseView>(PanelComparer.Instance);

        public PoolPanelCache<E_ViewId, PanelHistoryInfo> _panel_cache
            = new PoolPanelCache<E_ViewId, PanelHistoryInfo>(2);

        public static PanelFactory Instance = new PanelFactory();

        public PanelFactory()
        {
            //_panel_cache.AddIgnoreKey(E_ViewId.main);
            _panel_cache.OnRemoveValueEvent += OnRemoveValueEvent;
        }

        #region public 

        public bool CheckPanelState(E_ViewId view_id)
        {
            BaseView base_view;
            _panel_map.TryGetValue(view_id, out base_view);
            if (base_view == null) return false;

            return base_view.gameObject.activeSelf;
        }

        public BaseView Open(PanelInfo view_info, System.Object info = null)
        {
            BaseView base_view = _internal_open(view_info);
            _panel_cache.Set(view_info.ViewId, PanelHistoryInfo.Get(view_info.ViewId));
            return base_view;
        }

        public void Close(PanelInfo view_info)
        {
            if (view_info == null) return;
            _internal_close(view_info);
        }

        public void Destory(PanelInfo info)
        {
            if (info == null) return;

            BaseView base_view;
            _panel_map.TryGetValue(info.ViewId, out base_view);
            E_ViewId view_id = info.ViewId;
            if (base_view == null) return;

            _panel_map.Remove(view_id);

            base_view.OnDestroySelf();
            GameObjectHelper.DestroySelf(base_view.gameObject);

            ResRequestInfo res_request_info = ResRequestFactory.CreateRequest<GameObject>(info.GetPfbName, E_GameResType.ui_prefab);
            ResLoader.instance.UnloadRes(res_request_info);
        }

        #endregion

        #region 响应

        public void OnRemoveValueEvent(E_ViewId view_id)
        {
            PanelInfo view_data = PanelManagerConfig.Get(view_id);
            Destory(view_data);
        }

        #endregion

        #region private

        public BaseView _internal_open(PanelInfo data)
        {
            if (data == null) return null;

            E_ViewId view_id = data.ViewId;
            BaseView base_view;
            if (!_panel_map.ContainsKey(view_id))
            {
                base_view = _first_open_panel(data);
            }
            else
            {
                base_view = _again_open_panel(data);
            }
            base_view.OnEnter();
            GameObjectHelper.SetActive(base_view.gameObject, true);
            return base_view;
        }

        public void _internal_close(PanelInfo data)
        {
            E_ViewId view_id = data.ViewId;
            if (!_panel_map.ContainsKey(view_id)) return;
            BaseView base_view = _panel_map[view_id];
            base_view.OnExit();
            GameObjectHelper.SetActive(base_view.gameObject, false);
        }

        // 第一次打开界面的相关步骤
        public BaseView _first_open_panel(PanelInfo data)
        {
            E_ViewId view_id = data.ViewId;
            // 2.加载GameObject 并且 _instantiate
            GameObject go = ResManager.instance.LoadPrefab(data.GetPfbName, E_GameResType.ui_prefab);
            BaseView base_view = go.GetComponent<BaseView>();
            // 3.Instantiate
            //view = _instantiate(obj, data);
            // 4.view的额外操作
            //base_view.OpenExtraOpertion(data);
            base_view.SetPanelData(view_id);
            // 6.添加到层
            //_add_layer(view.gameObject);
            base_view.OnInit();
            // 5.view的额外data
            //base_view.SetPanelData(data);
            // 8.添加到缓存
            _panel_map.Add(view_id, base_view);
            return base_view;
        }
        // 再一次打开
        public BaseView _again_open_panel(PanelInfo data)
        {
            BaseView base_view = _panel_map[data.ViewId];
            base_view.SetPanelData(data.ViewId);
            return base_view;
        }

        #endregion
    }
}
