using System.Collections.Generic;
using UnityEngine;

namespace Summer
{
    /// <summary>
    /// 先把BaseEntities当做BaseCharacter用
    /// TODO 4.12
    /// QAQ:
    ///     根据高内聚原则,entity相关的方法最好内聚在本类，如果需求是多样化的。那么提供get方法让外部进行操作，但最总基础的方法还是在本类
    /// QAQ:
    ///     关于涉及到生命周期的一些东西，比如技能特效，声音，目前不太好处理，目前把这些东西放到SkillModule中
    /// </summary>
    public class BaseEntity : I_CharacterProperty, I_Update, I_EntityInTrigger, I_EntityOutTrigger, I_EntityLife
    {
        #region 属性

        public int Template { get; private set; }
        public BaseEntityController EntityController { get; private set; }                              // GameObject控制器
        public Vector3 WroldPosition { get { return EntityController.trans.position; } }                // 世界坐标
        public Vector3 Direction { get { return EntityController.trans.forward; } }                     // 当前方向
        public EntityId entity_id;                                                                      // Entity的唯一表示
        //public BuffContainer _buff_container;                                                           // Buff容器
        public SkillSet _skill_set;
        public HeroInfoCnf _cnf;
        public EntitiesAttributeProperty _attr_prop;                                                    // 人物属性
        public FsmSystem _fsm_system;

        public List<BaseEntity> _targets = new List<BaseEntity>();                                      // 目标
        public List<I_Update> update_list = new List<I_Update>();
        public EventSet<E_EntityOutTrigger, EventSetData> _out_event_set                                // 角色的外部事件
           = new EventSet<E_EntityOutTrigger, EventSetData>();
        public EventSet<E_EntityInTrigger, EventSetData> _in_event_set                                  // 角色的内部事件
       = new EventSet<E_EntityInTrigger, EventSetData>();

        public bool CanMovement { get; set; }

        #endregion

        #region Override 各种接口

        #region OnUpdate

        public void OnUpdate(float dt)
        {
            EntityController.OnUpdate(dt);
            int length = update_list.Count;
            for (int i = 0; i < length; i++)
            {
                update_list[i].OnUpdate(dt);
            }
        }

        #endregion

        #region 得到Entity的属性和数值

        public AttributeIntParam FindAttribute(E_CharAttributeType type)
        {
            return _attr_prop.FindAttribute(type);
        }

        public float FindValue(E_CharValueType type)
        {
            return _attr_prop.FindValue(type);
        }

        public void ChangeValue(E_CharValueType type, float value)
        {
            _attr_prop.ChangeValue(type, value);
        }

        public void ResetValue(E_CharValueType type, float value)
        {
            _attr_prop.ResetValue(type, value);
        }

        #endregion

        #region 注册监听人物的外部事件

        public bool RegisterHandler(E_EntityOutTrigger key, EventSet<E_EntityOutTrigger, EventSetData>.EventHandler handler)
        {
            return _out_event_set.RegisterHandler(key, handler);
        }

        public bool UnRegisterHandler(E_EntityOutTrigger key, EventSet<E_EntityOutTrigger, EventSetData>.EventHandler handler)
        {
            return _out_event_set.UnRegisterHandler(key, handler);
        }

        public void RaiseEvent(E_EntityOutTrigger key, EventSetData obj_info)
        {
            if (_out_event_set == null) return;
            _out_event_set.RaiseEvent(key, obj_info);
        }

        #endregion

        #region 注册监听人物的内部事件 基本都是一些原子节点

        public bool RegisterHandler(E_EntityInTrigger key, EventSet<E_EntityInTrigger, EventSetData>.EventHandler handler)
        {
            return _in_event_set.RegisterHandler(key, handler);
        }

        public bool UnRegisterHandler(E_EntityInTrigger key, EventSet<E_EntityInTrigger, EventSetData>.EventHandler handler)
        {
            return _in_event_set.UnRegisterHandler(key, handler);
        }

        // 被内部调用，由内部触发
        public void RaiseEvent(E_EntityInTrigger key, EventSetData param)
        {
            _in_event_set.RaiseEvent(key, param);
        }

        public E_StateId GetState()
        {
            return _fsm_system.GetState();
        }

        #endregion

        #region 缓存池相关函数

        public void OnInit()
        {
            _skill_set = new SkillSet(this);
            _fsm_system = EntityFsmFactory.CreateFsmSystem(this);
        }

        public void OnPop(int hero_id)
        {
            CanMovement = true;
            Template = hero_id;
            RegisterHandler();
            entity_id = new EntityId();
            _attr_prop = new EntitiesAttributeProperty(entity_id);
            _cnf = StaticCnf.FindData<HeroInfoCnf>(Template);
            _skill_set.OnReset(Template);

            // 更新通道
            update_list.Add(_skill_set);
            //update_list.Add(_fsm_system);
            // 加载模型
            BaseEntityController go = TransformPool.Instance.Pop<BaseEntityController>("res_bundle/prefab/model/Character/" + "NPC_Zhaoyun_001_02"/*_cnf.prefab_name*/);
            EntityController = go;
            EntityController.InitOutTrigger(this, this);

            _fsm_system.Start();

            PlayAnimationEventData param = EventDataFactory.Pop<PlayAnimationEventData>();
            param.animation_name = "idle";
            EntityActionFactory.OnAction<EntityPlayAnimationAction>(this, param);
            EventDataFactory.Push(param);
        }

        public void OnPush()
        {
            Clear();
            TransformPool.Instance.Push(EntityController);
            EntityController = null;
        }

        public bool IsUse { get; set; }
        public string ObjectName { get { return "BaseEntity"; } }

        #endregion

        #endregion

        #region 监听的内部事件,通过一些原子节点触发事件，迫使Entity做出某些行为

        // Entity播放动画
        public void PlayAnimation(EventSetData param)
        {
            EntityActionFactory.OnAction<EntityPlayAnimationAction>(this, param);
        }

        // 改变动画的速率
        public void ChangeAnimationSpeed(EventSetData param)
        {
            EntityActionFactory.OnAction<EntityChangeAnimationSpeedAction>(this, param);
        }

        // 播放特效
        public void PlayEffect(EventSetData param)
        {
            EntityActionFactory.OnAction<EntityPlayEffectAction>(this, param);
        }

        // 找到目标
        public void FindTargets(EventSetData param)
        {
            EntityActionFactory.OnAction<EntityFindTargetAction>(this, param);
        }

        // 输出伤害到目标身上
        public void ExportToTarget(EventSetData param)
        {
            EntityActionFactory.OnAction<EntityExportToTargetAction>(this, param);
        }

        public void MoveToTargetPostion(EventSetData param)
        {
            EntityActionFactory.OnAction<MoveToTargetPositionAction>(this, param);
        }

        // 目标死亡
        public void EntityDie(EventSetData param)
        {
            EntityEventFactory.ChangeInEntityState(this, E_StateId.die);
        }

        // 改变当前玩家状态
        public void ChangeState(EventSetData param)
        {
            ChangeEntityStateEventData data = param as ChangeEntityStateEventData;
            if (data == null) return;
            if (GetState() == data.state_id && !data.force) return;
            SkillLog.Log("之前状态:{0},改变之后状态:{1}", GetState(), data.state_id);
            _fsm_system.PerformTransition(data.state_id);
        }

        // 释放技能的控制，可以播放下一技能
        public void ReleaseSkill(EventSetData param)
        {
            _skill_set._skill_container.ReleaseSkill();
        }

        // 技能结束
        public void FinishSkill(EventSetData param)
        {
            // 技能结束
            _skill_set._skill_container.FinishSkill();
        }

        #endregion

        #region 监听的外部事件，比如动作文件为源头，触发动作事件

        public void ReceiveAnimationEvent(EventSetData param)
        {
            AnimationEventData data = param as AnimationEventData;
            if (data == null) return;
            _skill_set.ReceiveTransitionEvent(data.event_data);
        }

        public void OnBeHurt(EventSetData param)
        {
            EntityEventFactory.ChangeInEntityState(this, E_StateId.hurt);
            /*PlayAnimationEventData data = EventDataFactory.Pop<PlayAnimationEventData>();
            data.animation_name = "hit";
            EntityActionFactory.OnAction<EntityPlayAnimationAction>(this, data);*/
        }

        #endregion

        #region 注册事件

        public void RegisterHandler()
        {
            RegisterHandler(E_EntityInTrigger.play_animation, PlayAnimation);
            RegisterHandler(E_EntityInTrigger.change_animation_speed, ChangeAnimationSpeed);
            RegisterHandler(E_EntityInTrigger.play_effect, PlayEffect);
            RegisterHandler(E_EntityInTrigger.skill_release, ReleaseSkill);
            RegisterHandler(E_EntityInTrigger.skill_finish, FinishSkill);
            RegisterHandler(E_EntityInTrigger.find_targets, FindTargets);
            RegisterHandler(E_EntityInTrigger.export_to_target, ExportToTarget);
            RegisterHandler(E_EntityInTrigger.move_to_target_position, MoveToTargetPostion);
            RegisterHandler(E_EntityInTrigger.entity_die, EntityDie);
            RegisterHandler(E_EntityInTrigger.change_state, ChangeState);

            RegisterHandler(E_EntityOutTrigger.animation_event, ReceiveAnimationEvent);
            RegisterHandler(E_EntityOutTrigger.on_be_hurt, OnBeHurt);
        }


        #endregion

        #region


        #endregion

        #region public

        public bool IsDead() { return false; }
        public string ToDes() { return ""; }
        public int _skill_id = 10007;//10013 // 10008
        public void CastSkill()
        {
            if (_skill_set == null) return;
            if (_skill_id == 0)
                _skill_set.CastAttack();
            else
                _skill_set.CastSkill(_skill_id);
        }

        public void CastSkill(int skill_id)
        {
            if (_skill_set == null) return;
            _skill_set.CastSkill(skill_id);
        }

        public void ReceiveCommandMove(Vector2 diretion)
        {
            if (!CanMovement) return;
            EntityController.AddDirection(diretion);
        }

        public void InitPosRot()
        {
            EntityController.trans.position = new Vector3(Random.value * 40 - 20f, 0, Random.value * 40 - 20f);
        }

        public void MoveToTargetPostion(Vector3 target_position, float speed)
        {
            EntityController.movement.MoveToTargetPostion(target_position, speed);
        }

        public virtual I_Transform GetTransform()
        {
            return null;
        }

        public void Clear()
        {
            _out_event_set.Clear();
            _in_event_set.Clear();
            _targets.Clear();
            update_list.Clear();
        }

        #endregion

    }
}