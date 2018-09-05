
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
    public class PlaySoundLeafNode : SkillLeafNode
    {
        public const string DES = "播放声音";
        public string sound_name;              //特效名称
        public Vector3 _position;

        public override void OnEnter(EntityBlackBoard blackboard)
        {
            LogEnter();

            PlaySoundEventData data = EventDataFactory.Pop<PlaySoundEventData>();
            data.sound_name = sound_name;
            data.position = _position;

            RaiseEvent(E_EntityInTrigger.play_sound, data);
            Finish();
        }

        public override void OnExit(EntityBlackBoard blackboard)
        {
            LogExit();
        }

        public override void OnUpdate(float dt, EntityBlackBoard blackboard)
        {

        }

        public override string ToDes() { return DES; }
    }

    public class PlaySoundLeafNodeByAnimation : PlaySoundLeafNode
    {

    }
}
