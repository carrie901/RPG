
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

using Summer.Sequence;
using UnityEngine;
using System.Collections.Generic;

namespace Summer
{
    public class SkillSet_1 : I_Update, I_RegisterHandler
    {

        #region 属性

        public BaseEntity _entity;
        public SequenceLine _currSequece;                                               // 当前技能序列
        public bool _canCastSkill;                                                      // 可以释放下一个技能了
        public Dictionary<int, SequenceLine> _map = new Dictionary<int, SequenceLine>();
        #endregion

        #region Public

        public SkillSet_1(BaseEntity entity)
        {

            _entity = entity;
            Init();

        }

        public void OnReset(int heroTemplateId) { }

        public EntityBlackBoard GetBlackboard()
        {
            return _entity.GetBlackBorad();
        }

        public bool CastSkill(int id)
        {
            if (!_map.ContainsKey(id)) return false;
            SkillLog.Log("======================释放技能:[{0}]======================", id);
            _currSequece = _map[id];
            _currSequece.Start(_entity);
            /* //if (!CanCastSkill()) return false;
             EntityEventFactory.ChangeInEntityState(_entity, E_StateId.attack);
             //SkillLog.Assert(!_test, "释放技能bug:[{0}]", id);
             SkillLog.Log("======================释放技能:[{0}]======================", id);
             //_test = true;
             _canCastSkill = false;

             _currSequece = SkillFactory.Create();
             _currSequece.GetBlackBorad().SetValue(SequenceSelfConst.TARGET_POSITION1, new Vector3(1.87f, 0.1f, 3.76f));

             _currSequece.GetBlackBorad().SetValue(SequenceSelfConst.TARGET_POSITION2, _entity.EntityController.WroldPosition);

             _currSequece.GetBlackBorad().SetValue(SequenceSelfConst.TARGET_SELF_ENTITY, _entity);
             _currSequece.Start();*/
            return true;
        }

        public bool CanCastSkill()
        {
            return _canCastSkill;
        }
        #endregion

        #region Override I_Update/I_RegisterHandler

        public void OnUpdate(float dt)
        {
            if (_currSequece == null) return;
            _currSequece.OnUpdate(dt);
        }

        public void OnRegisterHandler()
        {

        }

        public void UnRegisterHandler()
        {

        }

        #endregion

        #region Private Methods

        private void Init()
        {
            _map.Clear();
            TextAsset textAsset = Resources.Load<TextAsset>("Config/Skill/02");
            ResMd resMd = new ResMd();
            resMd.ParseText(textAsset.text);
            List<EdNode> nodes = resMd._root_node.GetNodes("Sequence");
            int length = nodes.Count;
            for (int i = 0; i < length; i++)
            {
                SequenceLine line = SkillFactory.Create(nodes[i]);
                if (line == null) continue;
                _map.Add(line.Id, line);
            }
        }

        #endregion

    }
}