using System.Collections.Generic;
using UnityEngine;

namespace Summer
{
    public class SkillContainer : I_Update
    {
        public Dictionary<int, SkillSequence> _skill_map
            = new Dictionary<int, SkillSequence>(8);                                        // SkillNode cur_state;
        public SkillSequence _curr_sequen;                                                  // 当前序列
        public EventSet<E_EntityOutTrigger, EventEntityData> _skill_event_set
           = new EventSet<E_EntityOutTrigger, EventEntityData>();

        public SkillModule _module;
        public SkillContainer(BaseEntities entity)
        {
            _module = new SkillModule(entity);
            _skill_map.Clear();
            _skill_map.Add(1, SkillContainerTest.Create());
        }

        #region Update

        public void OnUpdate(float dt)
        {
            // 通过时间来触发事件，
            SkillContainerTest.OnUpdate(dt);

            if (_curr_sequen == null) return;

            _curr_sequen.OnUpdate(dt);
        }

        #endregion

        public void CastSkill(int id)
        {
            _curr_sequen = _skill_map[id];
            _curr_sequen.OnStart();
        }

        // 接收到指定的事件
        public void ReceiveTransitionEvent(E_SkillTransition transition_event)
        {
            if (_curr_sequen != null)
                _curr_sequen.ReceiveWithOutEvent(transition_event);
        }

        #region 角色注册事件，内部子节点触发事件

        public bool RegisterHandler(E_EntityOutTrigger key, EventSet<E_EntityOutTrigger, EventEntityData>.EventHandler handler)
        {
            return _skill_event_set.RegisterHandler(key, handler);
        }

        public bool UnRegisterHandler(E_EntityOutTrigger key, EventSet<E_EntityOutTrigger, EventEntityData>.EventHandler handler)
        {
            return _skill_event_set.UnRegisterHandler(key, handler);
        }

        public void RaiseEvent(E_EntityOutTrigger key, EventEntityData obj_info)
        {
            _skill_event_set.RaiseEvent(key, obj_info, false);
        }

        #endregion
    }
}

