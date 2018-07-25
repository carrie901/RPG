
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
        public Dictionary<E_ViewId, BaseView> _panel_map = new Dictionary<E_ViewId, BaseView>(new PanelComparer());

        #region public 

        public BaseView Open(ViewData view_info)
        {
            BaseView base_view = _internal_open(view_info);
            return base_view;
        }

        public void Close(ViewData view_info)
        {
            if (view_info == null) return;
            E_ViewId view_id = view_info.ViewId();
            _internal_close(view_info);
        }

        public void Destory(ViewData view_info)
        {
            if (view_info == null) return;
            E_ViewId view_id = view_info.ViewId();
            _internal_destroy(view_info);
        }

        #endregion

        #region private

        public BaseView _internal_open(ViewData data)
        {
            if (data == null) return null;

            E_ViewId view_id = data.ViewId();
            BaseView base_view;
            if (!_panel_map.ContainsKey(view_id))
            {
                base_view = _internal_load_panel(data);
            }
            else
            {
                base_view = _inter_again_open(data);
            }
            GameObjectHelper.SetActive(base_view.gameObject, true);
            return base_view;
        }

        public void _internal_close(ViewData data)
        {
            E_ViewId view_id = data.ViewId();
            if (!_panel_map.ContainsKey(view_id)) return;
            BaseView base_view = _panel_map[view_id];
            GameObjectHelper.SetActive(base_view.gameObject, false);
        }


        public void _internal_destroy(ViewData data)
        {
            E_ViewId view_id = data.ViewId();
            if (!_panel_map.ContainsKey(view_id)) return;
            BaseView base_view = _panel_map[view_id];

            ResRequestInfo res_request_info = ResRequestFactory.CreateRequest<GameObject>(data.GetPfbName(), E_GameResType.quanming);
            ResLoader.instance.UnLoadChildRes(res_request_info);
        }

        public BaseView _internal_load_panel(ViewData data)
        {
            E_ViewId view_id = data.ViewId();
            // 2.加载GameObject 并且 _instantiate
            BaseView view = _real_load_panel(data);
            // 3.Instantiate
            //view = _instantiate(obj, data);
            // 4.view的额外操作
            view.OpenExtraOpertion(data);
            // 5.view的额外data
            view.SetData(data.Info);
            // 6.添加到层
            //_add_layer(view.gameObject);
            view.OnInit();
            // 8.添加到缓存
            _panel_map.Add(view_id, view);
            return view;
        }

        public BaseView _real_load_panel(ViewData data)
        {
            if (data == null) return null;
            GameObject go = ResManager.instance.LoadPrefab(data.GetPfbName(), E_GameResType.ui_prefab);
            BaseView base_view = go.GetComponent<BaseView>();
            return base_view;
        }

        public BaseView _inter_again_open(ViewData data)
        {
            ResRequestInfo res_request = ResRequestFactory.CreateRequest<GameObject>(data.GetPfbName(), E_GameResType.ui_prefab);
            ResLoader.instance.CheckChildAssetAndLoad(res_request);

            BaseView base_view = _panel_map[data.ViewId()];
            base_view.OpenExtraOpertion(data);
            base_view.SetData(data.Info);

            return base_view;
        }

        #endregion
    }
}
