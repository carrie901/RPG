
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

namespace Summer
{
    public interface I_EntityAnimationGroup
    {
        void OnInit(I_EntityInTrigger entity);
        void OnRegisterHandler();
        void UnRegisterHandler();
        /// <summary>
        /// 动作触发事件
        /// </summary>
        void SkillEvent(E_SkillTransition skillEvent);
        /// <summary>
        /// 播放动作
        /// </summary>
        void PlayAnimation(string animName);

        void StopAnim(string animName);

        void ChangeAnimationSpeed(float speed);
        void Clear();
    }

}

