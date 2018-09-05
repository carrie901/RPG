
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

namespace Summer.Sequence
{
    public class PlayCameraOffset : SequenceLeafNode
    {
        public const string DES = "摄像头偏移";

        public Vector3 _offset;
        public Vector3 _rotaion;
        public float time = 0.01f;
        public override void OnEnter(BlackBoard blackboard)
        {
            //LogEnter();
            CameraSource camera_source = new CameraSource();
            camera_source._data._offset = _offset;
            camera_source._data._rotaion = _rotaion;
            camera_source._timer = time;

            GameEventSystem.Instance.RaiseEvent(E_GLOBAL_EVT.camera_source_add, camera_source);
            Finish();
        }

        public override void OnExit(BlackBoard blackboard)
        {
            //LogExit();
        }

        public override void OnUpdate(float dt, BlackBoard blackboard)
        {
            base.OnUpdate(dt, blackboard);
        }
        public override void SetConfigInfo(EdNode cnf)
        {

        }
        public override string ToDes() { return DES; }
    }
}