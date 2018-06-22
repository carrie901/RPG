using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Object = System.Object;
namespace Summer
{
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

        private NetworkEventSystem()
        {

        }


        public EventSet<int, System.Object> _event_set = new EventSet<int, System.Object>();

        public bool RegisterHandler(int key, EventSet<int, Object>.EventHandler handler)
        {
            return _event_set.RegisterHandler(key, handler);
        }

        public bool UnRegisterHandler(int key, EventSet<int, Object>.EventHandler handler)
        {
            return _event_set.UnRegisterHandler(key, handler);
        }

        public void RaiseEvent(int key, Object obj_info)
        {
            if (_event_set == null) return;
            _event_set.RaiseEvent(key, obj_info);
        }

    }
}
