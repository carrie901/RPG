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
    //[RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(EntityAnimationGroup),typeof(EntityMovement))]
    public class BaseEntityController : PoolDefaultGameObject, I_Update
    {
        #region 属性

        protected I_EntityOutTrigger _out_trigger;
        public float turn_smoothing;                                                                    // 翻转速度


        [HideInInspector]
        public EntityAnimationGroup anim_group;
        [HideInInspector]
        public EntityMovement movement;
        [HideInInspector]
        public Transform trans;                                                                         // 缓存Transform
        [HideInInspector]
        public Rigidbody rigid_body = null;
        public Vector3 WroldPosition { get { return trans.position; } }                                 // 世界坐标
        public Vector3 Direction { get { return trans.forward; } }                                      // 当前方向


        #endregion

        #region Mono Override

        void Awake()
        {
            trans = transform;
            rigid_body = GetComponent<Rigidbody>();
            anim_group = GetComponent<EntityAnimationGroup>();
            movement = GetComponent<EntityMovement>();
            if (rigid_body != null)
                rigid_body.useGravity = false;
        }

        private void OnDestroy()
        {
        }

        #endregion

        #region I_Update Update

        public void OnUpdate(float dt)
        {
            /*if (entity == null) return;
            entity.OnUpdate(dt);*/

            /*RaycastHit hit;
            Vector3 p1 = trans.position;
            Vector3 p2 = p1 + Vector3.forward * 0.5f;
            if (Physics.CapsuleCast(p1, p2, 0.0f, transform.forward, out hit, 0.2f))
            {
            }*/
        }

        #endregion

        #region

        public override void OnPush()
        {
            base.OnPush();
            _out_trigger = null;
        }

        #endregion

        #region public 

        public void InitOutTrigger(I_EntityOutTrigger trigger)
        {
            _out_trigger = trigger;
        }

        /*public void InitEntity(int hero_id)
        {
            entity = new BaseEntity(this, hero_id);
        }*/

        /// <summary>
        /// 旋转角度
        /// </summary>
        public void OnRotating(float horizontal, float vertival)
        {
            //Vector3 target_direction = new Vector3(horizontal, 0f, vertival);
            //OnRotating(target_direction);
        }

        public void OnRotating(Vector3 target_direction)
        {
            /*Quaternion target_rotation = Quaternion.LookRotation(target_direction, Vector3.up);
            // 根据玩家的旋转创建一个更接近目标旋转的增量旋转。
            Quaternion new_rotation = Quaternion.Lerp(rigid_body.rotation, target_rotation, turn_smoothing * Time.deltaTime);

            rigid_body.MoveRotation(new_rotation);*/
        }

        #region 由动作文件触发的外部事件

        // 由动作文件触发的外部事件
        public void SkillEvent(E_SkillTransition skill_event)
        {
            AnimationEventData param = EventDataFactory.Pop<AnimationEventData>();
            param.event_data = skill_event;
            _out_trigger.RaiseEvent(E_EntityOutTrigger.animation_event, param);
        }

        #endregion

        #endregion
    }
}

