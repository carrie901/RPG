using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Summer
{
    // TODO BaseEntityController和 BaseEntities的关系
    // 目前感觉BaseEntities持有BaseEntityController会比较好一点
    // 这种情况下如果没有BaseEntityController 也可以运行游戏

    /// <summary>
    /// TODO 4.16 QAQ 
    ///     QAQ: 相对于细节的多变性，相对而已抽象的东西要稳定的多
    ///     1. 由于I_EntityInTrigger.GetEntity(); 在原子节点中又遇到了老的buff节点遇到一样的问题
    ///     节点如果承担实际的原子操作（有关于自身的，也有外部的比如震屏），那么节点必须要有相关的自身引用
    ///         1.这里的处理方式有2种 1.通过一个ContextModule作为中介来得到双方的引用，进行处理相关问题
    ///         2.直接通过I_EntityInTrigger Raise触发事件，然后在I_EntityInTrigger的子类中进行处理
    ///         在没有对BaseEntity进行很好的抽象之前,原子操作的部分同时和功能内聚是有一定的冲突的
    /// </summary>
    public class BaseEntityController : MonoBehaviour, I_Update, I_EntityInTrigger
    {
        #region 属性

        public BaseEntities _entity;

        public EntityAnimationGroup _anim_group;
        public Rigidbody rigid_body = null;
        public float turn_smoothing;                                                                    // 翻转速度

        [HideInInspector]
        public Transform trans;                                                                         // 缓存Transform
        [HideInInspector]
        public List<BaseEntityController> _targets = new List<BaseEntityController>();                  // 目标

        public Vector3 WroldPosition { get { return trans.position; } }                                 // 世界坐标
        public Vector3 Direction { get { return trans.forward; } }                                      // 当前方向

        public EventSet<E_EntityInTrigger, EventSetData> _skill_event_set
          = new EventSet<E_EntityInTrigger, EventSetData>();
        #endregion

        #region Mono Override

        void Awake()
        {
            trans = transform;



            RegisterHandler(E_EntityInTrigger.play_animation, PlayAnimation);
            RegisterHandler(E_EntityInTrigger.skill_release, ReleaseSkill);
            RegisterHandler(E_EntityInTrigger.skill_finish, FinishSkill);
            RegisterHandler(E_EntityInTrigger.find_targets, FindTargets);
            RegisterHandler(E_EntityInTrigger.export_to_target, ExportToTarget);
        }

        private void OnDestroy()
        {
            _skill_event_set.Clear();
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

        public void InitEntity(int hero_id)
        {
            _entity = new BaseEntities(this, hero_id);
        }

        /// <summary>
        /// 旋转角度
        /// </summary>
        public void OnRotating(float horizontal, float vertival)
        {
            Vector3 target_direction = new Vector3(horizontal, 0f, vertival);
            OnRotating(target_direction);
        }

        public void OnRotating(Vector3 target_direction)
        {
            Quaternion target_rotation = Quaternion.LookRotation(target_direction, Vector3.up);
            // 根据玩家的旋转创建一个更接近目标旋转的增量旋转。
            Quaternion new_rotation = Quaternion.Lerp(rigid_body.rotation, target_rotation, turn_smoothing * Time.deltaTime);

            rigid_body.MoveRotation(new_rotation);
        }

        public void OnAttackNormal()
        {
            _entity.CastSkill();
        }

        public void OnCastSkill(int skill_id)
        {
            _entity.CastSkill(skill_id);
        }

        #region 由动作文件触发的外部事件

        // 由动作文件触发的外部事件
        public void SkillEvent(E_SkillTransition skill_event)
        {
            _entity._skill_set.ReceiveTransitionEvent(skill_event);
        }

        #endregion

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
            _anim_group.PlayAnimation(data.animation_name);
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
            int length = _targets.Count;
            for (int i = 0; i < length; i++)
            {
                LogManager.Log("对目标:[{0}]造成[{1}]点伤害", _targets[i].name, data.damage);
            }
            _targets.Clear();
        }

        public void ReleaseSkill(EventSetData param)
        {
            _entity._skill_set._skill_container.ReleaseSkill();
        }

        public void FinishSkill(EventSetData param)
        {
            _entity._skill_set._skill_container.FinishSkill();
        }

        #endregion

        #endregion
    }
}

