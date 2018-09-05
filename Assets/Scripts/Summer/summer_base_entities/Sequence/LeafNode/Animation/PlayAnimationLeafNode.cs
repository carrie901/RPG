
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
    /// 
    /// </summary>
    public class PlayAnimationLeafNode : SequenceLeafNode
    {
        public const string DES = "播放动作";
        public string animation_name;

        public override void OnEnter(BlackBoard blackboard)
        {
            /* PlayAnimationEventData data = EventDataFactory.Pop<PlayAnimationEventData>();
             data.animation_name = animation_name;
             SequenceHelper.Raise(_context._owner, E_EntityInTrigger.play_animation, data);*/
            LogEnter();
            //Finish();
        }

        public override void OnExit(BlackBoard blackboard)
        {
            LogExit();
            //SkillLog.Log("播放动画-->OnExit");
        }

        public override void OnUpdate(float dt, BlackBoard blackboard)
        {
            base.OnUpdate(dt, blackboard);
        }

        public override void SetConfigInfo(EdNode cnf)
        {

        }

        public override string ToDes()
        {
            return DES;
        }
    }
}