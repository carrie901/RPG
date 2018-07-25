using System;
using System.Collections.Generic;
using PureMVC.Patterns;


namespace Summer
{
    /// <summary>
    /// 如果分散开来不好管理
    /// 所以对所有的Proxy Command Mediator 做统一的管理
    /// TODO 需要一个入口来统一化所有的内容
    ///     1.来保证输入的一致性和统一化
    ///     2.明确程序的原点，如果想进行修改不需要东找西找
    /// 但是一旦东西多了之后，容易造成混乱
    /// </summary>
    public class AppFacadeManager
    {
        public static List<Proxy> _proxys = new List<Proxy>();
        public static Dictionary<string, Type> _commands = new Dictionary<string, Type>();

        #region 注册和移除

        public static void OnRegister()
        {
            RegisterCommand();
            RegisterProxy();
        }

        public static void OnRemove()
        {
            RemoveProxy();
            RemoveCommand();
        }

        #endregion

        #region Proxy

        // 注册Proxy
        private static void RegisterProxy()
        {
            /*_on_register_proxy(new BattleSkillProxy());             // 战斗
            _on_register_proxy(new HeroProxy());                    // 英雄卡片信息
            _on_register_proxy(new LevelProxy());                   // 关卡
            _on_register_proxy(new PlotProxy());                    // 剧情
            _on_register_proxy(new PlayerProxy());                  // 玩家个人信息
            _on_register_proxy(new ItemProxy());                    // 物品信息*/

            /*int length = _proxys.Count;
            for (int i = 0; i < length; i++)
                _proxys[i].InitProxyAfterAll();*/
        }

        // 移除Proxy
        public static void RemoveProxy()
        {
            int length = _proxys.Count;
            for (int i = 0; i < length; i++)
                AppFacade.Instance.RemoveProxy(_proxys[i].ProxyName);
        }

        #endregion

        #region Command

        public static void RegisterCommand()
        {
            _commands.Clear();

        }

        public static void RemoveCommand()
        {
            foreach (var v in _commands)
            {
                AppFacade.Instance.RemoveCommand(v.Key);
            }
        }

        #endregion

        #region private 

        public static void _on_register_proxy(Proxy proxy)
        {
            AppFacade.Instance.RegisterProxy(proxy);
            _proxys.Add(proxy);
        }

        // 注册单个命令
        public static void _on_register_command(string name, Type type)
        {
            AppFacade.Instance.RegisterCommand(name, type);
            _commands.Add(name, type);
        }

        #endregion
    }

    #region Command消息

   

    #endregion
}
