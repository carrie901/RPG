using System.Collections.Generic;
using Summer.AI;
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
    /// 
    /// 
    /// 目前没有明确性质的方案来判断到底原子操作的具体逻辑是放在原子操作中来完成好，还是通过由原子操作来进行注册触发好
    /// </summary>
    public class BaseEntity : /*I_CharacterProperty,*/ I_Update, I_EntityInTrigger, I_EntityOutTrigger, I_EntityLife/*, I_SkillBlackBorad*//*, I_RegisterHandler*/
    {
        #region 属性

        public int Template { get; private set; }
        public BaseEntityController EntityController { get; private set; }                              // GameObject控制器
        public Vector3 WroldPosition { get { return EntityController.trans.position; } }                // 世界坐标
        public Vector3 Direction { get { return EntityController.trans.forward; } }                     // 当前方向
        public bool CanMovement { get; set; }
        public EntityId entity_id;                                                                      // Entity的唯一表示
        public HeroInfoCnf _cnf;

        // TODO 是否通过黑箱进行操作 来规避掉内部的响应
        public List<BaseEntity> _targets = new List<BaseEntity>();                                       // 目标 是否通过黑箱进行操作
        public EventSet<E_EntityOutTrigger, EventSetData> _out_event_set                                // 角色的外部事件
           = new EventSet<E_EntityOutTrigger, EventSetData>();
        public EventSet<E_EntityInTrigger, EventSetData> _in_event_set                                  // 角色的内部事件
       = new EventSet<E_EntityInTrigger, EventSetData>();

        public EntityBlackBoard _entity_blackboard = new EntityBlackBoard();


        public List<I_Update> update_list = new List<I_Update>();                                       // 对应需要注册到容器中的组件 进行Update
        public List<I_RegisterHandler> register_list = new List<I_RegisterHandler>();                   // 注册项



        #region 缓存池相关

        public bool IsUse { get; set; }                                                                 // true=使用中
        public string ObjectName { get { return "BaseEntity"; } }

        #endregion

        #region 附带属性

        public BtEntityAi _entity_ai;                                                                   // 相关AI组件
        public SkillSet _skill_set;                                                                     // 相关技能组件
        public BuffSet _buff_set;                                                                        // 相关Buff组件
        public EntitiesAttributeProperty _attr_prop;                                                    // 相关人物属性组件
        public FsmSystem _fsm_system;                                                                   // 相关状态机组件

        #endregion

        #region Mono的附带属性

        public EntityAnimationGroup _anim_group;                                                        // 动画组件 播放动画
        public EntityMovement _movement;                                                                // 移动组件 人物移动
        public BaseEntityController _entity_controller;                                                 // 角色控制器 

        #endregion

        #endregion

        #region I_Update/I_EntityInTrigger/I_EntityOutTrigger/I_EntityLife

        #region OnUpdate

        public void OnUpdate(float dt)
        {
            EntityController.OnUpdate(dt);
            _movement.OnUpdate(dt);
            int length = update_list.Count;
            for (int i = 0; i < length; i++)
            {
                update_list[i].OnUpdate(dt);
            }
        }

        #endregion

        #region I_EntityInTrigger 注册监听人物的外部事件

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

        #region I_EntityOutTrigger 注册监听人物的内部事件 基本都是一些原子节点

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

        #region I_EntityLife 缓存池相关函数

        // 第一次被初始化出来
        public virtual void OnInit()
        {
            _skill_set = new SkillSet(this);
            _buff_set = new BuffSet(this);
            _fsm_system = EntityFsmFactory.CreateFsmSystem(this);
            _entity_ai = new BtEntityAi(this);
        }

        // 从缓存池中提取
        public virtual void OnPop(int hero_id)
        {
            Template = hero_id;             // 模板ID
            _init_data();
            _init_gameobject();

            // 更新通道
            update_list.Add(_skill_set);
            update_list.Add(_fsm_system);
            update_list.Add(_buff_set);

            // 注册
            _fsm_system.OnRegisterHandler();
            _skill_set.OnRegisterHandler();
            _buff_set.OnRegisterHandler();
            _attr_prop.OnRegisterHandler();

            OnRegisterHandler();

            _anim_group.PlayAnimation(AnimationNameConst.IDLE);
            _fsm_system.Start();
        }

        // 放入缓存池
        public virtual void OnPush()
        {
            Clear();
            TransformPool.Instance.Push(EntityController);
            EntityController = null;
        }


        #endregion

        #region I_SkillBlackBorad 黑箱数据

        public EntityBlackBoard GetBlackBorad()
        {
            return _entity_blackboard;
        }

        /*public T GetBlackBoradValue<T>(string key, T default_value)
        {
            T t = _skill_blackboard.GetValue<T>(key, default_value);
            return t;
        }*/

        #endregion

        #endregion

        #region public

        #region 监听的内部事件,通过一些原子节点触发事件，迫使Entity做出某些行为

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

        // 目标死亡
        public void EntityDie(EventSetData param)
        {
            EntityEventFactory.ChangeInEntityState(this, E_StateId.die);
        }
        #endregion

        #region 监听的外部事件，比如动作文件为源头，触发动作事件

        public void OnBeHurt(EventSetData param)
        {
            EntityEventFactory.ChangeInEntityState(this, E_StateId.hurt);
        }

        #endregion

        public void OnRegisterHandler()
        {
            RegisterHandler(E_EntityInTrigger.find_targets, FindTargets);
            RegisterHandler(E_EntityInTrigger.export_to_target, ExportToTarget);
            RegisterHandler(E_EntityInTrigger.entity_die, EntityDie);
            RegisterHandler(E_EntityOutTrigger.on_be_hurt, OnBeHurt);
        }

        public bool IsDead() { return false; }

        public void ReceiveCommandMove(Vector2 diretion)
        {
            if (!CanMovement) return;
            _movement.AddDirection(diretion);
        }

        public void InitPosRot()
        {
            EntityController.trans.position = new Vector3(Random.value * 40 - 20f, 0, Random.value * 40 - 20f);
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
            for (int i = 0; i < register_list.Count; i++)
                register_list[i].UnRegisterHandler();
            register_list.Clear();
        }
        public string ToDes() { return ""; }
        #endregion

        #region

        // 初始化GameObject的相关东西
        public void _init_gameobject()
        {
            // 加载模型
            BaseEntityController go = TransformPool.Instance.Pop<BaseEntityController>
                ("res_bundle/prefab/model/Character/" + "NPC_Zhaoyun_001_02"/*_cnf.prefab_name*/);
            EntityController = go;
            EntityController.InitOutTrigger(this, this);

            _anim_group = go.GetComponent<EntityAnimationGroup>();
            _anim_group.OnInit(this);
            _anim_group.OnRegisterHandler();

            _movement = go.GetComponent<EntityMovement>();
            _movement._base_entity = this;
        }

        public void _init_data()
        {
            CanMovement = true;
            entity_id = new EntityId();

            _cnf = StaticCnf.FindData<HeroInfoCnf>(Template);
            _skill_set.OnReset(Template);
            _attr_prop = new EntitiesAttributeProperty(entity_id);
        }

        #endregion
    }
}