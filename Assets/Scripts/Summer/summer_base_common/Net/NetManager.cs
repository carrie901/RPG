
//
//                            _ooOoo_
//                           o8888888o
//                           88" . "88
//                           (| -_- |)
//                           O\  =  /O
//                        ____/`---'\____
//                      .'  \\|     |//  `.
//                     /  \\|||  :  |||//  \
//                    /  _||||| -:- |||||-  \
//                    |   | \\\  -  /// |   |
//                    | \_|  ''\---/''  |   |
//                    \  .-\__  `-`  ___/-. /
//                  ___`. .'  /--.--\  `. . __
//               ."" '<  `.___\_<|>_/___.'  >'"".
//              | | :  `- \`.;`\ _ /`;.`/ - ` : | |
//              \  \ `-.   \_ __\ /__ _/   .-` /  /
//         ======`-.____`-.___\_____/___.-`____.-'======
//                            `=---='
//        ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
//                 			 佛祖 保佑             

using UnityEngine;

namespace Summer
{
    public class NetManager : MonoBehaviour
    {

        #region 属性

        public static NetManager _instance;
        public static NetManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    GameObject go = GameObjectHelper.CreateGameObject("NetManager", true);
                    _instance = go.AddComponent<NetManager>();
                }
                return _instance;
            }
        }

        public SocketManager _net_manager;

        #endregion

        #region MONO Override

        private void Awake()
        {
            _net_manager = new SocketManager();
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnDestroy()
        {
            if (_net_manager != null)
                _net_manager.CloseConnection();
            _net_manager = null;
        }


        #endregion

        #region static 

        public static void Send<T>(int cmd, T t) where T : global::ProtoBuf.IExtensible
        {
            Instance.SendMessage<T>(cmd, t);
        }

        #endregion

        #region Public

        public void SendMessage<T>(int cmd, T t) where T : global::ProtoBuf.IExtensible
        {
            if (_net_manager == null) return;
            _net_manager.SendPacket<T>(cmd, t);
        }

        #endregion

        #region Private Methods



        #endregion
    }
}