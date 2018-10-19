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
        public static readonly Dictionary<E_ViewId, PanelInfo> ViewPrefabMap
            = new Dictionary<E_ViewId, PanelInfo>(PanelComparer.Instance);

        public static int _viewCount = 0;

        #endregion

        #region public
        public static void Init()
        {
            _viewCount = 0;
            InitPanel();
            InitDialog();
            LogManager.Assert(_viewCount == ((int)E_ViewId.MAX - 1), "注册的UI数量不对");
        }

        public static PanelInfo Get(E_ViewId id)
        {
            return ViewPrefabMap[id];
        }

        #endregion

        #region private 

        public const string ROOT_PATH = "res_bundle/prefab/ui/";
        public static void InitPanel()
        {
            _init_view_data(E_ViewId.LOADING, E_PanelType.PANEL, ROOT_PATH + "PanelLoading.prefab");
            _init_view_data(E_ViewId.LOGIN, E_PanelType.PANEL, ROOT_PATH + "PanelLogin.prefab");
            _init_view_data(E_ViewId.MAIN, E_PanelType.PANEL, ROOT_PATH + "PanelMain.prefab");
            _init_view_data(E_ViewId.SHOP, E_PanelType.PANEL, ROOT_PATH + "PanelShop.prefab");
            _init_view_data(E_ViewId.ROLES, E_PanelType.PANEL, ROOT_PATH + "PanelRoles.prefab");
            _init_view_data(E_ViewId.BAG, E_PanelType.PANEL, ROOT_PATH + "PanelBag.prefab");
            _init_view_data(E_ViewId.TASK, E_PanelType.PANEL, ROOT_PATH + "PanelTask.prefab");
            _init_view_data(E_ViewId.EQUIP, E_PanelType.PANEL, ROOT_PATH + "PanelEquip.prefab");
            _init_view_data(E_ViewId.SKILL, E_PanelType.PANEL, ROOT_PATH + "PanelSkill.prefab");
            _init_view_data(E_ViewId.SELECT_LEVEL, E_PanelType.PANEL, ROOT_PATH + "PanelSelectLevel.prefab");
        }

        public static void InitDialog()
        {
            _init_view_data(E_ViewId.ALERT, E_PanelType.DIALOG, ROOT_PATH + "DialogAlert.prefab");
            //_init_view_data(E_ViewId.ALERT_MAIN, E_PanelType.DIALOG, ROOT_PATH + "DialogAlertMain.prefab");
            //_init_view_data(E_ViewId.player_name,E_PanelType.dialog, "PanelPlayerName");
        }

        private static void _init_view_data(E_ViewId id, E_PanelType type, string pfbName, E_PanelBgType showMode = E_PanelBgType.NOTHING,
            bool hasBgClickClose = true)
        {
            PanelInfo view = new PanelInfo(id, type, pfbName);
            view._showMode = showMode;
            view._hasBgClickClose = hasBgClickClose;
            ViewPrefabMap.Add(id, view);
            _viewCount++;
        }

        #endregion

    }
}


