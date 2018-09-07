
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

using UnityEngine;

namespace Summer.Sequence
{
    public class PlayPositionTargetLeafNode : SequenceLeafNode
    {
        public const string DES = "移动到指定地点";
        public string _bb_target;

        public Vector3 _target_pos;
        public BaseEntity _entity;
        public Vector3 _source_pos;

        public override void OnEnter(BlackBoard blackboard)
        {
            LogEnter();
            _target_pos = blackboard.GetValue<BbV3>(_bb_target).Value;
            _entity = blackboard.GetValue<BaseEntity>(SequenceSelfConst.TARGET_SELF_ENTITY);
            _source_pos = _entity.WroldPosition;
            _curr_frame = 0;
        }
        
        public override void OnExit(BlackBoard blackboard)
        {
            LogExit();
            _entity.EntityController._trans.position = _target_pos;
        }
        public override void SetConfigInfo(EdNode cnf)
        {

        }

        public int _curr_frame = 0;
        public override void OnUpdate(float dt, BlackBoard blackboard)
        {
            SkillLog.Log("位置偏移-->OnUpdate:[{0}]", TimeManager.FrameCount);
            _curr_frame++;
            // 有问题的
            _entity.EntityController._trans.position = Vector3.Lerp(_source_pos, _target_pos, _curr_frame * 1.0f / _frame_length);
        }

        public override string ToDes() { return DES; }
    }
}