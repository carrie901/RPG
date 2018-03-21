using UnityEngine;
using System.Collections;

//=============================================================================
// Author : mashao
// CreateTime : 2018-3-14 15:46:18
// FileName : ViewData.cs
//=============================================================================

namespace Summer
{
    //界面数据
    public class ViewData
    {
        public E_ViewId _view_id;                           //View ID
        public string pfb_name;
        public E_ViewType _view_type;                       //View Type 指向是否可以进入导航队列
        public string _path;

        public E_ViewShowMode show_mode = E_ViewShowMode.nothing;
        public bool has_bg_click_close = true;             //点击不关闭本界面
        public bool need_close_other = false;               //打开这个界面的的时候，不关闭其他界面

        public ViewData(E_ViewId view_id,
            E_ViewType view_type, string path, string name)
        {
            _view_id = view_id;
            _view_type = view_type;
            _path = path;
            pfb_name = name;
        }

        public E_ViewId ViewId() { return _view_id; }
        public E_ViewType ViewType() { return _view_type; }
        public string GetViewPath() { return _path + pfb_name; }

        public bool Equal(ViewData data)
        {
            if (data._view_id == _view_id && data._view_type == _view_type)
                return true;
            return false;
        }
    }
}
