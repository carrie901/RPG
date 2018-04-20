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
        public BaseEntityController EntityController { get; private set; }                             // GameObject控制器

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

        #region 构造

        #endregion

        #region Override 各种接口

        #region OnUpdate

        public void OnUpdate(float dt)
        {
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

        #region 注册监听人物的内部事件

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

        public BaseEntity GetEntity()
        {
            return this;
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
            BaseEntityController go = TransformPool.Instance.Pop<BaseEntityController>("Prefab/Model/Character/" + _cnf.prefab_name);
            EntityController = go;
            EntityController.InitOutTrigger(this, this);
            _fsm_system.Start();
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

        #region 监听的内部事件

        public void PlayAnimation(EventSetData param)
        {
            EntityActionFactory.OnAction<EntityPlayAnimationAction>(this, param);
        }

        public void ChangeAnimationSpeed(EventSetData param)
        {
            EntityActionFactory.OnAction<EntityChangeAnimationSpeedAction>(this, param);
        }

        public void PlayEffect(EventSetData param)
        {
            EntityActionFactory.OnAction<EntityPlayEffectAction>(this, param);
        }

        public void FindTargets(EventSetData param)
        {
            EntityActionFactory.OnAction<EntityFindTargetAction>(this, param);
        }

        public void ExportToTarget(EventSetData param)
        {
            EntityActionFactory.OnAction<EntityExportToTarget>(this, param);

        }

        public void EntityDie(EventSetData param)
        {
            //_fsm_system.PerformTransition(E_StateId.die);
        }

        public void ChangeState(EventSetData param)
        {
            ChangeEntityStateEventData data = param as ChangeEntityStateEventData;
            if (data == null) return;
            //_fsm_system.PerformTransition(data.state_id);
        }

        public void ReleaseSkill(EventSetData param)
        {
            _skill_set._skill_container.ReleaseSkill();
        }

        public void FinishSkill(EventSetData param)
        {
            _skill_set._skill_container.FinishSkill();
            //_fsm_system.PerformTransition(E_StateId.idle);
        }

        #endregion

        #region 监听的外部事件

        public void ReceiveAnimationEvent(EventSetData param)
        {
            AnimationEventData data = param as AnimationEventData;
            if (data == null) return;
            _skill_set.ReceiveTransitionEvent(data.event_data);
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
            RegisterHandler(E_EntityInTrigger.entity_die, EntityDie);
            RegisterHandler(E_EntityInTrigger.change_state, ChangeState);

            RegisterHandler(E_EntityOutTrigger.animation_event, ReceiveAnimationEvent);
        }


        #endregion

        #region


        #endregion

        #region public

        public bool IsDead() { return false; }
        public string ToDes() { return ""; }
        public int skill_id= 10008;//10013
        public void CastSkill()
        {
            if (_skill_set == null) return;
            if (skill_id == 0)
                _skill_set.CastAttack();
            else
                _skill_set.CastSkill(skill_id);
            //_fsm_system.PerformTransition(E_StateId.attack);
        }

        public void CastSkill(int skill_id)
        {
            if (_skill_set == null) return;
            _skill_set.CastSkill(skill_id);
        }

        public EntityMoveCommand move_command = new EntityMoveCommand();
        public void ReceiveCommandMove(Vector2 diretion)
        {
            if (!CanMovement) return;
            //_fsm_system.PerformTransition(E_StateId.move);
            EntityController.AddDirection(diretion);
        }

        public void InitPosRot()
        {
            EntityController.trans.position = new Vector3(Random.value * 40 - 20f, 0, Random.value * 40 - 20f);
        }

        public I_EntityInTrigger GetTrigger()
        {
            return this;
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