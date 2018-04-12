using System.Collections.Generic;
using UnityEngine;

namespace Summer
{
    public class SkillContainer : I_Update
    {
        public Dictionary<int, SkillSequence> _skill_map
            = new Dictionary<int, SkillSequence>(8);                                        // SkillNode cur_state;
        public SkillSequence _curr_sequen;                                                  // 当前序列
        public BaseEntities _entity;
        public float _last_time;
        public SkillContainer(BaseEntities entity)
        {
            I_SkillSequenceFactory skill_sequence_normal = new SkillSequenceNormal();
            _entity = entity;
            _skill_map.Clear();
            _skill_map.Add(1, skill_sequence_normal.Create(this));
        }

        #region Update

        public void OnUpdate(float dt)
        {
            // 通过时间来触发事件，
            SkillContainerTest.OnUpdate(dt);

            if (_curr_sequen == null) return;

            _curr_sequen.OnUpdate(dt);

            // TODO test code
            float curr_time = TimerHelper.RealtimeSinceStartup();
            if (curr_time - _last_time > 15.0)
            {
                LogManager.Error("技能释放错误,超过时间，Skill:{0}", _curr_sequen);
                _last_time = TimerHelper.RealtimeSinceStartup();
            }
        }

        #endregion

        #region public 

        public void CastSkill(int id)
        {
            _curr_sequen = _skill_map[id];
            _curr_sequen.OnStart();
            _last_time = TimerHelper.RealtimeSinceStartup();

        }

        // 接收到指定的事件
        public void ReceiveTransitionEvent(E_SkillTransition transition_event)
        {
            if (_curr_sequen != null)
                _curr_sequen.ReceiveWithOutEvent(transition_event);
        }

        public I_EntityInTrigger GetTrigger()
        {
            return _entity.GetTrigger();
        }

        #endregion
    }
}

