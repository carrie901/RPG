
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
    public class PlayCameraRadialBlurEffectEventSkill : EventSetData
    {
        public float duration;
        public float fade_in;
        public float fade_out;
        public float strength;
    }

    /// <summary>
    /// 径向模糊:图像旋转成从中心辐射。
    /// </summary>
    public class PlayCameraRadialBlurEffect : SequenceLeafNode
    {
        public const string DES = "径向模糊";
        public float duration;
        public float fade_in;
        public float fade_out;
        public float strength;
        public override void OnEnter(BlackBoard blackboard)
        {
            //LogEnter();
            PlayCameraRadialBlurEffectEventSkill data = EventDataFactory.Pop<PlayCameraRadialBlurEffectEventSkill>();
            data.duration = duration;
            data.fade_in = fade_in;
            data.fade_out = fade_out;
            data.strength = strength;

            GameEventSystem.Instance.RaiseEvent(E_GLOBAL_EVT.camera_effect_radial_blur, data);
            Finish();
        }

        public override void OnExit(BlackBoard blackboard)
        {
            //LogExit();
        }

        public override void SetConfigInfo(EdNode cnf)
        {
            
        }

        public override string ToDes() { return DES; }
    }
}