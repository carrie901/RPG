using Object = System.Object;
namespace Summer
{
    /// <summary>
    /// 内部Socket的相关事件
    /// </summary>
    public class NetworkEventSystem
    {
        #region 属性

        private static NetworkEventSystem _instance;
        public static NetworkEventSystem Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new NetworkEventSystem();
                }
                return _instance;
            }
        }

        #endregion

        private NetworkEventSystem() { }

        public EventSet<E_NetModule, System.Object> _event_set = new EventSet<E_NetModule, System.Object>();

        public bool RegisterHandler(E_NetModule key, EventSet<E_NetModule, Object>.EventHandler handler)
        {
            return _event_set.RegisterHandler(key, handler);
        }

        public bool UnRegisterHandler(E_NetModule key, EventSet<E_NetModule, Object>.EventHandler handler)
        {
            return _event_set.UnRegisterHandler(key, handler);
        }

        public void RaiseEvent(E_NetModule key, Object obj_info)
        {
            if (_event_set == null) return;
            _event_set.RaiseEvent(key, obj_info);
        }

    }
}
