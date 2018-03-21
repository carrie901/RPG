using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

namespace Summer
{
    /// <summary>
    /// TODO 缺少动画管理
    /// 1.层级管理
    /// 2.回调机制中缺少一个返回功能
    ///     这个返回功能 Panel A Dialog B dialog C 这时候打开 Panel B 然后Panel B再返回需要 打开Panel Dialog B dialog C
    ///     这样机制的情况下，就不太好分开处理Panel和Dialog了，按照Stack机制来做处理吧
    /// 
    /// 1.提供Open 和Close接口
    /// 2.开始锁屏操作
    /// 3.提供通用的二阶提示框/通用的弹出信息（不好归类 信息表型的形式）
    /// 4.提供半自动的界面中自带col/ col_and_img
    /// 5.回退功能中有一个刷新，如果界面的数据是有外部统计然后设置进去。那么再回退的时候必须进行刷新，但是数据本身又是外部的。这就会成为一个问题
    /// </summary>
    public class ViewManager : MonoBehaviour
    {
        public static ViewManager _instance;

        public Transform panel_canvas;
        public Transform dialog_canvas;
        public Transform spec_canvas;
        public Transform tips_canvas;           //显示伤害。直接人进去，然后不做管理，他们内部自己管理,本身的加载目前也不确定
        public GameObject lock_canvas;
        public PanelQueneManager _panel_mgr;
        public DialogQueneManager _dialog_mgr;
        public readonly Dictionary<ViewData, BaseView> _view_map = new Dictionary<ViewData, BaseView>();
        public Transform _mgr_root;
        public Camera ui_camera;

        #region Mono
        private void Awake()
        {
            ViewResDef.Init();
            _panel_mgr = new PanelQueneManager(panel_canvas);
            _dialog_mgr = new DialogQueneManager(dialog_canvas);
            _instance = this; 
        }

        private void Start()
        {
            //这里代码有点混乱，不合适的调用地方
            //AppFacade facade = AppFacade.Instance as AppFacade;
            //if (facade != null) facade.Startup();
        }

        void OnApplicationQuit()
        {
           
            LogManager.Quit();
        }

        public void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            
        }

        #endregion

        public static int _lock_count;
        // 锁屏操作
        public static void Lock(bool value)
        {
            _lock_count += (value ? 1 : -1);
            GameObjectHelper.SetActive(_instance.lock_canvas, value);
        }

        public static BaseView OpenView(E_ViewId view_id)
        {
            return _instance._open_view(view_id);
        }

        public static BaseView OpenView(E_ViewId view_id, System.Object info)
        {
            return _instance._open_view(view_id, info, null);
        }

        public static void OpenView(E_ViewId view_id, System.Object info, Action<BaseView> action)
        {
            _instance._open_view(view_id, info, action);
        }

        //特殊的一层，只做显示，目前obj的加载，还有销毁是由自己进行控制的，或者说当前为明确本层的规则
        public static void AddItemToView(GameObject obj)
        {
            obj.transform.SetParent(_instance.tips_canvas, false);
        }

        public static void CloseView(E_ViewId view_id)
        {
            _instance._close_view(view_id);
        }

        #region internal
        public BaseView _open_view(E_ViewId view_id)
        {
            return _open_view(view_id, null, null);
        }

        public BaseView _open_view(E_ViewId view_id, System.Object info, Action<BaseView> action)
        {
            ViewData view_data = ViewResDef.Get(view_id);
            E_ViewType view_type = view_data.ViewType();
            BaseView bv = null;
            if (view_type == E_ViewType.panel)
            {
                _dialog_mgr.CloseAll();
                bv = _panel_mgr.OpenView(view_data, info);
            }
            else if (view_type == E_ViewType.dialog)
            {
                bv = _dialog_mgr.OpenView(view_data, info);
            }
            return bv;
        }


        /// <summary>
        /// 1.针对Panel是回退到上一个界面
        /// 2.针对Dialog是关闭当前界面
        /// </summary>
        public void _close_view(E_ViewId view_id)
        {
            ViewData view_data = ViewResDef.Get(view_id);
            E_ViewType view_type = view_data.ViewType();
            if (view_type == E_ViewType.panel)
            {
                _dialog_mgr.CloseAll();
                _panel_mgr.CloseView(view_data);
            }
            else if (view_type == E_ViewType.dialog)
            {
                _dialog_mgr.CloseView(view_data);
            }
        }
        #endregion
    }

    /// <summary>
    /// 历史操作记录
    /// </summary>
    public class ViewStack
    {
        public Action<ViewData> on_open;
        public Action<ViewData> on_close;
        public ViewData _curr_view;
        public List<ViewData> _view_record = new List<ViewData>();// 界面操作记录


        public ViewStack(Action<ViewData> open, Action<ViewData> close)
        {
            on_open = open;
            on_close = close;
        }

        public void Open(ViewData data)
        {
            if (data.ViewType() == E_ViewType.panel)
            {
                int length = _view_record.Count;
                for (int i = length - 1; i >= 0; i--)
                {
                    on_close(_view_record[i]);
                    if (_view_record[i].ViewType() == E_ViewType.panel)
                        break;
                }
            }
            _view_record.Add(data);
            _curr_view = data;
        }

        public void Close(ViewData data)
        {
            LogManager.Assert(_curr_view != data, "界面数据不一致");

            int last_panel_index = -1;
            if (data.ViewType() == E_ViewType.panel)
            {
                int length = _view_record.Count;
                for (int i = length - 1; i >= 0; i--)
                    last_panel_index = i;
            }
            if (last_panel_index == -1) return;
            // 1.关闭当前界面
            on_close(data);
            _view_record.Remove(_curr_view);
            // 2.重置当前的界面
            _curr_view = _view_record[_view_record.Count - 1];
            // 3.如果不是Panel
            if (data.ViewType() == E_ViewType.dialog)
                return;
            // 打开之前的界面
            for (int i = last_panel_index; i < _view_record.Count; i++)
            {
                on_open(_view_record[i]);
            }
        }
    }
}


