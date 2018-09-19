
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

using System.Collections.Generic;
using UnityEngine;
namespace Summer
{
    public class MeshImage : MonoBehaviour
    {
        #region 属性

        public Vector2 Size;
        public string TexName;
        public bool Rebuild { get; set; }
        public Transform Trans { get; set; }
        private Vector3 _lastPos;
        public MeshRectInfo _info;
        #endregion

        #region MONO Override

        void Awake()
        {
            Trans = transform;
            // TODO Factory Get
            _info = new MeshRectInfo();
            _info.TexName = TexName;
            _info.Id = gameObject.GetInstanceID();
            _info.Pos = Trans.localPosition;
            _info.Size = Size;
            _info.PosReBuild = false;
        }

        void OnDisable()
        {
            Rebuild = true;
        }

        void OnEnable()
        {
            Rebuild = true;
        }

        void Update()
        {
            OnUpdate();
        }

        void OnUpdate()
        {
            if (Trans.localPosition == _lastPos) return;
            _info.PosReBuild = true;
            _info.Pos = Trans.localPosition;
            _lastPos = Trans.localPosition;
        }

        #endregion
    }
}