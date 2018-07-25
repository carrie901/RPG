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
        public E_PanelType _view_type;                       //View Type 指向是否可以进入导航队列

        public E_PanelBgType show_mode = E_PanelBgType.nothing;
        public bool has_bg_click_close = true;             //点击不关闭本界面

        public System.Object Info { get; set; }

        public ViewData(E_ViewId view_id,
            E_PanelType view_type, string name)
        {
            _view_id = view_id;
            _view_type = view_type;
            pfb_name = name;
        }

        public E_ViewId ViewId() { return _view_id; }
        public E_PanelType ViewType() { return _view_type; }
        public string GetPfbName() { return pfb_name; }

        public bool IsPanel()
        {
            return _view_type == E_PanelType.panel;
        }



        public bool Equal(ViewData data)
        {
            if (data._view_id == _view_id && data._view_type == _view_type)
                return true;
            return false;
        }
    }
}
