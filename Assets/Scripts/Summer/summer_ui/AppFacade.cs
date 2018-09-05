using UnityEngine;
using PureMVC.Interfaces;
using PureMVC.Patterns;

namespace Summer
{
    public class AppFacade : Facade
    {
        public new static IFacade Instance
        {
            get
            {
                if (m_instance == null)
                {
                    /*lock (m_staticSyncRoot)*/
                    {
                        if (m_instance == null)
                        {
                            m_instance = new AppFacade();
                        }
                    }
                }
                return m_instance;
            }
        }

        protected AppFacade()
        {

        }

        protected override void InitializeController()
        {
            base.InitializeController();
            RegisterCommand(StartupCommand.STARTUP, typeof(StartupCommand));
        }


        /// <summary>
        /// 启动PureMVC的入口函数
        /// </summary>
        public static void Startup()
        {
            Instance.SendNotification(StartupCommand.STARTUP);
        }

        public static T FindProxy<T>(string name) where T : Proxy
        {
            T t = Instance.RetrieveProxy(name) as T;
            return t;
        }


    }

    public class StartupCommand : SimpleCommand
    {
        public const string STARTUP = "StartupCommand_STARTUP";//启动事件
        public override void Execute(INotification notification)
        {

            //----------------- 注册相关信息 -----------------------
            AppFacadeManager.OnRegister();
            //----------------- 注册UI -----------------------
            ResManager.instance.LoadPrefab("RootPanel", E_GameResType.ui_prefab);
            //----------------- 初始化管理器 -----------------------
            // 本地化文本
            LangLocSet.Instance.Init();


            /*// 注册UI
            GameObject rp = (ResManager.instance.LoadAsset<GameObject>("RootPanel", E_GameResType.ui_prefab));
            GameObject go = Object.Instantiate(rp);
            Object.DontDestroyOnLoad(go);*/

            // 打开Login界面
            PanelManagerCtrl.Open(E_ViewId.login);
        }
    }

}



