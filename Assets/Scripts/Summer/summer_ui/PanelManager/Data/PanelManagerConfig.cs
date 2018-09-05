using System.Collections.Generic;

namespace Summer
{
    /// <summary>
    /// 界面的配置信息,可以通过表格来进行优化
    /// </summary>
    public class PanelManagerConfig
    {
        #region 属性

        //TODO 会有gc 拆箱操作 enum类型做Dictionary的key值，
        public static readonly Dictionary<E_ViewId, PanelInfo> view_prefab_map
            = new Dictionary<E_ViewId, PanelInfo>(PanelComparer.Instance);

        public static int view_count = 0;

        #endregion

        #region public
        public static void Init()
        {
            view_count = 0;
            InitPanel();
            InitDialog();
            LogManager.Assert(view_count == ((int)E_ViewId.max - 1), "注册的UI数量不对");
        }

        public static PanelInfo Get(E_ViewId id)
        {
            return view_prefab_map[id];
        }

        #endregion

        #region private 

        public static void InitPanel()
        {
            _init_view_data(E_ViewId.login, E_PanelType.panel, "PanelLogin");
            _init_view_data(E_ViewId.main, E_PanelType.panel, "PanelMain");
        }

        public static void InitDialog()
        {
            _init_view_data(E_ViewId.alert, E_PanelType.dialog, "DialogAlert");
            _init_view_data(E_ViewId.alert_main, E_PanelType.dialog, "DialogAlertMain");
            //_init_view_data(E_ViewId.player_name,E_PanelType.dialog, "PanelPlayerName");
        }

        private static void _init_view_data(E_ViewId id, E_PanelType type, string pfb_name, E_PanelBgType show_mode = E_PanelBgType.nothing,
            bool has_bg_click_close = true)
        {
            PanelInfo view = new PanelInfo(id, type, pfb_name);
            view.show_mode = show_mode;
            view.has_bg_click_close = has_bg_click_close;
            view_prefab_map.Add(id, view);
            view_count++;
        }

        #endregion

    }
}


