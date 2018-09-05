
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
using Summer.Sequence;
using UnityEngine;

namespace Summer
{
    public class SkillSet_1 : I_Update, I_RegisterHandler
    {

        #region 属性

        public BaseEntity _entity;
        public SequenceLine _curr_sequece;                                                  // 当前技能序列
        public bool _can_cast_skill;                                                        // 可以释放下一个技能了
        #endregion

        #region Public

        public SkillSet_1(BaseEntity entity)
        {
            _entity = entity;
        }

        public void OnReset(int hero_template_id) { }

        public EntityBlackBoard GetBlackboard()
        {
            return _entity.GetBlackBorad();
        }

        public bool CastSkill(int id)
        {
            //if (!CanCastSkill()) return false;
            EntityEventFactory.ChangeInEntityState(_entity, E_StateId.attack);
            //SkillLog.Assert(!_test, "释放技能bug:[{0}]", id);
            SkillLog.Log("======================释放技能:[{0}]======================", id);
            //_test = true;
            _can_cast_skill = false;

            _curr_sequece = SkillFactory.Create();
            _curr_sequece.GetBlackBorad().SetValue(SequenceSelfConst.TARGET_POSITION1, new Vector3(1.87f, 0.1f, 3.76f));

            _curr_sequece.GetBlackBorad().SetValue(SequenceSelfConst.TARGET_POSITION2, _entity.WroldPosition);

            _curr_sequece.GetBlackBorad().SetValue(SequenceSelfConst.TARGET_SELF_ENTITY, _entity);
            _curr_sequece.Start();
            return true;
        }

        public bool CanCastSkill()
        {
            return _can_cast_skill;
        }
        #endregion

        #region Override I_Update/I_RegisterHandler

        public void OnUpdate(float dt)
        {
            if (_curr_sequece == null) return;
            _curr_sequece.OnUpdate(dt);
        }

        public void OnRegisterHandler()
        {

        }

        public void UnRegisterHandler()
        {

        }

        #endregion

        #region Private Methods



        #endregion

    }
}