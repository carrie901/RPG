
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
    /// 播放角色动作
    /// </summary>
    public class ChangeAnimationSpeedLeafNode : SequenceLeafNode
    {
        public const string DES = "改变动作速度";

        public float speed = 0;
        public override void OnEnter(BlackBoard blackboard)
        {
            //LogEnter();
            AnimationSpeedEventData data = EventDataFactory.Pop<AnimationSpeedEventData>();
            data.animation_speed = speed;

            SequenceHelper.Raise(_context._owner, E_EntityInTrigger.CHANGE_ANIMATION_SPEED, data);
        }

        public override void OnExit(BlackBoard blackboard)
        {
            //LogExit();
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

