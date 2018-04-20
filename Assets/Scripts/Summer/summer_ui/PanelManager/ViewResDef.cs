using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Summer
{
    public enum E_ViewId
    {
        invaild = 0,
        login,                      // 登陆界面
        main,                       // 主界面
        battle,                     // 战斗主界面
        loading,                    // loading 界面
        SettingsPanel,              // 战斗主界面的回退界面
        battle_result_win,          // 战斗胜利界面
        DefeatedPanel,              // 战斗失败界面
        preview_card,               // 预览卡片界面
        overview_level,             // 关卡选择界面
        plot,                       // 剧情界面
        select_card,                // 选择卡片界面
        StageInfoPanel,             // 关卡详情界面
        hero_info_panel,            // 英雄信息界面
        hero_attribute_info,        // 卡片基础的属性信息界面
        max,

    }
    public class ViewResDef
    {
        #region 属性

        public static string ui_prefab = string.Empty;//"prefab/ui/";
        //TODO 会有gc 拆箱操作 enum类型做Dictionary的key值，
        public static readonly Dictionary<E_ViewId, ViewData> view_prefab_map
            = new Dictionary<E_ViewId, ViewData>(/*new EnumComparer<E_ViewId>()*/);

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

        public static ViewData Get(E_ViewId id)
        {
            return view_prefab_map[id];
        }

        #endregion

        #region private 

        public static void InitPanel()
        {
            _init_view_data(E_ViewId.login, E_ViewType.panel, ui_prefab, "PanelLogin");
            _init_view_data(E_ViewId.main, E_ViewType.panel, ui_prefab, "PanelMain");
            _init_view_data(E_ViewId.battle, E_ViewType.panel, ui_prefab, "PanelBattle");
            _init_view_data(E_ViewId.loading, E_ViewType.panel, ui_prefab, "PanelLoading");
            _init_view_data(E_ViewId.hero_info_panel, E_ViewType.panel, ui_prefab, "PanelHeroInfo");

            _init_view_data(E_ViewId.battle_result_win, E_ViewType.panel, ui_prefab, "VictoryPanel");
            _init_view_data(E_ViewId.DefeatedPanel, E_ViewType.panel, ui_prefab, "DefeatedPanel");
            _init_view_data(E_ViewId.preview_card, E_ViewType.panel, ui_prefab, "PreviewCardPanel");
            _init_view_data(E_ViewId.overview_level, E_ViewType.panel, ui_prefab, "CheckpointPanle");
            _init_view_data(E_ViewId.select_card, E_ViewType.panel, ui_prefab, "BattlePreparingPanel");

        }

        public static void InitDialog()
        {
            _init_view_data(E_ViewId.SettingsPanel, E_ViewType.dialog, ui_prefab, "SettingsPanel");
            _init_view_data(E_ViewId.plot, E_ViewType.dialog, ui_prefab, "DialogPlot");
            _init_view_data(E_ViewId.StageInfoPanel, E_ViewType.dialog, ui_prefab, "StageInfoPanel");
            _init_view_data(E_ViewId.hero_attribute_info, E_ViewType.dialog, ui_prefab, "PanelHeroAttributeInfo");
            //_init_view_data(E_ViewId.player_name,E_ViewType.dialog,ui_prefab, "PanelPlayerName");
        }

        private static void _init_view_data(
            E_ViewId id,
            E_ViewType type,
            string path, string pfb_name,
            E_ViewShowMode show_mode = E_ViewShowMode.nothing,
            bool has_bg_click_close = true,
            bool need_close_other = false)
        {
            ViewData view = new ViewData(id, type, path, pfb_name);
            view.show_mode = show_mode;
            view.has_bg_click_close = has_bg_click_close;
            view.need_close_other = need_close_other;
            view_prefab_map.Add(id, view);
            view_count++;
        }

        #endregion

    }
}


