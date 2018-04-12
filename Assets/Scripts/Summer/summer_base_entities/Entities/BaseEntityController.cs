using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Summer
{
    public class BaseEntityController : MonoBehaviour, I_Update, I_EntityInTrigger
    {
        #region 属性

        public BaseEntities _entity;

        public EntityAnimationGroup _anim_group;
        public EventSet<E_EntityInTrigger, EventEntityData> _skill_event_set
           = new EventSet<E_EntityInTrigger, EventEntityData>();

        #endregion

        #region Mono Override

        void Awake()
        {
            _entity = new BaseEntities(this);

            RegisterHandler(E_EntityInTrigger.play_animation, PlayAnimation);
            RegisterHandler(E_EntityInTrigger.find_targets, FindTargets);
        }

        private void OnDestroy()
        {
            UnRegisterHandler(E_EntityInTrigger.play_animation, PlayAnimation);
        }

        public bool flag_skill;
        private void Update()
        {
            if (flag_skill)
            {
                _entity.CastSkill();
                flag_skill = false;
            }
        }

        #endregion

        #region I_Update Update

        public void OnUpdate(float dt)
        {
            if (_entity == null) return;
            _entity.OnUpdate(dt);
        }

        #endregion

        #region public 

        // 由动作文件触发的外部事件
        public void SkillEvent(E_SkillTransition skill_event)
        {
            _entity._skill_container.ReceiveTransitionEvent(skill_event);
        }

        #region Override 监听人物的内部事件

        public bool RegisterHandler(E_EntityInTrigger key, EventSet<E_EntityInTrigger, EventEntityData>.EventHandler handler)
        {
            return _skill_event_set.RegisterHandler(key, handler);
        }

        public bool UnRegisterHandler(E_EntityInTrigger key, EventSet<E_EntityInTrigger, EventEntityData>.EventHandler handler)
        {
            return _skill_event_set.UnRegisterHandler(key, handler);
        }

        // 被内部调用，由内部触发
        public void RaiseEvent(E_EntityInTrigger key, EventEntityData param)
        {
            _skill_event_set.RaiseEvent(key, param);
        }

        #endregion

        #region 监听的事件

        public void PlayAnimation(EventEntityData param)
        {
            PlayAnimationEventData data = param as PlayAnimationEventData;
            if (data == null || _entity == null) return;
            _anim_group.PlayAnim(data.animation_name);
        }

        public void FindTargets(EventEntityData param)
        {
            
        }

        #endregion

        #endregion
    }
}

