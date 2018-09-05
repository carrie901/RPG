
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
    /// 运动模糊
    /// </summary>
    public class PlayCameraMotionBlurEffect : SequenceLeafNode
    {
        public const string DES = "运动模糊";
        public PlayCameraMotionBlurEffectEventSkill _data;
        public override void OnEnter(BlackBoard blackboard)
        {
            if (_data == null)
                _data = EventDataFactory.Pop<PlayCameraMotionBlurEffectEventSkill>();
            GameEventSystem.Instance.RaiseEvent(E_GLOBAL_EVT.camera_effect_motion_blur, _data);
            Finish();
        }

        public override void OnExit(BlackBoard blackboard)
        {

        }

        public override void OnUpdate(float dt, BlackBoard blackboard)
        {

        }
        public override void SetConfigInfo(EdNode cnf)
        {

        }
        public override string ToDes() { return DES; }
    }
}
