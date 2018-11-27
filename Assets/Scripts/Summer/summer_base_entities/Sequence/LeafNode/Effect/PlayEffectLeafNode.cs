
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
    public class PlayEffectLeafNode : SequenceLeafNode
    {
        public const string EFFECT_NAME = "EffectName";
        public const string DES = "播放特效";
        public string _effectName;             //特效名称
        //public GameObject bing_obj;            //绑定的GameObject
        public override void OnEnter(BlackBoard blackboard)
        {
            LogEnter();
            PlayEffectEventSkill data = EventDataFactory.Pop<PlayEffectEventSkill>();
            data.effect_name = _effectName;
            //data.bing_obj = bing_obj;
            RaiseEvent(E_EntityInTrigger.PLAY_EFFECT, data);
            Finish();
        }

        public override void OnExit(BlackBoard blackboard)
        {
            //LogExit();
        }
        public override void SetConfigInfo(EdNode cnf)
        {
            _effectName = cnf.GetAttribute(EFFECT_NAME).ToStr();
        }
        public override string ToDes() { return DES; }
    }
}