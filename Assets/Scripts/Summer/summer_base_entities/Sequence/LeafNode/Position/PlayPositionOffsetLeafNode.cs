﻿
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
    /// <summary>
    /// 位置相关的如：瞬移、冲撞、击退、跳跃等
    /// </summary>
    public class PlayPositionOffsetLeafNode : SequenceLeafNode
    {
        public const string DES = "位置偏移";
        public Vector3 _targetPos;
        public BaseEntity _entity;
        public Vector3 _sourcePos;

        public Vector3 _speed;
        public override void OnEnter(BlackBoard blackboard)
        {
            LogEnter();
            _targetPos = blackboard.GetValue<BbV3>(SequenceSelfConst.TARGET_POSITION1).Value;
            _entity = blackboard.GetValue<BaseEntity>(SequenceSelfConst.TARGET_SELF_ENTITY);
            _sourcePos = _entity.WroldPosition;
            //SkillLog.Log("位置偏移-->OnEnter");
        }

        public override void OnExit(BlackBoard blackboard)
        {
            LogExit();
            _entity.EntityController._trans.position = _targetPos;
        }
        public override void SetConfigInfo(EdNode cnf)
        {

        }
        public override void OnUpdate(float dt, BlackBoard blackboard)
        {
            //Finish();
            // 如果到达目标地点
            //SkillLog.Log("位置偏移-->OnUpdate:[{0}]", TimeManager.FrameCount);

            // 有问题的
            _entity.EntityController._trans.position = Vector3.SmoothDamp(_entity.WroldPosition, _targetPos, ref _speed, 2);
        }

        public override string ToDes() { return DES; }
    }
}
