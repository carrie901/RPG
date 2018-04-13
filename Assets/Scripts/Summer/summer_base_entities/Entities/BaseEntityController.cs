using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Summer
{
    // TODO BaseEntityController和 BaseEntities的关系
    // 目前感觉BaseEntities持有BaseEntityController会比较好一点
    // 这种情况下如果没有BaseEntityController 也可以运行游戏
    public class BaseEntityController : MonoBehaviour, I_Update, I_EntityInTrigger
    {
        #region 属性

        public BaseEntities _entity;

        public EntityAnimationGroup _anim_group;
        public EventSet<E_EntityInTrigger, EventSetData> _skill_event_set
           = new EventSet<E_EntityInTrigger, EventSetData>();

        public Transform trans;                                                                         // 缓存Transform
        public List<BaseEntityController> _targets = new List<BaseEntityController>();                  // 目标

        public Vector3 WroldPosition { get { return trans.position; } }                                 // 世界坐标
        public Vector3 Direction { get { return trans.forward; } }                                      // 当前方向

        #endregion

        #region Mono Override

        void Awake()
        {
            trans = transform;
            _entity = new BaseEntities(this);

            RegisterHandler(E_EntityInTrigger.play_animation, PlayAnimation);
            RegisterHandler(E_EntityInTrigger.find_targets, FindTargets);
            RegisterHandler(E_EntityInTrigger.export_to_target, ExportToTarget);
        }

        private void OnDestroy()
        {
            _skill_event_set.Clear();
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

        public bool RegisterHandler(E_EntityInTrigger key, EventSet<E_EntityInTrigger, EventSetData>.EventHandler handler)
        {
            return _skill_event_set.RegisterHandler(key, handler);
        }

        public bool UnRegisterHandler(E_EntityInTrigger key, EventSet<E_EntityInTrigger, EventSetData>.EventHandler handler)
        {
            return _skill_event_set.UnRegisterHandler(key, handler);
        }

        // 被内部调用，由内部触发
        public void RaiseEvent(E_EntityInTrigger key, EventSetData param)
        {
            _skill_event_set.RaiseEvent(key, param);
        }

        public BaseEntities GetEntity()
        {
            return _entity;
        }

        #endregion

        #region 监听的事件

        public void PlayAnimation(EventSetData param)
        {
            PlayAnimationEventData data = param as PlayAnimationEventData;
            if (data == null || _entity == null) return;
            _anim_group.PlayAnim(data.animation_name);
        }

        public void FindTargets(EventSetData param)
        {
            EntityFindTargetData data = param as EntityFindTargetData;
            if (data == null || _entity == null) return;
            _targets.AddRange(data._targets);
        }

        public void ExportToTarget(EventSetData param)
        {
            EntityExportToTargetData data = param as EntityExportToTargetData;
            if (data == null || _entity == null) return;
            for (int i = 0; i < _targets.Count; i++)
            {
                LogManager.Log("对目标:[{0}]造成[{1}]点伤害", _targets[i].name, data.damage);
            }
            _targets.Clear();
        }

        #endregion

        #endregion
    }
}

