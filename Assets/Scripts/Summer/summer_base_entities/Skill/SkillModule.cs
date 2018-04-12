using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Summer
{
    public class SkillModule : I_EntitySkill
    {
        public BaseEntities _entity;
        public EventSet<E_EntityOutTrigger, EventEntityData> _skill_event_set
           = new EventSet<E_EntityOutTrigger, EventEntityData>();
        public SkillModule(BaseEntities entity)
        {
            _entity = entity;
        }

        #region public

        public void Init()
        {
            RegisterHandler(E_EntityOutTrigger.play_animation, PlayAnimation);
            RegisterHandler(E_EntityOutTrigger.play_effect, PlayEffect);
            RegisterHandler(E_EntityOutTrigger.play_sound, PlaySound);
        }

        public void OnDestroy()
        {
            UnRegisterHandler(E_EntityOutTrigger.play_animation, PlayAnimation);
            UnRegisterHandler(E_EntityOutTrigger.play_effect, PlayEffect);
            UnRegisterHandler(E_EntityOutTrigger.play_sound, PlaySound);
        }


        #region 监听的事件

        public void PlayAnimation(EventEntityData param)
        {
            PlayAnimationEventData data = param as PlayAnimationEventData;
            if (data == null || _entity == null) return;
            _entity.PlayAnimation(data.animation_name);
        }

        public void PlayEffect(EventEntityData param)
        {
            if (_entity == null) return;
        }

        public void PlaySound(EventEntityData param)
        {
            if (_entity == null) return;


        }

        #endregion

        #endregion

        #region 技能流程

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
            _skill_event_set.RaiseEvent(key, obj_info);
        }

        #endregion
    }
}

