
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
    /// <summary>
    /// 1.Sprite
    /// 2.Text 
    ///     这两个Selectable来操作
    /// </summary>
    public class GameObjectChange : MonoBehaviour
    {
        public GameObject _normalGo;
        public GameObject _disableGo;
        protected bool _state;
        // Use this for initialization
        void Awake()
        {
            _on_change();
        }

        public bool IsEnabled
        {
            get
            {
                return _state;
            }
            set
            {
                _state = value;
                _on_change();
            }
        }

        private void _on_change()
        {
            GameObjectHelper.SetActive(_normalGo, _state);
            GameObjectHelper.SetActive(_disableGo, !_state);
        }
    }
}
