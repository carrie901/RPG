
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


namespace Summer.Sequence
{
    /// <summary>
    /// 镜头抖动
    /// </summary>
    public class PlayCameraShake : SequenceLeafNode
    {
        public const string DES = "镜头抖动";

        public override void OnEnter(BlackBoard blackboard)
        {
            //LogEnter();
            PlayCameraShakeEventSkill data = EventDataFactory.Pop<PlayCameraShakeEventSkill>();
            GameEventSystem.Instance.RaiseEvent(E_GLOBAL_EVT.camera_shake, data);
            Finish();
        }

        public override void OnExit(BlackBoard blackboard)
        {

        }
        public override void SetConfigInfo(EdNode cnf)
        {

        }
        public override string ToDes() { return DES; }
    }
}