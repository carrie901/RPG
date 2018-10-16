
//=============================================================================
// Author : mashao
// CreateTime : 2018-3-14 15:46:18
// FileName : PanelInfo.cs
//=============================================================================

namespace Summer
{
    //界面数据
    public class PanelInfo
    {
        public E_ViewId _viewId;                                        //View ID
        public string _pfbName;
        public E_PanelType _viewType;                                   //View Type 指向是否可以进入导航队列
        public E_PanelBgType _showMode = E_PanelBgType.NOTHING;
        public bool _hasBgClickClose = true;                            //点击不关闭本界面

        //public System.Object Info { get; set; }

        public PanelInfo(E_ViewId view_id, E_PanelType view_type, string name)
        {
            _viewId = view_id;
            _viewType = view_type;
            _pfbName = name;
        }

        public E_ViewId ViewId { get { return _viewId; } }

        public E_PanelType ViewType { get { return _viewType; } }
        public string GetPfbName { get { return _pfbName; } }

        public bool IsPanel()
        {
            return _viewType == E_PanelType.PANEL;
        }

        public bool Equal(PanelInfo data)
        {
            if (data._viewId == _viewId && data._viewType == _viewType)
                return true;
            return false;
        }
    }
}
