using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace Summer
{
    /// <summary>
    /// View的生命周期
    /// 1.Load 2.Instantiate 3.挂载 4.显示 
    /// 2.管道管理，不同的管道有不同的规则
    /// </summary>
    public abstract class ViewProcess
    {
        public static WaitForEndOfFrame _end_frame = new WaitForEndOfFrame();
        public Dictionary<E_ViewId, BaseView> _dic_map = new Dictionary<E_ViewId, BaseView>();
        public Transform _parent;
        public Transform _currtrans;
        public ViewProcess(Transform trans)
        {
            _parent = trans;
        }

        #region abstract

        public abstract BaseView OpenView(ViewData next_view, System.Object info);

        public abstract void CloseView(ViewData next_view);

        protected BaseView FindView(E_ViewId view_id)
        {
            if (_dic_map.ContainsKey(view_id))
            {
                return _dic_map[view_id];
            }
            return null;
        }

        #endregion

        protected BaseView InternalRealOpenView(ViewData data, System.Object info)
        {
            if (data == null) return null;
            // 1.查找viewdata
            E_ViewId view_id = data.ViewId();
            BaseView view;
            if (!_dic_map.ContainsKey(view_id))
            {
                // 2.加载GameObject
                GameObject obj = _load(data);
                // 3.Instantiate
                view = _instantiate(obj, data);
                // 4.view的额外操作
                view.OpenExtraOpertion(data);
                // 5.view的额外data
                view._view_info = info;
                // 6.添加到层
                _add_layer(view.gameObject);
                view.OnInit();
                // 7.显示层
                _show_view(view);
                // 8.添加到缓存
                _dic_map.Add(view_id, view);
            }
            else
            {
                view = _dic_map[view_id];
                view._view_info = info;
                _show_view(_dic_map[view_id]);
            }
            return view;
        }

        protected void InternalRealCloseView(ViewData data)
        {
            if (data == null) return;
            E_ViewId view_id = data.ViewId();
            if (_dic_map.ContainsKey(view_id))
            {
                _hide_view(_dic_map[view_id]);
            }
        }

        protected virtual GameObject _load(ViewData data)
        {
            if (data == null) return null;
            string path = data.GetViewPath();
            //GameObject obj = Resources.Load(path) as GameObject;
            ResRequestInfo res_request = ResRequestFactory.CreateRequest<GameObject>(path, E_GameResType.ui_prefab);
            GameObject obj = ResManager.instance.LoadPrefab(res_request, false);
            return obj;
        }

        protected virtual BaseView _instantiate(GameObject obj, ViewData data)
        {
            GameObject view = UnityEngine.Object.Instantiate(obj);
            view.name = data.pfb_name;
            BaseView component = view.GetComponent<BaseView>();
            return component;
        }

        #region virtual add/show/

        protected virtual void _add_layer(GameObject view)
        {
            //RectTransformHelper.SetParent(_parent, view.transform);
            GameObjectHelper.AddChild(_parent, view.transform);
        }

        protected virtual void _remove_layer() { }

        protected virtual void _show_view(BaseView view)
        {
            view.gameObject.transform.SetAsLastSibling();
            GameObjectHelper.SetActive(view.gameObject, true);
            view.OnEnter();
        }
        protected virtual void _hide_view(BaseView view)
        {
            view.CloseExtraOpertion();
            view.OnExit();
            GameObjectHelper.SetActive(view.gameObject, false);
        }

        protected virtual void _destroy_view() { }
        protected virtual void _unload_view() { }

        #endregion
    }
}

